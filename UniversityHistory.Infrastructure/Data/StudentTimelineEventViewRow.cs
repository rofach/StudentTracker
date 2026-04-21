namespace UniversityHistory.Infrastructure.Data;

public class StudentTimelineEventViewRow
{
    public Guid StudentId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public string? GroupCode { get; set; }
    public string? DepartmentName { get; set; }
    public string? AcademicUnitName { get; set; }
    public string? AcademicUnitType { get; set; }
    public int SortPriority { get; set; }
    public string EventKey { get; set; } = string.Empty;
}
