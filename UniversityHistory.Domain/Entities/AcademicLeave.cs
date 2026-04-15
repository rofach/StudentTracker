namespace UniversityHistory.Domain.Entities;

public class AcademicLeave
{
    public Guid LeaveId { get; set; }
    public Guid EnrollmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Reason { get; set; }
    public string? ReturnReason { get; set; }

    public StudentGroupEnrollment Enrollment { get; set; } = null!;
}

