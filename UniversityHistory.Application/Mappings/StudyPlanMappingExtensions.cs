using UniversityHistory.Application.DTOs;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Application.Mappings;

public static class StudyPlanMappingExtensions
{
    public static StudentPlanAssignment ToEntity(this AssignPlanDto dto, int studentId)
    {
        return new StudentPlanAssignment
        {
            StudentId = studentId,
            PlanId = dto.PlanId,
            DateFrom = dto.DateFrom
        };
    }

    public static StudyPlan ToEntity(this CreateStudyPlanDto dto)
    {
        return new StudyPlan
        {
            SpecialtyCode = dto.SpecialtyCode,
            PlanName = dto.PlanName,
            ValidFrom = dto.ValidFrom
        };
    }

    public static PlanDiscipline ToEntity(this AddPlanDisciplineDto dto, int planId, Domain.Enums.ControlType controlType)
    {
        return new PlanDiscipline
        {
            PlanId = planId,
            DisciplineId = dto.DisciplineId,
            SemesterNo = dto.SemesterNo,
            ControlType = controlType,
            Hours = dto.Hours,
            Credits = dto.Credits
        };
    }

    public static StudyPlanAssignmentDto ToDto(this StudentPlanAssignment assignment)
    {
        return new StudyPlanAssignmentDto(
            assignment.AssignmentId,
            assignment.Plan.SpecialtyCode,
            assignment.Plan.PlanName,
            assignment.DateFrom,
            assignment.DateTo);
    }

    public static StudyPlanAssignmentDto ToDto(
        this StudentPlanAssignment assignment,
        string specialtyCode,
        string? planName)
    {
        return new StudyPlanAssignmentDto(
            assignment.AssignmentId,
            specialtyCode,
            planName,
            assignment.DateFrom,
            assignment.DateTo);
    }

    public static StudyPlanDto ToDto(this StudyPlan plan)
    {
        return new StudyPlanDto(plan.PlanId, plan.SpecialtyCode, plan.PlanName, plan.ValidFrom);
    }

    public static PlanDisciplineDto ToDto(this PlanDiscipline planDiscipline)
    {
        return new PlanDisciplineDto(
            planDiscipline.PlanId,
            planDiscipline.DisciplineId,
            planDiscipline.Discipline.DisciplineName,
            planDiscipline.SemesterNo,
            planDiscipline.ControlType.ToString(),
            planDiscipline.Hours,
            planDiscipline.Credits);
    }
}
