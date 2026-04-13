using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetAverageGrade;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetAverageGradeQueryHandler : IGetAverageGradeQueryHandler
{
    private readonly UniversityDbContext _db;
    public GetAverageGradeQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<AverageGradeDto> HandleAsync(GetAverageGradeQuery query, CancellationToken ct = default)
    {
        var exists = await _db.Students.AnyAsync(s => s.StudentId == query.StudentId, ct);
        if (!exists) throw new NotFoundException("Student", query.StudentId);

        var studentId    = query.StudentId;
        var semesterNo   = query.SemesterNo;
        var disciplineId = query.DisciplineId;
        var academicYear = query.AcademicYearStart;

        var result = await _db.Database.SqlQuery<AverageGradeRaw>($"""
            SELECT
                AVG(TRY_CAST(gr.grade_value AS DECIMAL(10,2)))  AS Average,
                COUNT(gr.grade_id)                              AS GradeCount
            FROM Grade_Record gr
            JOIN Student_Course_Enrollment ce  ON ce.course_enrollment_id = gr.course_enrollment_id
            JOIN Student_Group_Enrollment  e   ON e.enrollment_id         = ce.enrollment_id
            JOIN Group_Plan_Assignment     gpa ON gpa.group_plan_assignment_id = ce.group_plan_assignment_id
            JOIN Plan_Disciplines          pd  ON pd.plan_id              = gpa.plan_id
                                               AND pd.discipline_id       = ce.discipline_id
            WHERE e.student_id                   = {studentId}
              AND ({semesterNo}   IS NULL OR pd.semester_no         = {semesterNo})
              AND ({disciplineId} IS NULL OR ce.discipline_id       = {disciplineId})
              AND ({academicYear} IS NULL OR ce.academic_year_start = {academicYear})
              AND TRY_CAST(gr.grade_value AS DECIMAL(10,2)) IS NOT NULL
            """)
            .FirstAsync(ct);

        return new AverageGradeDto(
            result.Average,
            result.GradeCount,
            academicYear.HasValue ? $"{academicYear}/{academicYear + 1}" : null);
    }

    private sealed class AverageGradeRaw
    {
        public decimal? Average { get; set; }
        public int GradeCount { get; set; }
    }
}
