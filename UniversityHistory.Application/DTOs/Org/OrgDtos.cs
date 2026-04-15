namespace UniversityHistory.Application.DTOs;

public record AcademicUnitDto(
    Guid AcademicUnitId,
    string Name,
    string Type,
    IEnumerable<DepartmentSummaryDto> Departments
);

public record DepartmentSummaryDto(
    Guid DepartmentId,
    string Name
);

public record DepartmentDto(
    Guid DepartmentId,
    Guid AcademicUnitId,
    string Name,
    string AcademicUnitName,
    string AcademicUnitType
);

public record CreateAcademicUnitDto(string Name, string Type);

public record UpdateAcademicUnitDto(string Name, string Type);

public record CreateDepartmentDto(Guid AcademicUnitId, string Name);

public record UpdateDepartmentDto(string Name);

