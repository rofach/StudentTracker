using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class StudyPlanService : IStudyPlanService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IStudyPlanRepository _planRepo;
    private readonly IDisciplineRepository _disciplineRepo;

    public StudyPlanService(
        IStudentRepository studentRepo,
        IStudyPlanRepository planRepo,
        IDisciplineRepository disciplineRepo)
    {
        _studentRepo    = studentRepo;
        _planRepo       = planRepo;
        _disciplineRepo = disciplineRepo;
    }

    // ── Student Plan Assignments ──────────────────────────────────────────────

    public async Task<IEnumerable<StudyPlanAssignmentDto>> GetPlanAssignmentsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var assignments = await _planRepo.GetAssignmentsByStudentIdAsync(studentId, ct);
        return assignments.Select(a => new StudyPlanAssignmentDto(
            a.AssignmentId, a.Plan.SpecialtyCode, a.Plan.PlanName, a.DateFrom, a.DateTo));
    }

    public async Task<StudyPlanAssignmentDto> AssignPlanAsync(int studentId, AssignPlanDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var plan = await _planRepo.GetPlanByIdAsync(dto.PlanId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), dto.PlanId);

        var open = await _planRepo.GetOpenAssignmentByStudentIdAsync(studentId, ct);
        if (open is not null)
        {
            if (dto.DateFrom <= open.DateFrom)
                throw new DomainException("New plan DateFrom must be after the current plan's start date.");

            open.DateTo = dto.DateFrom.AddDays(-1);
            await _planRepo.UpdateAssignmentAsync(open, ct);
        }

        var assignment = new StudentPlanAssignment
        {
            StudentId = studentId,
            PlanId    = dto.PlanId,
            DateFrom  = dto.DateFrom
        };

        await _planRepo.AddAssignmentAsync(assignment, ct);
        return new StudyPlanAssignmentDto(assignment.AssignmentId, plan.SpecialtyCode, plan.PlanName, assignment.DateFrom, null);
    }

    // ── Plan CRUD ─────────────────────────────────────────────────────────────

    public async Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default)
    {
        var plans = await _planRepo.GetAllPlansAsync(ct);
        return plans.Select(MapPlan);
    }

    public async Task<StudyPlanDto?> GetPlanByIdAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanByIdAsync(planId, ct);
        return plan is null ? null : MapPlan(plan);
    }

    public async Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = new StudyPlan
        {
            SpecialtyCode = dto.SpecialtyCode,
            PlanName      = dto.PlanName,
            ValidFrom     = dto.ValidFrom
        };
        return MapPlan(await _planRepo.AddPlanAsync(plan, ct));
    }

    public async Task<StudyPlanDto> UpdatePlanAsync(int planId, UpdateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        plan.SpecialtyCode = dto.SpecialtyCode;
        plan.PlanName      = dto.PlanName;
        plan.ValidFrom     = dto.ValidFrom;

        await _planRepo.UpdatePlanAsync(plan, ct);
        return MapPlan(plan);
    }

    public async Task DeletePlanAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        if (await _planRepo.PlanHasAssignmentsAsync(planId, ct))
            throw new DomainException($"Cannot delete plan {planId}: it has student plan assignments.");

        await _planRepo.DeletePlanAsync(plan, ct);
    }

    // ── Plan Disciplines ──────────────────────────────────────────────────────

    public async Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanWithDisciplinesAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        return plan.PlanDisciplines.Select(MapPlanDiscipline);
    }

    public async Task<PlanDisciplineDto> AddPlanDisciplineAsync(int planId, AddPlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        _ = await _disciplineRepo.GetByIdAsync(dto.DisciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), dto.DisciplineId);

        if (await _planRepo.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct) is not null)
            throw new DomainException($"Discipline {dto.DisciplineId} is already in plan {planId}.");

        var controlType = ParseControlType(dto.ControlType);
        ValidatePlanDisciplineValues(dto.SemesterNo, dto.Hours, dto.Credits);

        var pd = new PlanDiscipline
        {
            PlanId       = planId,
            DisciplineId = dto.DisciplineId,
            SemesterNo   = dto.SemesterNo,
            ControlType  = controlType,
            Hours        = dto.Hours,
            Credits      = dto.Credits
        };

        await _planRepo.AddPlanDisciplineAsync(pd, ct);
        return MapPlanDiscipline((await _planRepo.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct))!);
    }

    public async Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(int planId, int disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        var pd = await _planRepo.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");

        var controlType = ParseControlType(dto.ControlType);
        ValidatePlanDisciplineValues(dto.SemesterNo, dto.Hours, dto.Credits);

        pd.SemesterNo  = dto.SemesterNo;
        pd.ControlType = controlType;
        pd.Hours       = dto.Hours;
        pd.Credits     = dto.Credits;

        await _planRepo.UpdatePlanDisciplineAsync(pd, ct);
        return MapPlanDiscipline((await _planRepo.GetPlanDisciplineAsync(planId, disciplineId, ct))!);
    }

    public async Task DeletePlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default)
    {
        _ = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        var pd = await _planRepo.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");

        if (await _planRepo.PlanDisciplineIsUsedAsync(planId, disciplineId, ct))
            throw new DomainException($"Cannot remove discipline {disciplineId} from plan {planId}: students have course enrollments for it.");

        await _planRepo.DeletePlanDisciplineAsync(pd, ct);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static ControlType ParseControlType(string value)
    {
        if (!Enum.TryParse<ControlType>(value, ignoreCase: true, out var result))
            throw new DomainException($"Unknown control type '{value}'. Valid: Exam, Credit, Coursework.");
        return result;
    }

    private static void ValidatePlanDisciplineValues(int semesterNo, int hours, decimal credits)
    {
        if (semesterNo <= 0) throw new DomainException("SemesterNo must be greater than zero.");
        if (hours <= 0)      throw new DomainException("Hours must be greater than zero.");
        if (credits <= 0)    throw new DomainException("Credits must be greater than zero.");
    }

    private static StudyPlanDto MapPlan(StudyPlan p) =>
        new(p.PlanId, p.SpecialtyCode, p.PlanName, p.ValidFrom);

    private static PlanDisciplineDto MapPlanDiscipline(PlanDiscipline pd) =>
        new(pd.PlanId, pd.DisciplineId, pd.Discipline.DisciplineName,
            pd.SemesterNo, pd.ControlType.ToString(), pd.Hours, pd.Credits);
}
