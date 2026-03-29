namespace UniversityHistory.Application.Queries.GetTimeline;

public record GetTimelineQuery(int StudentId, int Page = 1, int PageSize = 20);
