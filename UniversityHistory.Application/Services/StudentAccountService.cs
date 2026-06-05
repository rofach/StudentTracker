using UniversityHistory.Application.Auth;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Auth;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
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
        return await CreateStudentWithAccountAsync(dto, ct);
    }

    public async Task<StudentCreatedResultDto> CreateTransferredStudentAsync(
        CreateTransferredStudentDto dto,
        CancellationToken ct = default)
    {
        var studentDto = dto.ToStudentCreateDto();
        var email = studentDto.Email!;

        await _identityAccountManager.EnsureRoleExistsAsync(AuthRoles.Student, ct);
        await _identityAccountManager.EnsureEmailIsAvailableAsync(email, null, ct);

        return await _unitOfWork.ExecuteInTransactionAsync(async innerCt =>
        {
            var result = await CreateStudentWithAccountCoreAsync(studentDto, email, innerCt);

            await EnrollTransferredStudentAsync(
                result.Student.StudentId,
                dto.GroupId,
                dto.SubgroupId,
                dto.DateFrom,
                dto.InstitutionId,
                dto.NewInstitutionName,
                dto.Notes,
                innerCt);

            return result;
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

    private async Task<StudentCreatedResultDto> CreateStudentWithAccountAsync(StudentCreateDto dto, CancellationToken ct)
    {
        var email = dto.Email!;

        await _identityAccountManager.EnsureRoleExistsAsync(AuthRoles.Student, ct);
        await _identityAccountManager.EnsureEmailIsAvailableAsync(email, null, ct);

        return await _unitOfWork.ExecuteInTransactionAsync(async innerCt =>
        {
            return await CreateStudentWithAccountCoreAsync(dto, email, innerCt);
        }, ct);
    }

    private async Task<StudentCreatedResultDto> CreateStudentWithAccountCoreAsync(
        StudentCreateDto dto,
        string email,
        CancellationToken ct)
    {
        var student = await _studentService.CreateAsync(dto, ct);
        var password = CreateGeneratedPassword();

        var account = await _identityAccountManager.CreateAccountAsync(
            email,
            email,
            password.Value,
            student.StudentId,
            emailConfirmed: true,
            ct);

        await _identityAccountManager.EnsureRoleAssignedAsync(account.UserId, AuthRoles.Student, ct);

        return new StudentCreatedResultDto(
            student,
            new StudentAccountPasswordDto(email, password.Value, password.GeneratedRandomly));
    }

    private async Task EnrollTransferredStudentAsync(
        Guid studentId,
        Guid groupId,
        Guid? subgroupId,
        DateOnly dateFrom,
        Guid? institutionId,
        string? newInstitutionName,
        string? notes,
        CancellationToken ct)
    {
        Guid actualInstitutionId;
        if (!institutionId.HasValue || institutionId.Value == Guid.Empty)
        {
            if (string.IsNullOrWhiteSpace(newInstitutionName))
                throw new DomainException("Необхідно вказати заклад або назву нового закладу.");

            var newInst = new Domain.Entities.Institution
            {
                InstitutionName = newInstitutionName.Trim()
            };
            _unitOfWork.ExternalTransfers.AddInstitution(newInst);
            await _unitOfWork.SaveChangesAsync(ct);
            actualInstitutionId = newInst.InstitutionId;
        }
        else
        {
            actualInstitutionId = institutionId.Value;
            _ = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(actualInstitutionId, ct)
                ?? throw new NotFoundException("Institution", actualInstitutionId);
        }

        var group = await _unitOfWork.Groups.GetByIdAsync(groupId, ct)
            ?? throw new NotFoundException("StudyGroup", groupId);

        if (subgroupId.HasValue && !group.Subgroups.Any(sg => sg.SubgroupId == subgroupId.Value))
            throw new DomainException($"Subgroup {subgroupId.Value} does not belong to Group {groupId}.");

        if (await _unitOfWork.Enrollments.HasOverlapAsync(studentId, dateFrom, null, null, ct))
            throw new DomainException("Для студента вже існує активне або таке, що перетинається, зарахування.");

        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException("Student", studentId);

        student.Status = Domain.Enums.StudentStatus.Active;
        _unitOfWork.Students.Update(student);

        var enrollment = new Domain.Entities.StudentGroupEnrollment
        {
            StudentId = studentId,
            GroupId = groupId,
            DateFrom = dateFrom,
            ReasonStart = "Переведення з іншого університету"
        };
        _unitOfWork.Enrollments.Add(enrollment);
        await _unitOfWork.SaveChangesAsync(ct);

        if (subgroupId.HasValue)
        {
            _unitOfWork.SubgroupEnrollments.Add(new Domain.Entities.StudentSubgroupEnrollment
            {
                EnrollmentId = enrollment.EnrollmentId,
                SubgroupId = subgroupId.Value,
                DateFrom = dateFrom,
                Reason = "Переведення з іншого університету"
            });
        }

        var activePlan = await _unitOfWork.GroupPlanAssignments.GetActiveOnDateAsync(groupId, dateFrom, ct);
        if (activePlan is not null)
        {
            var courses = StudyPlanService.GenerateCourseEnrollments(
                enrollment.EnrollmentId,
                activePlan.GroupPlanAssignmentId,
                dateFrom,
                activePlan.Plan);
            _unitOfWork.StudyPlans.AddCourseEnrollments(courses);
        }

        _unitOfWork.ExternalTransfers.Add(new Domain.Entities.ExternalTransfer
        {
            StudentId = studentId,
            InstitutionId = actualInstitutionId,
            TransferType = Domain.Enums.TransferType.In,
            TransferDate = dateFrom,
            Notes = notes
        });

        await _unitOfWork.SaveChangesAsync(ct);
    }

    private readonly record struct GeneratedPassword(string Value, bool GeneratedRandomly);
}
