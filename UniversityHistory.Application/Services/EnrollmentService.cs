using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IUnitOfWork uow) => _unitOfWork = uow;

    public async Task<Guid> EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(dto.StudentId, ct)
            ?? throw new NotFoundException(nameof(Student), dto.StudentId);
        var group = await _unitOfWork.Groups.GetByIdAsync(dto.GroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.GroupId);

        if (dto.SubgroupId.HasValue && !group.Subgroups.Any(sg => sg.SubgroupId == dto.SubgroupId.Value))
            throw new DomainException($"Subgroup {dto.SubgroupId.Value} does not belong to Group {dto.GroupId}.");

        if (await _unitOfWork.Enrollments.HasOverlapAsync(dto.StudentId, dto.DateFrom, null, null, ct))
            throw new EnrollmentOverlapException(dto.StudentId, dto.DateFrom, null);

        var enrollment = new StudentGroupEnrollment
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            DateFrom = dto.DateFrom,
            ReasonStart = dto.ReasonStart
        };
        _unitOfWork.Enrollments.Add(enrollment);
        await _unitOfWork.SaveChangesAsync(ct);

        if (dto.SubgroupId.HasValue)
            OpenSubgroupEnrollment(enrollment.EnrollmentId, dto.SubgroupId.Value, dto.DateFrom, dto.ReasonStart);

        var activePlan = await _unitOfWork.GroupPlanAssignments.GetActiveOnDateAsync(dto.GroupId, dto.DateFrom, ct);
        if (activePlan is not null)
        {
            var courses = StudyPlanService.GenerateCourseEnrollments(
                enrollment.EnrollmentId, activePlan.GroupPlanAssignmentId, dto.DateFrom, activePlan.Plan);
            _unitOfWork.StudyPlans.AddCourseEnrollments(courses);
        }

        await _unitOfWork.SaveChangesAsync(ct);
        return enrollment.EnrollmentId;
    }

    public async Task CloseEnrollmentAsync(Guid enrollmentId, CloseEnrollmentDto dto, CancellationToken ct = default)
    {
        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);
        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Enrollment {enrollmentId} is already closed.");
        if (dto.DateTo < enrollment.DateFrom)
            throw new DomainException("DateTo cannot be before DateFrom.");
        enrollment.DateTo = dto.DateTo;
        enrollment.ReasonEnd = dto.ReasonEnd;
        _unitOfWork.Enrollments.Update(enrollment);

        var openSubgroupEnrollment = await _unitOfWork.SubgroupEnrollments.GetOpenByEnrollmentIdAsync(enrollmentId, ct);
        if (openSubgroupEnrollment is not null)
        {
            if (dto.DateTo < openSubgroupEnrollment.DateFrom)
                throw new DomainException("Enrollment end date cannot be before the active subgroup enrollment start date.");

            openSubgroupEnrollment.DateTo = dto.DateTo;
            _unitOfWork.SubgroupEnrollments.Update(openSubgroupEnrollment);
        }

        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task MoveToGroupAsync(Guid studentId, MoveStudentDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);
        var newGroup = await _unitOfWork.Groups.GetByIdAsync(dto.NewGroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.NewGroupId);

        if (dto.NewSubgroupId.HasValue && !newGroup.Subgroups.Any(sg => sg.SubgroupId == dto.NewSubgroupId.Value))
            throw new DomainException($"Subgroup {dto.NewSubgroupId.Value} does not belong to Group {dto.NewGroupId}.");

        var current = await _unitOfWork.Enrollments.GetActiveByStudentIdAsync(studentId, ct)
            ?? throw new DomainException($"Student {studentId} has no active enrollment to move from.");

        if (await _unitOfWork.AcademicLeaves.HasActiveLeaveOnDateAsync(current.EnrollmentId, dto.MoveDate, ct))
            throw new DomainException("Cannot modify study process while the student is on academic leave.");

        if (dto.MoveDate <= current.DateFrom)
            throw new DomainException("Move date cannot be before the current enrollment's start date.");

        var oldCourses = (await _unitOfWork.StudyPlans.GetCourseEnrollmentsByEnrollmentIdAsync(current.EnrollmentId, ct)).ToList();
        var toRemove = oldCourses.Where(ce => ce.Status == CourseStatus.Planned).ToList();
        _unitOfWork.StudyPlans.RemoveCourseEnrollments(toRemove);

        var openSubgroupEnrollment = await _unitOfWork.SubgroupEnrollments
            .GetOpenByEnrollmentIdAsync(current.EnrollmentId, ct);
        if (openSubgroupEnrollment is not null)
        {
            if (dto.MoveDate <= openSubgroupEnrollment.DateFrom)
                throw new DomainException("Move date must be after the current subgroup enrollment's start date.");

            openSubgroupEnrollment.DateTo = dto.MoveDate.AddDays(-1);
            _unitOfWork.SubgroupEnrollments.Update(openSubgroupEnrollment);
        }

        current.DateTo = dto.MoveDate.AddDays(-1);
        current.ReasonEnd = dto.ReasonEnd;
        _unitOfWork.Enrollments.Update(current);

        var newEnrollment = new StudentGroupEnrollment
        {
            StudentId = studentId,
            GroupId = dto.NewGroupId,
            DateFrom = dto.MoveDate,
            ReasonStart = dto.ReasonStart
        };
        _unitOfWork.Enrollments.Add(newEnrollment);
        await _unitOfWork.SaveChangesAsync(ct);

        var activePlan = await _unitOfWork.GroupPlanAssignments.GetActiveOnDateAsync(dto.NewGroupId, dto.MoveDate, ct);
        if (activePlan is not null)
        {
            var completedDisciplineIds = oldCourses
                .Where(ce => ce.Status != CourseStatus.Planned)
                .Select(ce => ce.PlanDiscipline.DisciplineId)
                .ToHashSet();

            var newCourses = StudyPlanService.GenerateCourseEnrollments(
                    newEnrollment.EnrollmentId, activePlan.GroupPlanAssignmentId, dto.MoveDate, activePlan.Plan)
                .Where(ce => !completedDisciplineIds.Contains(ce.PlanDiscipline.DisciplineId))
                .ToList();
            _unitOfWork.StudyPlans.AddCourseEnrollments(newCourses);
        }

        if (dto.NewSubgroupId.HasValue)
            OpenSubgroupEnrollment(newEnrollment.EnrollmentId, dto.NewSubgroupId.Value, dto.MoveDate, dto.ReasonStart);

        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task AssignSubgroupAsync(Guid enrollmentId, AssignSubgroupDto dto, CancellationToken ct = default)
    {
        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);
        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Cannot assign subgroup to a closed enrollment {enrollmentId}.");

        var operationDate = DateOnly.FromDateTime(DateTime.Today);
        if (await _unitOfWork.AcademicLeaves.HasActiveLeaveOnDateAsync(enrollmentId, operationDate, ct))
            throw new DomainException("Cannot modify study process while the student is on academic leave.");

        if (!enrollment.Group.Subgroups.Any(sg => sg.SubgroupId == dto.SubgroupId))
            throw new DomainException($"Subgroup {dto.SubgroupId} does not belong to Group {enrollment.GroupId}.");

        var open = await _unitOfWork.SubgroupEnrollments.GetOpenByEnrollmentIdAsync(enrollmentId, ct);
        if (open is not null)
            throw new DomainException("Student already has an active subgroup enrollment. Use subgroup-move instead.");

        OpenSubgroupEnrollment(enrollmentId, dto.SubgroupId, operationDate, "Initial subgroup assignment");
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task MoveSubgroupAsync(Guid enrollmentId, MoveStudentToSubgroupDto dto, CancellationToken ct = default)
    {
        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);

        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Cannot move subgroup for a closed enrollment {enrollmentId}.");

        if (await _unitOfWork.AcademicLeaves.HasActiveLeaveOnDateAsync(enrollmentId, dto.MoveDate, ct))
            throw new DomainException("Cannot modify study process while the student is on academic leave.");

        if (dto.MoveDate < enrollment.DateFrom)
            throw new DomainException("Move date cannot be before the enrollment start date.");

        if (!enrollment.Group.Subgroups.Any(sg => sg.SubgroupId == dto.NewSubgroupId))
            throw new DomainException($"Subgroup {dto.NewSubgroupId} does not belong to Group {enrollment.GroupId}.");

        var open = await _unitOfWork.SubgroupEnrollments.GetOpenByEnrollmentIdAsync(enrollmentId, ct);
        if (open is null)
            throw new DomainException("Student has no active subgroup enrollment to transfer from.");

        if (open.SubgroupId == dto.NewSubgroupId)
            throw new DomainException("Student is already assigned to this subgroup.");

        if (dto.MoveDate <= open.DateFrom)
            throw new DomainException("Move date must be after the current subgroup enrollment's start date.");

        open.DateTo = dto.MoveDate.AddDays(-1);
        _unitOfWork.SubgroupEnrollments.Update(open);

        OpenSubgroupEnrollment(enrollmentId, dto.NewSubgroupId, dto.MoveDate, dto.Reason);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    private void OpenSubgroupEnrollment(Guid enrollmentId, Guid subgroupId, DateOnly dateFrom, string reason)
    {
        _unitOfWork.SubgroupEnrollments.Add(new StudentSubgroupEnrollment
        {
            EnrollmentId = enrollmentId,
            SubgroupId = subgroupId,
            DateFrom = dateFrom,
            DateTo = null,
            Reason = reason
        });
    }

}
