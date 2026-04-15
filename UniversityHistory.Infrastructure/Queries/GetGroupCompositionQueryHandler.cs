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
                ssa.subgroup_id     AS SubgroupId,
                sg.subgroup_name    AS SubgroupName,
                e.date_from         AS DateFrom,
                e.date_to           AS DateTo
            FROM Student_Group_Enrollment e
            JOIN Student s ON s.student_id  = e.student_id
            LEFT JOIN Student_Subgroup_Assignment ssa ON ssa.enrollment_id = e.enrollment_id
            LEFT JOIN Subgroup sg ON sg.subgroup_id = ssa.subgroup_id
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

