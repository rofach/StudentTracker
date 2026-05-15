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
                g.group_id          AS GroupId,
                g.group_code        AS GroupCode,
                d.name              AS DepartmentName,
                au.name             AS AcademicUnitName,
                au.type             AS AcademicUnitType,
                g.date_created      AS DateCreated,
                g.date_closed       AS DateClosed,
                CASE WHEN MONTH(GETDATE()) >= 9 THEN YEAR(GETDATE()) ELSE YEAR(GETDATE()) - 1 END
                - CASE WHEN MONTH(g.date_created) >= 9 THEN YEAR(g.date_created) ELSE YEAR(g.date_created) - 1 END
                + 1                 AS CourseYear
            FROM Study_Group g
            JOIN Department d   ON d.department_id    = g.department_id
            JOIN Academic_Unit au ON au.academic_unit_id = d.academic_unit_id
            WHERE g.date_created <= {date}
              AND (g.date_closed IS NULL OR g.date_closed >= {date})
            ORDER BY g.group_code
            """)
            .ToListAsync(ct);
    }
}

