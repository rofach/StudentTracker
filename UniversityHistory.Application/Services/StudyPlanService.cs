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
    private readonly IStudentRepository _studentRepo;
    private readonly IStudyPlanRepository _planRepo;
    private readonly IDisciplineRepository _disciplineRepo;

    public StudyPlanService(
        IStudentRepository studentRepo,
        IStudyPlanRepository planRepo,
        IDisciplineRepository disciplineRepo)
    {
        _studentRepo = studentRepo;
        _planRepo = planRepo;
        _disciplineRepo = disciplineRepo;
    }

    public async Task<IEnumerable<StudyPlanAssignmentDto>> GetPlanAssignmentsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var assignments = await _planRepo.GetAssignmentsByStudentIdAsync(studentId, ct);
        return assignments.Select(static assignment => assignment.ToDto());
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
            {
                throw new DomainException("New plan DateFrom must be after the current plan's start date.");
            }

            open.DateTo = dto.DateFrom.AddDays(-1);
            await _planRepo.UpdateAssignmentAsync(open, ct);
        }

        var assignment = dto.ToEntity(studentId);
        await _planRepo.AddAssignmentAsync(assignment, ct);
        return assignment.ToDto(plan.SpecialtyCode, plan.PlanName);
    }

    public async Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default)
    {
        var plans = await _planRepo.GetAllPlansAsync(ct);
        return plans.Select(static plan => plan.ToDto());
    }

    public async Task<StudyPlanDto?> GetPlanByIdAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanByIdAsync(planId, ct);
        return plan is null ? null : plan.ToDto();
    }

    public async Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = dto.ToEntity();
        return (await _planRepo.AddPlanAsync(plan, ct)).ToDto();
    }

    public async Task<StudyPlanDto> UpdatePlanAsync(int planId, UpdateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        plan.SpecialtyCode = dto.SpecialtyCode;
        plan.PlanName = dto.PlanName;
        plan.ValidFrom = dto.ValidFrom;

        await _planRepo.UpdatePlanAsync(plan, ct);
        return plan.ToDto();
    }

    public async Task DeletePlanAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        if (await _planRepo.PlanHasAssignmentsAsync(planId, ct))
        {
            throw new DomainException($"Cannot delete plan {planId}: it has student plan assignments.");
        }

        await _planRepo.DeletePlanAsync(plan, ct);
    }

    public async Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _planRepo.GetPlanWithDisciplinesAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        return plan.PlanDisciplines.Select(static planDiscipline => planDiscipline.ToDto());
    }

    public async Task<PlanDisciplineDto> AddPlanDisciplineAsync(int planId, AddPlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        _ = await _disciplineRepo.GetByIdAsync(dto.DisciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), dto.DisciplineId);

        if (await _planRepo.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct) is not null)
        {
            throw new DomainException($"Discipline {dto.DisciplineId} is already in plan {planId}.");
        }

        var controlType = ParseControlType(dto.ControlType);
        ValidatePlanDisciplineValues(dto.SemesterNo, dto.Hours, dto.Credits);

        var planDiscipline = dto.ToEntity(planId, controlType);
        await _planRepo.AddPlanDisciplineAsync(planDiscipline, ct);
        return (await _planRepo.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct))!.ToDto();
    }

    public async Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(int planId, int disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        var planDiscipline = await _planRepo.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");

        var controlType = ParseControlType(dto.ControlType);
        ValidatePlanDisciplineValues(dto.SemesterNo, dto.Hours, dto.Credits);

        planDiscipline.SemesterNo = dto.SemesterNo;
        planDiscipline.ControlType = controlType;
        planDiscipline.Hours = dto.Hours;
        planDiscipline.Credits = dto.Credits;

        await _planRepo.UpdatePlanDisciplineAsync(planDiscipline, ct);
        return (await _planRepo.GetPlanDisciplineAsync(planId, disciplineId, ct))!.ToDto();
    }

    public async Task DeletePlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default)
    {
        _ = await _planRepo.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        var planDiscipline = await _planRepo.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");

        if (await _planRepo.PlanDisciplineIsUsedAsync(planId, disciplineId, ct))
        {
            throw new DomainException($"Cannot remove discipline {disciplineId} from plan {planId}: students have course enrollments for it.");
        }

        await _planRepo.DeletePlanDisciplineAsync(planDiscipline, ct);
    }

    private static ControlType ParseControlType(string value)
    {
        if (!Enum.TryParse<ControlType>(value, ignoreCase: true, out var result))
        {
            throw new DomainException($"Unknown control type '{value}'. Valid: Exam, Credit, Coursework.");
        }

        return result;
    }

    private static void ValidatePlanDisciplineValues(int semesterNo, int hours, decimal credits)
    {
        if (semesterNo <= 0)
        {
            throw new DomainException("SemesterNo must be greater than zero.");
        }

        if (hours <= 0)
        {
            throw new DomainException("Hours must be greater than zero.");
        }

        if (credits <= 0)
        {
            throw new DomainException("Credits must be greater than zero.");
        }
    }
}
