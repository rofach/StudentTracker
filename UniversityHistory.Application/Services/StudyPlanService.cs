using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class StudyPlanService : IStudyPlanService
{
    private readonly IUnitOfWork _unitOfWork;
    public StudyPlanService(IUnitOfWork uow) => _unitOfWork = uow;

    public async Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default)
    {
        var plans = await _unitOfWork.StudyPlans.GetAllPlansAsync(ct);
        return plans.Select(p => p.ToDto());
    }

    public async Task<StudyPlanDto?> GetPlanByIdAsync(Guid planId, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct);
        return plan is null ? null : plan.ToDto();
    }

    public async Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = dto.ToEntity();
        _unitOfWork.StudyPlans.AddPlan(plan);
        await _unitOfWork.SaveChangesAsync(ct);
        return plan.ToDto();
    }

    public async Task<StudyPlanDto> UpdatePlanAsync(Guid planId, UpdateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        plan.SpecialtyCode = dto.SpecialtyCode;
        plan.PlanName = dto.PlanName;
        plan.ValidFrom = dto.ValidFrom;
        _unitOfWork.StudyPlans.UpdatePlan(plan);
        await _unitOfWork.SaveChangesAsync(ct);
        return plan.ToDto();
    }

    public async Task DeletePlanAsync(Guid planId, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        if (await _unitOfWork.StudyPlans.PlanHasAssignmentsAsync(planId, ct))
            throw new DomainException($"Cannot delete plan {planId}: it has group plan assignments.");
        _unitOfWork.StudyPlans.DeletePlan(plan);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(Guid planId, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanWithDisciplinesAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        return plan.PlanDisciplines.Select(pd => pd.ToDto());
    }

    public async Task<PlanDisciplineDto> AddPlanDisciplineAsync(Guid planId, AddPlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        _ = await _unitOfWork.Disciplines.GetByIdAsync(dto.DisciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), dto.DisciplineId);
        if (await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct) is not null)
            throw new DomainException($"Discipline {dto.DisciplineId} is already in plan {planId}.");
        var controlType = Enum.Parse<ControlType>(dto.ControlType, ignoreCase: true);
        var pd = dto.ToEntity(planId, controlType);
        _unitOfWork.StudyPlans.AddPlanDiscipline(pd);
        await _unitOfWork.SaveChangesAsync(ct);
        return (await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct))!.ToDto();
    }

    public async Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(Guid planId, Guid disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        var pd = await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");
        var controlType = Enum.Parse<ControlType>(dto.ControlType, ignoreCase: true);
        pd.SemesterNo = dto.SemesterNo;
        pd.ControlType = controlType;
        pd.Hours = dto.Hours;
        pd.Credits = dto.Credits;
        _unitOfWork.StudyPlans.UpdatePlanDiscipline(pd);
        await _unitOfWork.SaveChangesAsync(ct);
        return (await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct))!.ToDto();
    }

    public async Task DeletePlanDisciplineAsync(Guid planId, Guid disciplineId, CancellationToken ct = default)
    {
        _ = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        var pd = await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");
        if (await _unitOfWork.StudyPlans.PlanDisciplineIsUsedAsync(planId, disciplineId, ct))
            throw new DomainException(
                $"Cannot remove discipline {disciplineId} from plan {planId}: " +
                "students already have completed, in-progress, or retake records for it.");
        await _unitOfWork.StudyPlans.RemovePlannedCourseEnrollmentsForDisciplineAsync(planId, disciplineId, ct);
        _unitOfWork.StudyPlans.DeletePlanDiscipline(pd);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<GroupPlanAssignmentDto>> GetGroupPlanHistoryAsync(Guid groupId, CancellationToken ct = default)
    {
        var assignments = await _unitOfWork.GroupPlanAssignments.GetByGroupIdAsync(groupId, ct);
        return assignments.Select(a => a.ToDto());
    }

    public async Task<GroupPlanAssignmentDto> AssignGroupPlanAsync(Guid groupId, AssignGroupPlanDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Groups.GetByIdAsync(groupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), groupId);
        var plan = await _unitOfWork.StudyPlans.GetPlanWithDisciplinesAsync(dto.PlanId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), dto.PlanId);

        if (await _unitOfWork.GroupPlanAssignments.HasOverlapAsync(groupId, dto.DateFrom, null, null, ct))
            throw new DomainException($"Group {groupId} already has an overlapping plan assignment from {dto.DateFrom}.");

        var assignment = new GroupPlanAssignment
        {
            GroupId = groupId,
            PlanId = dto.PlanId,
            DateFrom = dto.DateFrom
        };
        _unitOfWork.GroupPlanAssignments.Add(assignment);
        await _unitOfWork.SaveChangesAsync(ct);

        var activeEnrollments = await _unitOfWork.Enrollments.GetActiveByGroupIdOnDateAsync(groupId, dto.DateFrom, ct);
        foreach (var enrollment in activeEnrollments)
        {
            if (await _unitOfWork.AcademicLeaves.HasActiveLeaveOnDateAsync(enrollment.EnrollmentId, dto.DateFrom, ct))
                throw new DomainException(
                    $"Cannot modify study process while student enrollment {enrollment.EnrollmentId} is on academic leave.");

            var existingDisciplineIds = (await _unitOfWork.StudyPlans
                    .GetCourseEnrollmentsByEnrollmentIdAsync(enrollment.EnrollmentId, ct))
                .Select(ce => ce.PlanDiscipline.DisciplineId)
                .ToHashSet();

            var newCourses = GenerateCourseEnrollments(
                    enrollment.EnrollmentId, assignment.GroupPlanAssignmentId, dto.DateFrom, plan)
                .Where(ce => !existingDisciplineIds.Contains(ce.PlanDiscipline.DisciplineId))
                .ToList();

            if (newCourses.Count > 0)
                _unitOfWork.StudyPlans.AddCourseEnrollments(newCourses);
        }

        await _unitOfWork.SaveChangesAsync(ct);
        return (await _unitOfWork.GroupPlanAssignments.GetByIdAsync(assignment.GroupPlanAssignmentId, ct))!.ToDto();
    }

    public async Task<GroupPlanAssignmentDto> ChangeGroupPlanAsync(Guid groupId, ChangeGroupPlanDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Groups.GetByIdAsync(groupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), groupId);

        var newPlan = await _unitOfWork.StudyPlans.GetPlanWithDisciplinesAsync(dto.NewPlanId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), dto.NewPlanId);

        var current = (await _unitOfWork.GroupPlanAssignments.GetByGroupIdAsync(groupId, ct))
            .FirstOrDefault(a => a.DateTo == null);
        
        if (current is not null)
        {
            if (dto.NewPlanDateFrom <= current.DateFrom)
                throw new DomainException("DateFrom must be after the current assignment's start date.");
            current.DateTo = dto.NewPlanDateFrom.AddDays(-1);
            _unitOfWork.GroupPlanAssignments.Update(current);
        }

        var newAssignment = new GroupPlanAssignment
        {
            GroupId = groupId,
            PlanId = dto.NewPlanId,
            DateFrom = dto.NewPlanDateFrom
        };
        _unitOfWork.GroupPlanAssignments.Add(newAssignment);
        await _unitOfWork.SaveChangesAsync(ct);

        var activeEnrollments = await _unitOfWork.Enrollments.GetActiveByGroupIdOnDateAsync(groupId, dto.NewPlanDateFrom, ct);
        foreach (var enrollment in activeEnrollments)
        {
            if (await _unitOfWork.AcademicLeaves.HasActiveLeaveOnDateAsync(enrollment.EnrollmentId, dto.NewPlanDateFrom, ct))
                throw new DomainException(
                    $"Cannot modify study process while student enrollment {enrollment.EnrollmentId} is on academic leave.");

            var allCourses = (await _unitOfWork.StudyPlans
                .GetCourseEnrollmentsByEnrollmentIdAsync(enrollment.EnrollmentId, ct)).ToList();

            var planned = allCourses.Where(ce => ce.Status == CourseStatus.Planned).ToList();
            _unitOfWork.StudyPlans.RemoveCourseEnrollments(planned);

            var progressedDisciplineIds = allCourses
                .Where(ce => ce.Status != CourseStatus.Planned)
                .Select(ce => ce.PlanDiscipline.DisciplineId)
                .ToHashSet();

            var newCourses = GenerateCourseEnrollments(
                    enrollment.EnrollmentId, newAssignment.GroupPlanAssignmentId, dto.NewPlanDateFrom, newPlan)
                .Where(ce => !progressedDisciplineIds.Contains(ce.PlanDiscipline.DisciplineId))
                .ToList();

            if (newCourses.Count > 0)
                _unitOfWork.StudyPlans.AddCourseEnrollments(newCourses);
        }

        await _unitOfWork.SaveChangesAsync(ct);
        return (await _unitOfWork.GroupPlanAssignments.GetByIdAsync(newAssignment.GroupPlanAssignmentId, ct))!.ToDto();
    }

    internal static List<StudentCourseEnrollment> GenerateCourseEnrollments(
        Guid enrollmentId, Guid groupPlanAssignmentId, DateOnly startDate, StudyPlan plan)
    {
        return plan.PlanDisciplines
            .OrderBy(pd => pd.SemesterNo).ThenBy(pd => pd.DisciplineId)
            .Select(pd => new StudentCourseEnrollment
            {
                EnrollmentId = enrollmentId,
                GroupPlanAssignmentId = groupPlanAssignmentId,
                PlanDisciplineId = pd.PlanDisciplineId,
                PlanDiscipline = pd,
                AcademicYearStart = CalculateAcademicYearStart(startDate, pd.SemesterNo),
                Status = CourseStatus.Planned
            })
            .ToList();
    }

    private static int CalculateAcademicYearStart(DateOnly startDate, int semesterNo) =>
        startDate.Year + Math.Max(semesterNo - 1, 0) / 2;
}

