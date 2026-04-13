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
    private readonly IUnitOfWork _uow;
    public StudyPlanService(IUnitOfWork uow) => _uow = uow;

    // ── Study plan CRUD ────────────────────────────────────────────────────────

    public async Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default)
    {
        var plans = await _uow.StudyPlans.GetAllPlansAsync(ct);
        return plans.Select(p => p.ToDto());
    }

    public async Task<StudyPlanDto?> GetPlanByIdAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _uow.StudyPlans.GetPlanByIdAsync(planId, ct);
        return plan is null ? null : plan.ToDto();
    }

    public async Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = dto.ToEntity();
        _uow.StudyPlans.AddPlan(plan);
        await _uow.SaveChangesAsync(ct);
        return plan.ToDto();
    }

    public async Task<StudyPlanDto> UpdatePlanAsync(int planId, UpdateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = await _uow.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        plan.SpecialtyCode = dto.SpecialtyCode;
        plan.PlanName = dto.PlanName;
        plan.ValidFrom = dto.ValidFrom;
        _uow.StudyPlans.UpdatePlan(plan);
        await _uow.SaveChangesAsync(ct);
        return plan.ToDto();
    }

    public async Task DeletePlanAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _uow.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        if (await _uow.StudyPlans.PlanHasAssignmentsAsync(planId, ct))
            throw new DomainException($"Cannot delete plan {planId}: it has group plan assignments.");
        _uow.StudyPlans.DeletePlan(plan);
        await _uow.SaveChangesAsync(ct);
    }

    // ── Plan disciplines ───────────────────────────────────────────────────────

    public async Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _uow.StudyPlans.GetPlanWithDisciplinesAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        return plan.PlanDisciplines.Select(pd => pd.ToDto());
    }

    public async Task<PlanDisciplineDto> AddPlanDisciplineAsync(int planId, AddPlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _uow.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        _ = await _uow.Disciplines.GetByIdAsync(dto.DisciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), dto.DisciplineId);
        if (await _uow.StudyPlans.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct) is not null)
            throw new DomainException($"Discipline {dto.DisciplineId} is already in plan {planId}.");
        var controlType = Enum.Parse<ControlType>(dto.ControlType, ignoreCase: true);
        var pd = dto.ToEntity(planId, controlType);
        _uow.StudyPlans.AddPlanDiscipline(pd);
        await _uow.SaveChangesAsync(ct);
        return (await _uow.StudyPlans.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct))!.ToDto();
    }

    public async Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(int planId, int disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _uow.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        var pd = await _uow.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");
        var controlType = Enum.Parse<ControlType>(dto.ControlType, ignoreCase: true);
        pd.SemesterNo = dto.SemesterNo;
        pd.ControlType = controlType;
        pd.Hours = dto.Hours;
        pd.Credits = dto.Credits;
        _uow.StudyPlans.UpdatePlanDiscipline(pd);
        await _uow.SaveChangesAsync(ct);
        return (await _uow.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct))!.ToDto();
    }

    public async Task DeletePlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default)
    {
        _ = await _uow.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);
        var pd = await _uow.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");
        if (await _uow.StudyPlans.PlanDisciplineIsUsedAsync(planId, disciplineId, ct))
            throw new DomainException($"Cannot remove discipline {disciplineId} from plan {planId}: students have non-planned course enrollments for it.");
        _uow.StudyPlans.DeletePlanDiscipline(pd);
        await _uow.SaveChangesAsync(ct);
    }

    // ── Group plan assignments ─────────────────────────────────────────────────

    public async Task<IEnumerable<GroupPlanAssignmentDto>> GetGroupPlanHistoryAsync(int groupId, CancellationToken ct = default)
    {
        var assignments = await _uow.GroupPlanAssignments.GetByGroupIdAsync(groupId, ct);
        return assignments.Select(a => a.ToDto());
    }

    public async Task<GroupPlanAssignmentDto> AssignGroupPlanAsync(int groupId, AssignGroupPlanDto dto, CancellationToken ct = default)
    {
        _ = await _uow.Groups.GetByIdAsync(groupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), groupId);
        _ = await _uow.StudyPlans.GetPlanWithDisciplinesAsync(dto.PlanId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), dto.PlanId);

        if (await _uow.GroupPlanAssignments.HasOverlapAsync(groupId, dto.DateFrom, null, null, ct))
            throw new DomainException($"Group {groupId} already has an overlapping plan assignment from {dto.DateFrom}.");

        var assignment = new GroupPlanAssignment
        {
            GroupId = groupId,
            PlanId = dto.PlanId,
            DateFrom = dto.DateFrom
        };
        _uow.GroupPlanAssignments.Add(assignment);
        await _uow.SaveChangesAsync(ct);

        return (await _uow.GroupPlanAssignments.GetByIdAsync(assignment.GroupPlanAssignmentId, ct))!.ToDto();
    }

    public async Task<GroupPlanAssignmentDto> ChangeGroupPlanAsync(int groupId, ChangeGroupPlanDto dto, CancellationToken ct = default)
    {
        _ = await _uow.Groups.GetByIdAsync(groupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), groupId);

        var newPlan = await _uow.StudyPlans.GetPlanWithDisciplinesAsync(dto.NewPlanId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), dto.NewPlanId);

        // Close the currently open assignment
        var current = (await _uow.GroupPlanAssignments.GetByGroupIdAsync(groupId, ct))
            .FirstOrDefault(a => a.DateTo == null);
        if (current is not null)
        {
            if (dto.EffectiveFrom <= current.DateFrom)
                throw new DomainException("EffectiveFrom must be after the current assignment's start date.");
            current.DateTo = dto.EffectiveFrom.AddDays(-1);
            _uow.GroupPlanAssignments.Update(current);
        }

        // Create new assignment
        var newAssignment = new GroupPlanAssignment
        {
            GroupId = groupId,
            PlanId = dto.NewPlanId,
            DateFrom = dto.EffectiveFrom
        };
        _uow.GroupPlanAssignments.Add(newAssignment);
        await _uow.SaveChangesAsync(ct);

        // Propagate: update Planned SCEs for active students in this group
        var activeEnrollments = await _uow.Enrollments.GetActiveByGroupIdAsync(groupId, ct);
        foreach (var enrollment in activeEnrollments)
        {
            var planned = (await _uow.StudyPlans.GetCourseEnrollmentsByEnrollmentIdAsync(enrollment.EnrollmentId, ct))
                .Where(ce => ce.Status == CourseStatus.Planned)
                .ToList();
            _uow.StudyPlans.RemoveCourseEnrollments(planned);

            var newCourses = GenerateCourseEnrollments(enrollment.EnrollmentId, newAssignment.GroupPlanAssignmentId, dto.EffectiveFrom, newPlan);
            _uow.StudyPlans.AddCourseEnrollments(newCourses);
        }

        await _uow.SaveChangesAsync(ct);
        return (await _uow.GroupPlanAssignments.GetByIdAsync(newAssignment.GroupPlanAssignmentId, ct))!.ToDto();
    }

    internal static List<StudentCourseEnrollment> GenerateCourseEnrollments(
        int enrollmentId, int groupPlanAssignmentId, DateOnly startDate, StudyPlan plan)
    {
        return plan.PlanDisciplines
            .OrderBy(pd => pd.SemesterNo).ThenBy(pd => pd.DisciplineId)
            .Select(pd => new StudentCourseEnrollment
            {
                EnrollmentId = enrollmentId,
                GroupPlanAssignmentId = groupPlanAssignmentId,
                DisciplineId = pd.DisciplineId,
                AcademicYearStart = CalculateAcademicYearStart(startDate, pd.SemesterNo),
                Status = CourseStatus.Planned
            })
            .ToList();
    }

    private static int CalculateAcademicYearStart(DateOnly startDate, int semesterNo) =>
        startDate.Year + Math.Max(semesterNo - 1, 0) / 2;
}
