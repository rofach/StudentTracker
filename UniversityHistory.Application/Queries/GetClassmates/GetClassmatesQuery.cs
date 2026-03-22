namespace UniversityHistory.Application.Queries.GetClassmates;

public record GetClassmatesQuery(int StudentId, DateOnly? DateFrom, DateOnly? DateTo);
