namespace UniversityHistory.Domain.Entities;

public class GradeRecord
{
    public int GradeId { get; set; }
    public int CourseEnrollmentId { get; set; }
    public int AttemptNo { get; set; } = 1;
    public string GradeValue { get; set; } = string.Empty;
    public DateOnly AssessmentDate { get; set; }

    public StudentCourseEnrollment CourseEnrollment { get; set; } = null!;
}
