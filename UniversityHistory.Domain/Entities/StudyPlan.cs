namespace UniversityHistory.Domain.Entities;

public class StudyPlan
{
    public int PlanId { get; set; }
    public string SpecialtyCode { get; set; } = string.Empty;
    public string? PlanName { get; set; }
    public DateOnly ValidFrom { get; set; }

    public ICollection<PlanDiscipline> PlanDisciplines { get; set; } = new List<PlanDiscipline>();
    public ICollection<GroupPlanAssignment> GroupPlanAssignments { get; set; } = new List<GroupPlanAssignment>();
}
