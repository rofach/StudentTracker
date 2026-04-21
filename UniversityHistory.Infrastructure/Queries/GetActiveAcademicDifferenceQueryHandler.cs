using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetActiveAcademicDifference;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetActiveAcademicDifferenceQueryHandler : IGetActiveAcademicDifferenceQueryHandler
{
    private readonly UniversityDbContext _db;

    public GetActiveAcademicDifferenceQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<PagedResult<ActiveAcademicDifferenceDto>> HandleAsync(
        GetActiveAcademicDifferenceQuery query,
        CancellationToken ct = default)
    {
        var studentName = string.IsNullOrWhiteSpace(query.StudentName) ? null : query.StudentName.Trim();
        var disciplineName = string.IsNullOrWhiteSpace(query.DisciplineName) ? null : query.DisciplineName.Trim();
        var status = string.IsNullOrWhiteSpace(query.Status) ? "Pending" : query.Status.Trim();

        var rawQuery = _db.Database.SqlQuery<ActiveAcademicDifferenceDto>($"""
            SELECT
                adi.difference_item_id                                         AS DifferenceItemId,
                t.transfer_id                                                  AS TransferId,
                s.student_id                                                   AS StudentId,
                CONCAT(s.last_name, N' ', s.first_name, N' ', ISNULL(s.patronymic, N'')) AS StudentName,
                old_g.group_code                                               AS OldGroupCode,
                new_g.group_code                                               AS NewGroupCode,
                t.transfer_date                                                AS TransferDate,
                d.discipline_name                                              AS DisciplineName,
                pd.semester_no                                                 AS SemesterNo,
                CAST(adi.status AS nvarchar(32))                               AS Status,
                adi.notes                                                      AS Notes
            FROM Academic_Difference_Item adi
            JOIN Student_Group_Transfer t
                ON t.transfer_id = adi.transfer_id
            JOIN Student_Group_Enrollment old_e
                ON old_e.enrollment_id = t.old_enrollment_id
            JOIN Student s
                ON s.student_id = old_e.student_id
            JOIN Student_Group_Enrollment new_e
                ON new_e.enrollment_id = t.new_enrollment_id
            JOIN Study_Group old_g
                ON old_g.group_id = old_e.group_id
            JOIN Study_Group new_g
                ON new_g.group_id = new_e.group_id
            JOIN Plan_Disciplines pd
                ON pd.plan_discipline_id = adi.plan_discipline_id
            JOIN Discipline d
                ON d.discipline_id = pd.discipline_id
            WHERE ({status} IS NULL OR adi.status = {status})
              AND ({studentName} IS NULL
                   OR s.last_name LIKE N'%' + {studentName} + N'%'
                   OR s.first_name LIKE N'%' + {studentName} + N'%'
                   OR ISNULL(s.patronymic, N'') LIKE N'%' + {studentName} + N'%')
              AND ({disciplineName} IS NULL OR d.discipline_name LIKE N'%' + {disciplineName} + N'%')
              AND ({query.DateFrom} IS NULL OR t.transfer_date >= {query.DateFrom})
              AND ({query.DateTo} IS NULL OR t.transfer_date <= {query.DateTo})
            """);

        var totalCount = await rawQuery.CountAsync(ct);
        var items = await rawQuery
            .OrderBy(item => item.TransferDate)
            .ThenBy(item => item.StudentName)
            .ThenBy(item => item.DisciplineName)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<ActiveAcademicDifferenceDto>(items, query.Page, query.PageSize, totalCount);
    }
}
