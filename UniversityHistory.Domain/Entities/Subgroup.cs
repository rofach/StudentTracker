namespace UniversityHistory.Domain.Entities;

public class Subgroup
{
    public int SubgroupId { get; set; }
    public int GroupId { get; set; }
    public string SubgroupName { get; set; } = string.Empty;

    public StudyGroup Group { get; set; } = null!;
    public ICollection<StudentGroupEnrollment> Enrollments { get; set; } = new List<StudentGroupEnrollment>();
}
