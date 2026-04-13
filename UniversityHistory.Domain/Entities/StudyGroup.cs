namespace UniversityHistory.Domain.Entities;

public class StudyGroup
{
    public int GroupId { get; set; }
    public string GroupCode { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public DateOnly DateCreated { get; set; }
    public DateOnly? DateClosed { get; set; }

    public Department Department { get; set; } = null!;
    public ICollection<Subgroup> Subgroups { get; set; } = new List<Subgroup>();
    public ICollection<StudentGroupEnrollment> Enrollments { get; set; } = new List<StudentGroupEnrollment>();
    public ICollection<GroupPlanAssignment> PlanAssignments { get; set; } = new List<GroupPlanAssignment>();
}
