using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetInternalTransferJournal;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetInternalTransferJournalQueryHandler : IGetInternalTransferJournalQueryHandler
{
    private readonly UniversityDbContext _db;

    public GetInternalTransferJournalQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<PagedResult<InternalTransferJournalItemDto>> HandleAsync(
        GetInternalTransferJournalQuery query,
        CancellationToken ct = default)
    {
        var studentName = string.IsNullOrWhiteSpace(query.StudentName) ? null : query.StudentName.Trim();

        var rawQuery = _db.Database.SqlQuery<InternalTransferJournalItemDto>($"""
            SELECT
                t.transfer_id                                                   AS TransferId,
                s.student_id                                                    AS StudentId,
                CONCAT(s.last_name, N' ', s.first_name, N' ', ISNULL(s.patronymic, N'')) AS StudentName,
                old_g.group_code                                                AS OldGroupCode,
                new_g.group_code                                                AS NewGroupCode,
                t.transfer_date                                                 AS TransferDate,
                t.reason                                                        AS Reason,
                COUNT(adi.difference_item_id)                                   AS DifferenceItemsTotal,
                SUM(CASE WHEN adi.status = N'Pending' THEN 1 ELSE 0 END)        AS DifferenceItemsPending,
                SUM(CASE WHEN adi.status = N'Completed' THEN 1 ELSE 0 END)      AS DifferenceItemsCompleted,
                SUM(CASE WHEN adi.status = N'Waived' THEN 1 ELSE 0 END)         AS DifferenceItemsWaived
            FROM Student_Group_Transfer t
            JOIN Student_Group_Enrollment old_e
                ON old_e.enrollment_id = t.old_enrollment_id
            JOIN Student_Group_Enrollment new_e
                ON new_e.enrollment_id = t.new_enrollment_id
            JOIN Student s
                ON s.student_id = old_e.student_id
            JOIN Study_Group old_g
                ON old_g.group_id = old_e.group_id
            JOIN Study_Group new_g
                ON new_g.group_id = new_e.group_id
            LEFT JOIN Academic_Difference_Item adi
                ON adi.transfer_id = t.transfer_id
            WHERE ({studentName} IS NULL
                   OR s.last_name LIKE N'%' + {studentName} + N'%'
                   OR s.first_name LIKE N'%' + {studentName} + N'%'
                   OR ISNULL(s.patronymic, N'') LIKE N'%' + {studentName} + N'%')
              AND ({query.DateFrom} IS NULL OR t.transfer_date >= {query.DateFrom})
              AND ({query.DateTo} IS NULL OR t.transfer_date <= {query.DateTo})
            GROUP BY
                t.transfer_id,
                s.student_id,
                s.last_name,
                s.first_name,
                s.patronymic,
                old_g.group_code,
                new_g.group_code,
                t.transfer_date,
                t.reason
            HAVING ({query.OnlyWithPendingDifference} = CAST(0 AS bit)
                    OR SUM(CASE WHEN adi.status = N'Pending' THEN 1 ELSE 0 END) > 0)
            """);

        var totalCount = await rawQuery.CountAsync(ct);
        var items = await rawQuery
            .OrderByDescending(item => item.TransferDate)
            .ThenBy(item => item.StudentName)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<InternalTransferJournalItemDto>(items, query.Page, query.PageSize, totalCount);
    }
}
