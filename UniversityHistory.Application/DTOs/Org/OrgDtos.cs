namespace UniversityHistory.Application.DTOs;

// ── Read DTOs ────────────────────────────────────────────────────────────────

public record AcademicUnitDto(
    int AcademicUnitId,
    string Name,
    string Type,
    IEnumerable<DepartmentSummaryDto> Departments
);

public record DepartmentSummaryDto(
    int DepartmentId,
    string Name
);

public record DepartmentDto(
    int DepartmentId,
    int AcademicUnitId,
    string Name,
    string AcademicUnitName,
    string AcademicUnitType
);

// ── Write DTOs ───────────────────────────────────────────────────────────────

public record CreateAcademicUnitDto(string Name, string Type);

public record UpdateAcademicUnitDto(string Name, string Type);

public record CreateDepartmentDto(int AcademicUnitId, string Name);

public record UpdateDepartmentDto(string Name);
