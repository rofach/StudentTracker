namespace UniversityHistory.Domain.Entities;

public class GroupPlanAssignment
{
    public Guid GroupPlanAssignmentId { get; set; }
    public Guid GroupId { get; set; }
    public Guid PlanId { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }

    public StudyGroup Group { get; set; } = null!;
    public StudyPlan Plan { get; set; } = null!;
    public ICollection<StudentCourseEnrollment> StudentCourseEnrollments { get; set; } = new List<StudentCourseEnrollment>();
}

