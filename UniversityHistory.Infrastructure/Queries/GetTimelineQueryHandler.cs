using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetTimeline;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetTimelineQueryHandler : IGetTimelineQueryHandler
{
    private readonly UniversityDbContext _db;

    public GetTimelineQueryHandler(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<TimelineEventDto>> HandleAsync(
        GetTimelineQuery query,
        CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(s => s.StudentId == query.StudentId, ct);

        if (!exists)
        {
            throw new NotFoundException("Student", query.StudentId);
        }

        var studentId = query.StudentId;

        var rawQuery = _db.Database.SqlQuery<TimelineEventDto>($"""
            SELECT
                'Enrollment'                                    AS EventType,
                CONCAT('Enrolled in group ', g.group_code,
                       ' (', e.reason_start, ')')               AS Description,
                e.date_from                                     AS DateFrom,
                e.date_to                                       AS DateTo
            FROM Student_Group_Enrollment e
            JOIN Study_Group g ON g.group_id = e.group_id
            WHERE e.student_id = {studentId}

            UNION ALL

            SELECT
                'AcademicLeave',
                CONCAT('Academic leave: ',
                       ISNULL(al.reason, 'No reason provided')),
                al.start_date,
                al.end_date
            FROM Academic_Leave al
            JOIN Student_Group_Enrollment e_al ON e_al.enrollment_id = al.enrollment_id
            WHERE e_al.student_id = {studentId}

            UNION ALL

            SELECT
                'ExternalTransfer',
                CONCAT(et.transfer_type, ' transfer - ', i.institution_name,
                       CASE WHEN et.notes IS NOT NULL
                            THEN CONCAT('. Notes: ', et.notes) ELSE '' END),
                et.transfer_date,
                NULL
            FROM External_Transfers et
            JOIN Institution i ON i.institution_id = et.institution_id
            WHERE et.student_id = {studentId}
            """);

        var count = await rawQuery.CountAsync(ct);
        var items = await rawQuery
            .OrderBy(x => x.DateFrom)
            .ThenBy(x => x.EventType)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<TimelineEventDto>(items, query.Page, query.PageSize, count);
    }
}
