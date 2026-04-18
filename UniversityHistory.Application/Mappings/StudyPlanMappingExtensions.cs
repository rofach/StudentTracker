using UniversityHistory.Application.DTOs;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Application.Mappings;

public static class StudyPlanMappingExtensions
{
    public static StudyPlan ToEntity(this CreateStudyPlanDto dto) =>
        new() { SpecialtyCode = dto.SpecialtyCode, PlanName = dto.PlanName, ValidFrom = dto.ValidFrom };

    public static StudyPlanDto ToDto(this StudyPlan plan) =>
        new(plan.PlanId, plan.SpecialtyCode, plan.PlanName, plan.ValidFrom);

    public static PlanDiscipline ToEntity(this AddPlanDisciplineDto dto, Guid planId, Domain.Enums.ControlType controlType) =>
        new()
        {
            PlanDisciplineId = Guid.NewGuid(),
            PlanId = planId,
            DisciplineId = dto.DisciplineId,
            SemesterNo = dto.SemesterNo,
            ControlType = controlType,
            Hours = dto.Hours,
            Credits = dto.Credits
        };

    public static PlanDisciplineDto ToDto(this PlanDiscipline pd) =>
        new(pd.PlanId, pd.DisciplineId, pd.Discipline.DisciplineName,
            pd.SemesterNo, pd.ControlType.ToString(), pd.Hours, pd.Credits);

    public static GroupPlanAssignmentDto ToDto(this GroupPlanAssignment a) =>
        new(a.GroupPlanAssignmentId, a.GroupId, a.PlanId,
            a.Plan.SpecialtyCode, a.Plan.PlanName, a.DateFrom, a.DateTo);
}

