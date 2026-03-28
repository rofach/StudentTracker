using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetAverageGrade;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GradeService : IGradeService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IGradeRepository _gradeRepo;
    private readonly IGetAverageGradeQueryHandler _avgHandler;

    public GradeService(IStudentRepository studentRepo, IGradeRepository gradeRepo,
        IGetAverageGradeQueryHandler avgHandler)
    {
        _studentRepo = studentRepo;
        _gradeRepo   = gradeRepo;
        _avgHandler  = avgHandler;
    }

    public async Task<IEnumerable<GradeDto>> GetGradesAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var grades = await _gradeRepo.GetByStudentIdAsync(studentId, ct);
        return grades.Select(g => new GradeDto(
            g.GradeId,
            g.CourseEnrollment.Discipline.DisciplineName,
            ResolveSemesterNo(g),
            g.CourseEnrollment.AcademicYearStart,
            $"{g.CourseEnrollment.AcademicYearStart}/{g.CourseEnrollment.AcademicYearStart + 1}",
            g.GradeValue,
            g.AssessmentDate));
    }

    public async Task<AverageGradeDto> GetAverageGradeAsync(
        int studentId, int? semesterNo, int? disciplineId, int? academicYearStart,
        CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        return await _avgHandler.HandleAsync(
            new GetAverageGradeQuery(studentId, semesterNo, disciplineId, academicYearStart), ct);
    }

    private static int ResolveSemesterNo(GradeRecord grade)
    {
        var semesterNo = grade.CourseEnrollment.Assignment.Plan.PlanDisciplines
            .SingleOrDefault(pd => pd.DisciplineId == grade.CourseEnrollment.DisciplineId)
            ?.SemesterNo;

        return semesterNo ?? throw new DomainException(
            $"No semester mapping found in plan {grade.CourseEnrollment.Assignment.PlanId} " +
            $"for discipline {grade.CourseEnrollment.DisciplineId}.");
    }
}
