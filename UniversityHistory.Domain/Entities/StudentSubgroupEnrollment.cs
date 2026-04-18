namespace UniversityHistory.Domain.Entities;

public class StudentSubgroupEnrollment
{
    public Guid SubgroupEnrollmentId { get; set; }
    public Guid EnrollmentId { get; set; }
    public Guid SubgroupId { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public string Reason { get; set; } = string.Empty;

    public StudentGroupEnrollment Enrollment { get; set; } = null!;
    public Subgroup Subgroup { get; set; } = null!;
}
