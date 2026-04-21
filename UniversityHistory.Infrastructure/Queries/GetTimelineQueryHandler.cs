using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetTimeline;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetTimelineQueryHandler : IGetTimelineQueryHandler
{
    private readonly UniversityDbContext _db;

    public GetTimelineQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<PagedResult<TimelineEventDto>> HandleAsync(
        GetTimelineQuery query,
        CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(s => s.StudentId == query.StudentId, ct);
        if (!exists)
            throw new NotFoundException("Student", query.StudentId);

        var baseQuery = _db.StudentTimelineEvents
            .AsNoTracking()
            .Where(x => x.StudentId == query.StudentId);

        var count = await baseQuery.CountAsync(ct);

        var items = await baseQuery
            .OrderBy(x => x.DateFrom)
            .ThenBy(x => x.SortPriority)
            .ThenBy(x => x.EventKey)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(x => new TimelineEventDto(
                x.EventType,
                x.Description,
                x.DateFrom,
                x.DateTo,
                x.GroupCode,
                x.DepartmentName,
                x.AcademicUnitName,
                x.AcademicUnitType))
            .ToListAsync(ct);

        return new PagedResult<TimelineEventDto>(
            items,
            query.Page,
            query.PageSize,
            count);
    }
}
