namespace UniversityHistory.Application.Queries.GetDisciplineSearch;

public record GetDisciplineSearchQuery(
    string? Name,
    int Page,
    int PageSize
);
