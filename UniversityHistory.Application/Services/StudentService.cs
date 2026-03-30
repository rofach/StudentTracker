using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IEnrollmentRepository _enrollmentRepo;
    private readonly IMovementService _movementService;
    private readonly IStudyPlanService _planService;

    public StudentService(
        IStudentRepository studentRepo,
        IEnrollmentRepository enrollmentRepo,
        IMovementService movementService,
        IStudyPlanService planService)
    {
        _studentRepo    = studentRepo;
        _enrollmentRepo = enrollmentRepo;
        _movementService = movementService;
        _planService    = planService;
    }

    public async Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct);
        return student is null ? null : MapToDto(student);
    }

    public async Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var (items, count) = await _studentRepo.GetAllAsync(page, pageSize, ct);
        return new PagedResult<StudentDto>(items.Select(MapToDto), page, pageSize, count);
    }

    public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
    {
        var student = new Student
        {
            FirstName = dto.FirstName,
            LastName  = dto.LastName,
            BirthDate = dto.BirthDate,
            Email     = dto.Email,
            Phone     = dto.Phone,
            Status    = StudentStatus.Active
        };
        return MapToDto(await _studentRepo.AddAsync(student, ct));
    }

    public async Task<StudentDto> UpdateAsync(int studentId, StudentUpdateDto dto, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        student.FirstName = dto.FirstName;
        student.LastName  = dto.LastName;
        student.BirthDate = dto.BirthDate;
        student.Email     = dto.Email;
        student.Phone     = dto.Phone;

        await _studentRepo.UpdateAsync(student, ct);
        return MapToDto(student);
    }

    public async Task ChangeStatusAsync(int studentId, ChangeStatusDto dto, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        if (!Enum.TryParse<StudentStatus>(dto.Status, ignoreCase: true, out var newStatus))
            throw new DomainException($"Unknown status: '{dto.Status}'. Valid values: Active, OnLeave, Expelled, Graduated.");

        if (student.Status is StudentStatus.Expelled or StudentStatus.Graduated)
            throw new DomainException($"Cannot change status of a student with terminal status '{student.Status}'.");

        student.Status = newStatus;
        await _studentRepo.UpdateAsync(student, ct);
    }

    public async Task<StudentDetailDto> GetDetailAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollments = await _enrollmentRepo.GetByStudentIdAsync(studentId, ct);
        var plans       = await _planService.GetPlanAssignmentsAsync(studentId, ct);
        var movements   = await _movementService.GetMovementsAsync(studentId, ct);

        return new StudentDetailDto(
            student.StudentId,
            student.FirstName,
            student.LastName,
            student.BirthDate,
            student.Email,
            student.Phone,
            student.Status.ToString(),
            enrollments.Select(e => new EnrollmentSummaryDto(
                e.EnrollmentId,
                e.GroupId,
                e.Group.GroupCode,
                e.Group.Faculty,
                e.DateFrom,
                e.DateTo,
                e.SubgroupAssignment?.SubgroupId,
                e.SubgroupAssignment?.Subgroup.SubgroupName)),
            plans,
            movements.Leaves,
            movements.Transfers
        );
    }

    private static StudentDto MapToDto(Student s) =>
        new(s.StudentId, s.FirstName, s.LastName, s.BirthDate, s.Email, s.Phone, s.Status.ToString());
}
