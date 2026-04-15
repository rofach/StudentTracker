namespace UniversityHistory.Domain.Entities;

public class Subgroup
{
    public Guid SubgroupId { get; set; }
    public Guid GroupId { get; set; }
    public string SubgroupName { get; set; } = string.Empty;

    public StudyGroup Group { get; set; } = null!;
    public ICollection<StudentSubgroupAssignment> SubgroupAssignments { get; set; } = new List<StudentSubgroupAssignment>();
}

