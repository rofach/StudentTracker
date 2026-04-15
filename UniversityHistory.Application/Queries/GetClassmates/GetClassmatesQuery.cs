namespace UniversityHistory.Application.Queries.GetClassmates;

public record GetClassmatesQuery(Guid StudentId, DateOnly? DateFrom, DateOnly? DateTo);

