using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class ExternalTransfer
{
    public Guid TransferId { get; set; }
    public Guid StudentId { get; set; }
    public Guid InstitutionId { get; set; }
    public TransferType TransferType { get; set; }
    public DateOnly TransferDate { get; set; }
    public string? Notes { get; set; }

    public Student Student { get; set; } = null!;
    public Institution Institution { get; set; } = null!;
}

