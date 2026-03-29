using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class ExternalTransfer
{
    public int TransferId { get; set; }
    public int StudentId { get; set; }
    public int InstitutionId { get; set; }
    public TransferType TransferType { get; set; }
    public DateOnly TransferDate { get; set; }
    public string? Notes { get; set; }

    public Student Student { get; set; } = null!;
    public Institution Institution { get; set; } = null!;
}
