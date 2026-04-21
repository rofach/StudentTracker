using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetStudentDisciplines;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetStudentDisciplinesQueryHandler : IGetStudentDisciplinesQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetStudentDisciplinesQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<IReadOnlyList<StudentDisciplineOptionDto>> HandleAsync(
        GetStudentDisciplinesQuery query, CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(s => s.StudentId == query.StudentId, ct);
        if (!exists) throw new NotFoundException("Student", query.StudentId);

        var studentId = query.StudentId;

        var rawItems = await _db.Database.SqlQuery<StudentDisciplineOptionRaw>($"""
            SELECT DISTINCT
                ce.course_enrollment_id                                                  AS CourseEnrollmentId,
                pd.discipline_id                                                        AS DisciplineId,
                d.discipline_name                                                       AS DisciplineName,
                pd.semester_no                                                          AS SemesterNo,
                ce.academic_year_start                                                  AS AcademicYearStart,
                CAST(ce.academic_year_start AS nvarchar(4))
                    + N'/'
                    + CAST(ce.academic_year_start + 1 AS nvarchar(4))                  AS AcademicYearLabel,
                CAST(CASE
                    WHEN EXISTS (
                        SELECT 1
                        FROM Grade_Record gr
                        JOIN Student_Course_Enrollment ce_inner
                            ON ce_inner.course_enrollment_id = gr.course_enrollment_id
                        JOIN Plan_Disciplines pd_inner
                            ON pd_inner.plan_discipline_id = ce_inner.plan_discipline_id
                        JOIN Student_Group_Enrollment e_inner
                            ON e_inner.enrollment_id = ce_inner.enrollment_id
                        WHERE e_inner.student_id = {studentId}
                          AND pd_inner.discipline_id = pd.discipline_id
                    ) THEN 1
                    ELSE 0
                END AS bit)                                                             AS HasGrade
            FROM Student_Course_Enrollment ce
            JOIN Student_Group_Enrollment e
                ON e.enrollment_id = ce.enrollment_id
            JOIN Plan_Disciplines pd
                ON pd.plan_discipline_id = ce.plan_discipline_id
            JOIN Discipline d
                ON d.discipline_id = pd.discipline_id
            WHERE e.student_id = {studentId}
            ORDER BY pd.semester_no, d.discipline_name
            """)
            .ToListAsync(ct);

        return rawItems
            .Select(item => new StudentDisciplineOptionDto(
                item.CourseEnrollmentId,
                item.DisciplineId,
                item.DisciplineName,
                item.SemesterNo,
                item.AcademicYearStart,
                item.AcademicYearLabel,
                item.HasGrade))
            .ToList();
    }

    private sealed class StudentDisciplineOptionRaw
    {
        public Guid CourseEnrollmentId { get; set; }
        public Guid DisciplineId { get; set; }
        public string DisciplineName { get; set; } = string.Empty;
        public int SemesterNo { get; set; }
        public int AcademicYearStart { get; set; }
        public string AcademicYearLabel { get; set; } = string.Empty;
        public bool HasGrade { get; set; }
    }
}

