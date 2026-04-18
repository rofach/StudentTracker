namespace UniversityHistory.Domain.Entities;

public class StudentGroupTransfer
{
    public Guid TransferId { get; set; }
    public Guid OldEnrollmentId { get; set; }
    public Guid NewEnrollmentId { get; set; }
    public DateOnly TransferDate { get; set; }
    public string Reason { get; set; } = string.Empty;

    public StudentGroupEnrollment OldEnrollment { get; set; } = null!;
    public StudentGroupEnrollment NewEnrollment { get; set; } = null!;
    public ICollection<AcademicDifferenceItem> DifferenceItems { get; set; } = new List<AcademicDifferenceItem>();
}
