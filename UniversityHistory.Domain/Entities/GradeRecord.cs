namespace UniversityHistory.Domain.Entities;

public class GradeRecord
{
    public Guid GradeId { get; set; }
    public Guid CourseEnrollmentId { get; set; }
    public string GradeValue { get; set; } = string.Empty;
    public DateOnly AssessmentDate { get; set; }

    public StudentCourseEnrollment CourseEnrollment { get; set; } = null!;
}

