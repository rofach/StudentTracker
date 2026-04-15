namespace UniversityHistory.Domain.Entities;

public class Department
{
    public Guid DepartmentId { get; set; }
    public Guid AcademicUnitId { get; set; }
    public string Name { get; set; } = string.Empty;

    public AcademicUnit AcademicUnit { get; set; } = null!;
    public ICollection<StudyGroup> Groups { get; set; } = new List<StudyGroup>();
}

