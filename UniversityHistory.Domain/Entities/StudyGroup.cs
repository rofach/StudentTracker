namespace UniversityHistory.Domain.Entities;

public class StudyGroup
{
    public int GroupId { get; set; }
    public string GroupCode { get; set; } = string.Empty;
    public int CreationYear { get; set; }
    public string? Faculty { get; set; }

    public ICollection<Subgroup> Subgroups { get; set; } = new List<Subgroup>();
    public ICollection<StudentGroupEnrollment> Enrollments { get; set; } = new List<StudentGroupEnrollment>();
}
