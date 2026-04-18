using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetStudentGroupOnDate;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetStudentGroupOnDateQueryHandler : IGetStudentGroupOnDateQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetStudentGroupOnDateQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<StudentCurrentGroupDto?> HandleAsync(
        GetStudentGroupOnDateQuery query, CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(s => s.StudentId == query.StudentId, ct);
        if (!exists) throw new NotFoundException("Student", query.StudentId);

        var studentId = query.StudentId;
        var date = query.Date;

        return await _db.Database.SqlQuery<StudentCurrentGroupDto>($"""
            SELECT TOP 1
                e.enrollment_id     AS EnrollmentId,
                e.group_id          AS GroupId,
                g.group_code        AS GroupCode,
                d.name              AS DepartmentName,
                au.name             AS AcademicUnitName,
                au.type             AS AcademicUnitType,
                sse.subgroup_id     AS SubgroupId,
                sse.subgroup_name   AS SubgroupName,
                e.date_from         AS DateFrom,
                e.date_to           AS DateTo
            FROM Student_Group_Enrollment e
            JOIN Study_Group g      ON g.group_id       = e.group_id
            JOIN Department d       ON d.department_id  = g.department_id
            JOIN Academic_Unit au   ON au.academic_unit_id = d.academic_unit_id
            OUTER APPLY (
                SELECT TOP 1
                    se.subgroup_id   AS subgroup_id,
                    sg.subgroup_name AS subgroup_name
                FROM Student_Subgroup_Enrollment se
                JOIN Subgroup sg ON sg.subgroup_id = se.subgroup_id
                WHERE se.enrollment_id = e.enrollment_id
                  AND se.date_from <= {date}
                  AND (se.date_to IS NULL OR se.date_to >= {date})
                ORDER BY se.date_from DESC, se.subgroup_enrollment_id DESC
            ) sse
            WHERE e.student_id = {studentId}
              AND e.date_from <= {date}
              AND (e.date_to IS NULL OR e.date_to >= {date})
            ORDER BY e.date_from DESC, e.enrollment_id DESC
            """)
            .FirstOrDefaultAsync(ct);
    }
}

