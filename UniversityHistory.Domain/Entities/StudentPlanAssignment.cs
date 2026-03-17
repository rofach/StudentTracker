namespace UniversityHistory.Domain.Entities;

public class StudentPlanAssignment
{
    public int AssignmentId { get; set; }
    public int StudentId { get; set; }
    public int PlanId { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }

    public Student Student { get; set; } = null!;
    public StudyPlan Plan { get; set; } = null!;
    public ICollection<StudentCourseEnrollment> CourseEnrollments { get; set; } = new List<StudentCourseEnrollment>();
}
