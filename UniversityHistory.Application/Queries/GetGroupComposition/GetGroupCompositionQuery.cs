namespace UniversityHistory.Application.Queries.GetGroupComposition;

public record GetGroupCompositionQuery(Guid GroupId, DateOnly Date, int Page = 1, int PageSize = 20);

