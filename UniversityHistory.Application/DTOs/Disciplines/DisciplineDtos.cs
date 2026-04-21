namespace UniversityHistory.Application.DTOs;

public record CreateDisciplineDto(string DisciplineName, string? Description);

public record UpdateDisciplineDto(string DisciplineName, string? Description);

public record DisciplineDto(
    Guid DisciplineId,
    string DisciplineName,
    string? Description
);

