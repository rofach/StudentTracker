namespace UniversityHistory.Application.DTOs;

public record CreateDisciplineDto(string DisciplineName);

public record UpdateDisciplineDto(string DisciplineName);

public record DisciplineDto(
    Guid DisciplineId,
    string DisciplineName
);

