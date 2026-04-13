using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _uow;
    public EnrollmentService(IUnitOfWork uow) => _uow = uow;

    public async Task<int> EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct = default)
    {
        _ = await _uow.Students.GetByIdAsync(dto.StudentId, ct)
            ?? throw new NotFoundException(nameof(Student), dto.StudentId);
        var group = await _uow.Groups.GetByIdAsync(dto.GroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.GroupId);

        if (dto.SubgroupId.HasValue && !group.Subgroups.Any(sg => sg.SubgroupId == dto.SubgroupId.Value))
            throw new DomainException($"Subgroup {dto.SubgroupId.Value} does not belong to Group {dto.GroupId}.");

        var hasOverlap = await _uow.Enrollments.HasOverlapAsync(dto.StudentId, dto.DateFrom, null, null, ct);
        if (hasOverlap)
            throw new EnrollmentOverlapException(dto.StudentId, dto.DateFrom, null);

        var enrollment = new StudentGroupEnrollment
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            DateFrom = dto.DateFrom,
            ReasonStart = dto.ReasonStart,
            SubgroupAssignment = dto.SubgroupId.HasValue
                ? new StudentSubgroupAssignment { SubgroupId = dto.SubgroupId.Value }
                : null
        };
        _uow.Enrollments.Add(enrollment);
        await _uow.SaveChangesAsync(ct);

        // Generate course enrollments from group's active plan
        var activePlan = await _uow.GroupPlanAssignments.GetActiveOnDateAsync(dto.GroupId, dto.DateFrom, ct);
        if (activePlan is not null)
        {
            var courses = StudyPlanService.GenerateCourseEnrollments(
                enrollment.EnrollmentId, activePlan.GroupPlanAssignmentId, dto.DateFrom, activePlan.Plan);
            _uow.StudyPlans.AddCourseEnrollments(courses);
            await _uow.SaveChangesAsync(ct);
        }

        return enrollment.EnrollmentId;
    }

    public async Task CloseEnrollmentAsync(int enrollmentId, CloseEnrollmentDto dto, CancellationToken ct = default)
    {
        var enrollment = await _uow.Enrollments.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);
        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Enrollment {enrollmentId} is already closed.");
        if (dto.DateTo < enrollment.DateFrom)
            throw new DomainException("DateTo cannot be before DateFrom.");
        enrollment.DateTo = dto.DateTo;
        enrollment.ReasonEnd = dto.ReasonEnd;
        _uow.Enrollments.Update(enrollment);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task MoveToGroupAsync(int studentId, MoveStudentDto dto, CancellationToken ct = default)
    {
        _ = await _uow.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);
        var newGroup = await _uow.Groups.GetByIdAsync(dto.NewGroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.NewGroupId);

        if (dto.NewSubgroupId.HasValue && !newGroup.Subgroups.Any(sg => sg.SubgroupId == dto.NewSubgroupId.Value))
            throw new DomainException($"Subgroup {dto.NewSubgroupId.Value} does not belong to Group {dto.NewGroupId}.");

        var current = await _uow.Enrollments.GetActiveByStudentIdAsync(studentId, ct)
            ?? throw new DomainException($"Student {studentId} has no active enrollment to move from.");
        if (dto.MoveDate < current.DateFrom)
            throw new DomainException("Move date cannot be before the current enrollment's start date.");

        // Preserve non-Planned course enrollments; remove Planned ones from old enrollment
        var oldCourses = (await _uow.StudyPlans.GetCourseEnrollmentsByEnrollmentIdAsync(current.EnrollmentId, ct)).ToList();
        var toRemove = oldCourses.Where(ce => ce.Status == CourseStatus.Planned).ToList();
        _uow.StudyPlans.RemoveCourseEnrollments(toRemove);

        current.DateTo = dto.MoveDate;
        current.ReasonEnd = dto.ReasonEnd;
        _uow.Enrollments.Update(current);

        var newEnrollment = new StudentGroupEnrollment
        {
            StudentId = studentId,
            GroupId = dto.NewGroupId,
            DateFrom = dto.MoveDate,
            ReasonStart = dto.ReasonStart,
            SubgroupAssignment = dto.NewSubgroupId.HasValue
                ? new StudentSubgroupAssignment { SubgroupId = dto.NewSubgroupId.Value }
                : null
        };
        _uow.Enrollments.Add(newEnrollment);
        await _uow.SaveChangesAsync(ct);

        // Generate new Planned courses from new group's active plan
        var activePlan = await _uow.GroupPlanAssignments.GetActiveOnDateAsync(dto.NewGroupId, dto.MoveDate, ct);
        if (activePlan is not null)
        {
            // Compute already-completed discipline IDs to avoid duplicating them
            var completedDisciplineIds = oldCourses
                .Where(ce => ce.Status != CourseStatus.Planned)
                .Select(ce => ce.DisciplineId)
                .ToHashSet();

            var newCourses = StudyPlanService.GenerateCourseEnrollments(
                    newEnrollment.EnrollmentId, activePlan.GroupPlanAssignmentId, dto.MoveDate, activePlan.Plan)
                .Where(ce => !completedDisciplineIds.Contains(ce.DisciplineId))
                .ToList();
            _uow.StudyPlans.AddCourseEnrollments(newCourses);
            await _uow.SaveChangesAsync(ct);
        }
    }

    public async Task AssignSubgroupAsync(int enrollmentId, AssignSubgroupDto dto, CancellationToken ct = default)
    {
        var enrollment = await _uow.Enrollments.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);
        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Cannot assign subgroup to a closed enrollment {enrollmentId}.");
        if (!enrollment.Group.Subgroups.Any(sg => sg.SubgroupId == dto.SubgroupId))
            throw new DomainException($"Subgroup {dto.SubgroupId} does not belong to Group {enrollment.GroupId}.");
        var existing = await _uow.SubgroupAssignments.GetByEnrollmentIdAsync(enrollmentId, ct);
        _uow.SubgroupAssignments.Upsert(existing,
            new StudentSubgroupAssignment { EnrollmentId = enrollmentId, SubgroupId = dto.SubgroupId });
        await _uow.SaveChangesAsync(ct);
    }
}
