namespace UniversityHistory.Domain.Entities;

public class GradeRecord
{
    public Guid GradeId { get; set; }
    public Guid CourseEnrollmentId { get; set; }
    public int GradeValue { get; set; }
    public DateOnly AssessmentDate { get; set; }

    public StudentCourseEnrollment CourseEnrollment { get; set; } = null!;
}

