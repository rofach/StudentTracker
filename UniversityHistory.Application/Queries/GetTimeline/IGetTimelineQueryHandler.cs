using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetTimeline;

public interface IGetTimelineQueryHandler
{
    Task<PagedResult<TimelineEventDto>> HandleAsync(GetTimelineQuery query, CancellationToken ct = default);
}
