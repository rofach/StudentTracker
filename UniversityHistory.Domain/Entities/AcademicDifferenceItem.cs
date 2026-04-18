using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class AcademicDifferenceItem
{
    public Guid DifferenceItemId { get; set; }
    public Guid TransferId { get; set; }
    public Guid PlanDisciplineId { get; set; }
    public DifferenceItemStatus Status { get; set; } = DifferenceItemStatus.Pending;
    public string? Notes { get; set; }

    public StudentGroupTransfer Transfer { get; set; } = null!;
    public PlanDiscipline PlanDiscipline { get; set; } = null!;
}
