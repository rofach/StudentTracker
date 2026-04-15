namespace UniversityHistory.Application.Queries.GetStudentSearch;

public record GetStudentSearchQuery(
    string? FullName,
    string? Email,
    string? Status,
    int Page = 1,
    int PageSize = 20);
