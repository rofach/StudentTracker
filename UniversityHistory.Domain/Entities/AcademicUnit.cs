using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class AcademicUnit
{
    public Guid AcademicUnitId { get; set; }
    public string Name { get; set; } = string.Empty;
    public AcademicUnitType Type { get; set; }

    public ICollection<Department> Departments { get; set; } = new List<Department>();
}

