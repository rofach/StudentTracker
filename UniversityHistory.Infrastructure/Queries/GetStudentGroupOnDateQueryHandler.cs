using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetStudentGroupOnDate;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetStudentGroupOnDateQueryHandler : IGetStudentGroupOnDateQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetStudentGroupOnDateQueryHandler(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<StudentCurrentGroupDto?> HandleAsync(
        GetStudentGroupOnDateQuery query, CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(s => s.StudentId == query.StudentId, ct);
        if (!exists) throw new NotFoundException("Student", query.StudentId);

        var studentId = query.StudentId;
        var date = query.Date;

        return await _db.Database.SqlQuery<StudentCurrentGroupDto>($"""
            SELECT TOP 1
                e.enrollment_id AS EnrollmentId,
                e.group_id      AS GroupId,
                g.group_code    AS GroupCode,
                g.faculty       AS Faculty,
                e.date_from     AS DateFrom,
                e.date_to       AS DateTo
            FROM Student_Group_Enrollment e
            JOIN Study_Group g ON g.group_id = e.group_id
            WHERE e.student_id = {studentId}
              AND e.date_from <= {date}
              AND (e.date_to IS NULL OR e.date_to >= {date})
            """)
            .FirstOrDefaultAsync(ct);
    }
}
