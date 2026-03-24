using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GradeService : IGradeService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IGradeRepository _gradeRepo;

    public GradeService(IStudentRepository studentRepo, IGradeRepository gradeRepo)
    {
        _studentRepo = studentRepo;
        _gradeRepo = gradeRepo;
    }

    public async Task<IEnumerable<GradeDto>> GetGradesAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var grades = await _gradeRepo.GetByStudentIdAsync(studentId, ct);
        return grades.Select(g => new GradeDto(
            g.GradeId,
            g.CourseEnrollment.Discipline.DisciplineName,
            g.CourseEnrollment.SemesterNo,
            g.GradeValue,
            g.AssessmentDate));
    }
}
