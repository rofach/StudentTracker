namespace UniversityHistory.Domain.Entities;

public class AcademicLeave
{
    public int LeaveId { get; set; }
    public int StudentId { get; set; }
    public int EnrollmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Reason { get; set; }

    public Student Student { get; set; } = null!;
    public StudentGroupEnrollment Enrollment { get; set; } = null!;
}
