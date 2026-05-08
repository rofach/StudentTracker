using UniversityHistory.Application.Auth;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Auth;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class StudentAccountService : IStudentAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentService _studentService;
    private readonly IIdentityAccountManager _identityAccountManager;
    private readonly IPasswordGenerator _passwordGenerator;

    public StudentAccountService(
        IUnitOfWork unitOfWork,
        IStudentService studentService,
        IIdentityAccountManager identityAccountManager,
        IPasswordGenerator passwordGenerator)
    {
        _unitOfWork = unitOfWork;
        _studentService = studentService;
        _identityAccountManager = identityAccountManager;
        _passwordGenerator = passwordGenerator;
    }

    public async Task<StudentCreatedResultDto> CreateStudentAsync(StudentCreateDto dto, CancellationToken ct = default)
    {
        var email = dto.Email!;

        await _identityAccountManager.EnsureRoleExistsAsync(AuthRoles.Student, ct);
        await _identityAccountManager.EnsureEmailIsAvailableAsync(email, null, ct);

        return await _unitOfWork.ExecuteInTransactionAsync(async innerCt =>
        {
            var student = await _studentService.CreateAsync(dto, innerCt);
            var password = CreateGeneratedPassword();

            var account = await _identityAccountManager.CreateAccountAsync(
                email,
                email,
                password.Value,
                student.StudentId,
                emailConfirmed: true,
                innerCt);

            await _identityAccountManager.EnsureRoleAssignedAsync(account.UserId, AuthRoles.Student, innerCt);

            return new StudentCreatedResultDto(
                student,
                new StudentAccountPasswordDto(email, password.Value, password.GeneratedRandomly));
        }, ct);
    }

    public Task<StudentDto> UpdateStudentAsync(Guid studentId, StudentUpdateDto dto, CancellationToken ct = default)
    {
        return _unitOfWork.ExecuteInTransactionAsync(async innerCt =>
        {
            var account = await _identityAccountManager.FindByStudentIdAsync(studentId, innerCt);

            if (account is not null)
                await _identityAccountManager.EnsureEmailIsAvailableAsync(dto.Email!, account.UserId, innerCt);

            var student = await _studentService.UpdateAsync(studentId, dto, innerCt);

            if (account is not null)
                await _identityAccountManager.SyncEmailAsync(account.UserId, dto.Email!, innerCt);

            return student;
        }, ct);
    }

    public async Task<StudentAccountPasswordDto> ResetPasswordAsync(
        Guid studentId,
        ResetStudentPasswordDto dto,
        CancellationToken ct = default)
    {
        var student = await GetRequiredStudentWithEmailAsync(studentId, ct);
        var password = string.IsNullOrWhiteSpace(dto.NewPassword)
            ? CreateGeneratedPassword()
            : new GeneratedPassword(dto.NewPassword.Trim(), false);

        await _identityAccountManager.EnsureRoleExistsAsync(AuthRoles.Student, ct);

        return await _unitOfWork.ExecuteInTransactionAsync(async innerCt =>
        {
            var account = await _identityAccountManager.FindByStudentIdAsync(studentId, innerCt);
            if (account is null)
            {
                account = await _identityAccountManager.CreateAccountAsync(
                    student.Email!,
                    student.Email!,
                    password.Value,
                    studentId,
                    emailConfirmed: true,
                    innerCt);

                await _identityAccountManager.EnsureRoleAssignedAsync(account.UserId, AuthRoles.Student, innerCt);
            }
            else
            {
                await _identityAccountManager.SyncEmailAsync(account.UserId, student.Email!, innerCt);
                await _identityAccountManager.SetPasswordAsync(account.UserId, password.Value, innerCt);
            }

            return new StudentAccountPasswordDto(student.Email!, password.Value, password.GeneratedRandomly);
        }, ct);
    }

    private async Task<StudentDto> GetRequiredStudentWithEmailAsync(Guid studentId, CancellationToken ct)
    {
        var student = await _studentService.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException("Student", studentId);

        if (string.IsNullOrWhiteSpace(student.Email))
            throw new DomainException("Для студента не вказано email. Неможливо створити або оновити обліковий запис.");

        return student;
    }

    private GeneratedPassword CreateGeneratedPassword()
    {
        return new GeneratedPassword(_passwordGenerator.Generate(), true);
    }

    private readonly record struct GeneratedPassword(string Value, bool GeneratedRandomly);
}
