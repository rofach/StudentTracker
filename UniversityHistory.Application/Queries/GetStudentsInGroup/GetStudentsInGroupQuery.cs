namespace UniversityHistory.Application.Queries.GetStudentsInGroup;

public record GetStudentsInGroupQuery(Guid GroupId, DateOnly Date, int Page = 1, int PageSize = 20);

