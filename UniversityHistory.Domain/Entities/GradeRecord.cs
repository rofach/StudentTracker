namespace UniversityHistory.Domain.Entities;

public class GradeRecord
{
    public int GradeId { get; set; }
    public int CourseEnrollmentId { get; set; }
    public string GradeValue { get; set; } = string.Empty;
    public DateOnly AssessmentDate { get; set; }

    public StudentCourseEnrollment CourseEnrollment { get; set; } = null!;
}
