using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetClassmates;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetClassmatesQueryHandler : IGetClassmatesQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetClassmatesQueryHandler(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ClassmateDto>> HandleAsync(
        GetClassmatesQuery query, CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(s => s.StudentId == query.StudentId, ct);
        if (!exists) throw new NotFoundException("Student", query.StudentId);

        var studentId = query.StudentId;
        var dateFrom = query.DateFrom;
        var dateTo = query.DateTo;

        return await _db.Database.SqlQuery<ClassmateDto>($"""
            SELECT DISTINCT
                other_e.student_id        AS ClassmateStudentId,
                s.first_name              AS FirstName,
                s.last_name               AS LastName,
                g.group_code              AS GroupCode,
                other_e.group_id          AS GroupId,
                sse.subgroup_id           AS SubgroupId,
                sse.subgroup_name         AS SubgroupName,
                CASE
                    WHEN mine_e.date_from > other_e.date_from THEN mine_e.date_from
                    ELSE other_e.date_from
                END                       AS SharedFrom,
                CASE
                    WHEN mine_e.date_to IS NULL AND other_e.date_to IS NULL THEN NULL
                    WHEN mine_e.date_to IS NULL THEN other_e.date_to
                    WHEN other_e.date_to IS NULL THEN mine_e.date_to
                    WHEN mine_e.date_to < other_e.date_to THEN mine_e.date_to
                    ELSE other_e.date_to
                END                       AS SharedTo
            FROM Student_Group_Enrollment mine_e
            JOIN Student_Group_Enrollment other_e
              ON other_e.group_id = mine_e.group_id
             AND other_e.student_id <> {studentId}
            JOIN Student s ON s.student_id = other_e.student_id
            JOIN Study_Group g ON g.group_id = other_e.group_id
            OUTER APPLY (
                SELECT TOP 1
                    se.subgroup_id   AS subgroup_id,
                    sg.subgroup_name AS subgroup_name
                FROM Student_Subgroup_Enrollment se
                JOIN Subgroup sg ON sg.subgroup_id = se.subgroup_id
                WHERE se.enrollment_id = other_e.enrollment_id
                  AND se.date_from <= CASE
                        WHEN mine_e.date_from > other_e.date_from THEN mine_e.date_from
                        ELSE other_e.date_from
                    END
                  AND (se.date_to IS NULL OR se.date_to >= CASE
                        WHEN mine_e.date_from > other_e.date_from THEN mine_e.date_from
                        ELSE other_e.date_from
                    END)
                ORDER BY se.date_from DESC, se.subgroup_enrollment_id DESC
            ) sse
            WHERE mine_e.student_id = {studentId}
              AND mine_e.date_from <= ISNULL(other_e.date_to, '9999-12-31')
              AND ISNULL(mine_e.date_to, '9999-12-31') >= other_e.date_from
              AND ({dateFrom} IS NULL OR ISNULL(
                    CASE
                        WHEN mine_e.date_to IS NULL AND other_e.date_to IS NULL THEN NULL
                        WHEN mine_e.date_to IS NULL THEN other_e.date_to
                        WHEN other_e.date_to IS NULL THEN mine_e.date_to
                        WHEN mine_e.date_to < other_e.date_to THEN mine_e.date_to
                        ELSE other_e.date_to
                    END,
                    '9999-12-31') >= {dateFrom})
              AND ({dateTo}   IS NULL OR CASE
                    WHEN mine_e.date_from > other_e.date_from THEN mine_e.date_from
                    ELSE other_e.date_from
                  END <= {dateTo})
            ORDER BY
                CASE
                    WHEN mine_e.date_from > other_e.date_from THEN mine_e.date_from
                    ELSE other_e.date_from
                END,
                s.last_name,
                s.first_name
            """)
            .ToListAsync(ct);
    }
}

