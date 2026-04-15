using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetStudentsInGroup;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetStudentsInGroupQueryHandler : IGetStudentsInGroupQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetStudentsInGroupQueryHandler(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<GroupStudentDto>> HandleAsync(
        GetStudentsInGroupQuery query, CancellationToken ct = default)
    {
        var exists = await _db.StudyGroups.AnyAsync(g => g.GroupId == query.GroupId, ct);
        if (!exists) throw new NotFoundException("StudyGroup", query.GroupId);

        var groupId = query.GroupId;
        var date = query.Date;

        var rawQuery = _db.Database.SqlQuery<GroupStudentDto>($"""
            SELECT
                e.enrollment_id AS EnrollmentId,
                e.student_id    AS StudentId,
                s.first_name    AS FirstName,
                s.last_name     AS LastName,
                s.email         AS Email,
                e.date_from     AS DateFrom,
                e.date_to       AS DateTo
            FROM Student_Group_Enrollment e
            JOIN Student s ON s.student_id = e.student_id
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

        return new PagedResult<GroupStudentDto>(items, query.Page, query.PageSize, count);
    }
}

