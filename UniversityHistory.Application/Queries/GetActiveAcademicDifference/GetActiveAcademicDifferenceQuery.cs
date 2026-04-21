namespace UniversityHistory.Application.Queries.GetActiveAcademicDifference;

public record GetActiveAcademicDifferenceQuery(
    string? StudentName,
    string? DisciplineName,
    string? Status,
    DateOnly? DateFrom,
    DateOnly? DateTo,
    int Page,
    int PageSize
);
