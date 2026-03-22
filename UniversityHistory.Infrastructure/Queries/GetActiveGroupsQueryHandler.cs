using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetActiveGroups;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetActiveGroupsQueryHandler : IGetActiveGroupsQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetActiveGroupsQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<IEnumerable<ActiveGroupDto>> HandleAsync(
        GetActiveGroupsQuery query, CancellationToken ct = default)
    {
        var date = query.Date;

        return await _db.Database.SqlQuery<ActiveGroupDto>($"""
            SELECT
                group_id    AS GroupId,
                group_code  AS GroupCode,
                faculty     AS Faculty,
                date_created AS DateCreated,
                date_closed  AS DateClosed
            FROM Study_Group
            WHERE date_created <= {date}
              AND (date_closed IS NULL OR date_closed >= {date})
            ORDER BY group_code
            """)
            .ToListAsync(ct);
    }
}
