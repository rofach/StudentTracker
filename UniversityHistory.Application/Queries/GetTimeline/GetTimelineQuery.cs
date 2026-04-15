namespace UniversityHistory.Application.Queries.GetTimeline;

public record GetTimelineQuery(Guid StudentId, int Page = 1, int PageSize = 20);

