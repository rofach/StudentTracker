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

    public StudyPlanService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<StudyPlanAssignmentDto>> GetPlanAssignmentsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var assignments = await _unitOfWork.StudyPlans.GetAssignmentsByStudentIdAsync(studentId, ct);
        return assignments.Select(static assignment => assignment.ToDto());
    }

    public async Task<StudyPlanAssignmentDto> AssignPlanAsync(int studentId, AssignPlanDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var plan = await _unitOfWork.StudyPlans.GetPlanByIdAsync(dto.PlanId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), dto.PlanId);

        var open = await _unitOfWork.StudyPlans.GetOpenAssignmentByStudentIdAsync(studentId, ct);

        if (open is not null)
        {
            if (dto.DateFrom <= open.DateFrom)
            {
                throw new DomainException("New plan DateFrom must be after the current plan's start date.");
            }

            open.DateTo = dto.DateFrom.AddDays(-1);
            await _unitOfWork.StudyPlans.UpdateAssignmentAsync(open, ct);
        }

        var assignment = dto.ToEntity(studentId);
        await _unitOfWork.StudyPlans.AddAssignmentAsync(assignment, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return assignment.ToDto(plan.SpecialtyCode, plan.PlanName);
    }

    public async Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default)
    {
        var plans = await _unitOfWork.StudyPlans.GetAllPlansAsync(ct);
        return plans.Select(static plan => plan.ToDto());
    }

    public async Task<StudyPlanDto?> GetPlanByIdAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct);
        return plan is null ? null : plan.ToDto();
    }

    public async Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = dto.ToEntity();
        await _unitOfWork.StudyPlans.AddPlanAsync(plan, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return plan.ToDto();
    }

    public async Task<StudyPlanDto> UpdatePlanAsync(int planId, UpdateStudyPlanDto dto, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        plan.SpecialtyCode = dto.SpecialtyCode;
        plan.PlanName = dto.PlanName;
        plan.ValidFrom = dto.ValidFrom;

        await _unitOfWork.StudyPlans.UpdatePlanAsync(plan, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return plan.ToDto();
    }

    public async Task DeletePlanAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        if (await _unitOfWork.StudyPlans.PlanHasAssignmentsAsync(planId, ct))
        {
            throw new DomainException($"Cannot delete plan {planId}: it has student plan assignments.");
        }

        await _unitOfWork.StudyPlans.DeletePlanAsync(plan, ct);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(int planId, CancellationToken ct = default)
    {
        var plan = await _unitOfWork.StudyPlans.GetPlanWithDisciplinesAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        return plan.PlanDisciplines.Select(static planDiscipline => planDiscipline.ToDto());
    }

    public async Task<PlanDisciplineDto> AddPlanDisciplineAsync(int planId, AddPlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        _ = await _unitOfWork.Disciplines.GetByIdAsync(dto.DisciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), dto.DisciplineId);

        if (await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct) is not null)
        {
            throw new DomainException($"Discipline {dto.DisciplineId} is already in plan {planId}.");
        }

        var controlType = Enum.Parse<ControlType>(dto.ControlType, ignoreCase: true);

        var planDiscipline = dto.ToEntity(planId, controlType);
        await _unitOfWork.StudyPlans.AddPlanDisciplineAsync(planDiscipline, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return (await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, dto.DisciplineId, ct))!.ToDto();
    }

    public async Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(int planId, int disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        var planDiscipline = await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");

        var controlType = Enum.Parse<ControlType>(dto.ControlType, ignoreCase: true);

        planDiscipline.SemesterNo = dto.SemesterNo;
        planDiscipline.ControlType = controlType;
        planDiscipline.Hours = dto.Hours;
        planDiscipline.Credits = dto.Credits;

        await _unitOfWork.StudyPlans.UpdatePlanDisciplineAsync(planDiscipline, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return (await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct))!.ToDto();
    }

    public async Task DeletePlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default)
    {
        _ = await _unitOfWork.StudyPlans.GetPlanByIdAsync(planId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), planId);

        var planDiscipline = await _unitOfWork.StudyPlans.GetPlanDisciplineAsync(planId, disciplineId, ct)
            ?? throw new NotFoundException("PlanDiscipline", $"plan={planId}, discipline={disciplineId}");

        if (await _unitOfWork.StudyPlans.PlanDisciplineIsUsedAsync(planId, disciplineId, ct))
        {
            throw new DomainException($"Cannot remove discipline {disciplineId} from plan {planId}: students have course enrollments for it.");
        }

        await _unitOfWork.StudyPlans.DeletePlanDisciplineAsync(planDiscipline, ct);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}
