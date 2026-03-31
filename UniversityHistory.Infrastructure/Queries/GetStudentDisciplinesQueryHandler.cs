using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetStudentDisciplines;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetStudentDisciplinesQueryHandler : IGetStudentDisciplinesQueryHandler
{
    private readonly UniversityDbContext _db;

    public GetStudentDisciplinesQueryHandler(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<StudentDisciplineOptionDto>> HandleAsync(
        GetStudentDisciplinesQuery query,
        CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(student => student.StudentId == query.StudentId, ct);

        if (!exists)
        {
            throw new NotFoundException("Student", query.StudentId);
        }

        var studentId = query.StudentId;

        var rawItems = await _db.Database.SqlQuery<StudentDisciplineOptionRaw>($"""
            SELECT DISTINCT
                ce.discipline_id AS DisciplineId,
                d.discipline_name AS DisciplineName,
                pd.semester_no AS SemesterNo,
                CAST(CASE
                    WHEN EXISTS (
                        SELECT 1
                        FROM Grade_Record gr
                        JOIN Student_Course_Enrollment ce_inner
                            ON ce_inner.course_enrollment_id = gr.course_enrollment_id
                        JOIN Student_Plan_Assignment pa_inner
                            ON pa_inner.assignment_id = ce_inner.assignment_id
                        WHERE pa_inner.student_id = {studentId}
                          AND ce_inner.discipline_id = ce.discipline_id
                    ) THEN 1
                    ELSE 0
                END AS bit) AS HasGrade
            FROM Student_Course_Enrollment ce
            JOIN Student_Plan_Assignment pa
                ON pa.assignment_id = ce.assignment_id
            JOIN Plan_Disciplines pd
                ON pd.plan_id = pa.plan_id
               AND pd.discipline_id = ce.discipline_id
            JOIN Discipline d
                ON d.discipline_id = ce.discipline_id
            WHERE pa.student_id = {studentId}
            ORDER BY pd.semester_no, d.discipline_name
            """)
            .ToListAsync(ct);

        return rawItems
            .Select(static item => new StudentDisciplineOptionDto(
                item.DisciplineId,
                item.DisciplineName,
                item.SemesterNo,
                item.HasGrade))
            .ToList();
    }

    private sealed class StudentDisciplineOptionRaw
    {
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; } = string.Empty;
        public int SemesterNo { get; set; }
        public bool HasGrade { get; set; }
    }
}
