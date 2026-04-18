using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetGroupComposition;
using UniversityHistory.Domain.Common;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetGroupCompositionQueryHandler : IGetGroupCompositionQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetGroupCompositionQueryHandler(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<GroupCompositionMemberDto>> HandleAsync(
        GetGroupCompositionQuery query, CancellationToken ct = default)
    {
        var exists = await _db.StudyGroups.AnyAsync(g => g.GroupId == query.GroupId, ct);
        if (!exists) throw new NotFoundException("StudyGroup", query.GroupId);

        var groupId = query.GroupId;
        var date = query.Date;

        var rawQuery = _db.Database.SqlQuery<GroupCompositionMemberDto>($"""
            SELECT
                e.student_id        AS StudentId,
                s.first_name        AS FirstName,
                s.last_name         AS LastName,
                s.email             AS Email,
                sse.subgroup_id     AS SubgroupId,
                sse.subgroup_name   AS SubgroupName,
                e.date_from         AS DateFrom,
                e.date_to           AS DateTo
            FROM Student_Group_Enrollment e
            JOIN Student s ON s.student_id  = e.student_id
            OUTER APPLY (
                SELECT TOP 1
                    se.subgroup_id      AS subgroup_id,
                    sg.subgroup_name    AS subgroup_name
                FROM Student_Subgroup_Enrollment se
                JOIN Subgroup sg ON sg.subgroup_id = se.subgroup_id
                WHERE se.enrollment_id = e.enrollment_id
                  AND se.date_from <= {date}
                  AND (se.date_to IS NULL OR se.date_to >= {date})
                ORDER BY se.date_from DESC, se.subgroup_enrollment_id DESC
            ) sse
            WHERE e.group_id  = {groupId}
              AND e.date_from <= {date}
              AND (e.date_to IS NULL OR e.date_to >= {date})
            """);

        var count = await rawQuery.CountAsync(ct);
        var items = await rawQuery
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<GroupCompositionMemberDto>(items, query.Page, query.PageSize, count);
    }
}

