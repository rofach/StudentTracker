namespace UniversityHistory.Domain.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public int AcademicUnitId { get; set; }
    public string Name { get; set; } = string.Empty;

    public AcademicUnit AcademicUnit { get; set; } = null!;
    public ICollection<StudyGroup> Groups { get; set; } = new List<StudyGroup>();
}
