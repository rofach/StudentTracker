using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IGroupRepository _groupRepo;
    private readonly IEnrollmentRepository _enrollmentRepo;
    private readonly ISubgroupAssignmentRepository _subgroupRepo;

    public EnrollmentService(
        IStudentRepository studentRepo,
        IGroupRepository groupRepo,
        IEnrollmentRepository enrollmentRepo,
        ISubgroupAssignmentRepository subgroupRepo)
    {
        _studentRepo  = studentRepo;
        _groupRepo    = groupRepo;
        _enrollmentRepo = enrollmentRepo;
        _subgroupRepo = subgroupRepo;
    }

    public async Task<int> EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(dto.StudentId, ct)
            ?? throw new NotFoundException(nameof(Student), dto.StudentId);
        var group = await _groupRepo.GetByIdAsync(dto.GroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.GroupId);

        if (dto.SubgroupId.HasValue)
        {
            var isValidSubgroup = group.Subgroups.Any(sg => sg.SubgroupId == dto.SubgroupId.Value);
            if (!isValidSubgroup)
                throw new DomainException($"Validation Failed: Subgroup {dto.SubgroupId.Value} does not belong to Group {dto.GroupId}.");
        }

        var hasOverlap = await _enrollmentRepo.HasOverlapAsync(dto.StudentId, dto.DateFrom, null, null, ct);
        if (hasOverlap)
            throw new EnrollmentOverlapException(dto.StudentId, dto.DateFrom, null);

        var enrollment = new StudentGroupEnrollment
        {
            StudentId   = dto.StudentId,
            GroupId     = dto.GroupId,
            DateFrom    = dto.DateFrom,
            ReasonStart = dto.ReasonStart,
            SubgroupAssignment = dto.SubgroupId.HasValue
                ? new StudentSubgroupAssignment { SubgroupId = dto.SubgroupId.Value }
                : null
        };

        var created = await _enrollmentRepo.AddAsync(enrollment, ct);
        return created.EnrollmentId;
    }

    public async Task CloseEnrollmentAsync(int enrollmentId, CloseEnrollmentDto dto, CancellationToken ct = default)
    {
        var enrollment = await _enrollmentRepo.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);

        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Enrollment {enrollmentId} is already closed.");

        if (dto.DateTo < enrollment.DateFrom)
            throw new DomainException("DateTo cannot be before DateFrom.");

        enrollment.DateTo    = dto.DateTo;
        enrollment.ReasonEnd = dto.ReasonEnd;
        await _enrollmentRepo.UpdateAsync(enrollment, ct);
    }

    public async Task MoveToGroupAsync(int studentId, MoveStudentDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var newGroup = await _groupRepo.GetByIdAsync(dto.NewGroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.NewGroupId);

        if (dto.NewSubgroupId.HasValue && !newGroup.Subgroups.Any(sg => sg.SubgroupId == dto.NewSubgroupId.Value))
            throw new DomainException($"Subgroup {dto.NewSubgroupId.Value} does not belong to Group {dto.NewGroupId}.");

        var current = await _enrollmentRepo.GetActiveByStudentIdAsync(studentId, ct)
            ?? throw new DomainException($"Student {studentId} has no active enrollment to move from.");

        if (dto.MoveDate < current.DateFrom)
            throw new DomainException("Move date cannot be before the current enrollment's start date.");

        current.DateTo    = dto.MoveDate;
        current.ReasonEnd = dto.ReasonEnd;
        await _enrollmentRepo.UpdateAsync(current, ct);

        var newEnrollment = new StudentGroupEnrollment
        {
            StudentId   = studentId,
            GroupId     = dto.NewGroupId,
            DateFrom    = dto.MoveDate,
            ReasonStart = dto.ReasonStart,
            SubgroupAssignment = dto.NewSubgroupId.HasValue
                ? new StudentSubgroupAssignment { SubgroupId = dto.NewSubgroupId.Value }
                : null
        };

        await _enrollmentRepo.AddAsync(newEnrollment, ct);
    }

    public async Task AssignSubgroupAsync(int enrollmentId, AssignSubgroupDto dto, CancellationToken ct = default)
    {
        var enrollment = await _enrollmentRepo.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);

        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Cannot assign subgroup to a closed enrollment {enrollmentId}.");

        var isValidSubgroup = enrollment.Group.Subgroups.Any(sg => sg.SubgroupId == dto.SubgroupId);
        if (!isValidSubgroup)
            throw new DomainException($"Subgroup {dto.SubgroupId} does not belong to Group {enrollment.GroupId}.");

        await _subgroupRepo.UpsertAsync(
            new StudentSubgroupAssignment { EnrollmentId = enrollmentId, SubgroupId = dto.SubgroupId }, ct);
    }
}
