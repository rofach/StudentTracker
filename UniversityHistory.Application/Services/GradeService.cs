using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Application.Queries.GetAverageGrade;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GradeService : IGradeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGetAverageGradeQueryHandler _avgHandler;

    public GradeService(
        IUnitOfWork unitOfWork,
        IGetAverageGradeQueryHandler avgHandler)
    {
        _unitOfWork = unitOfWork;
        _avgHandler = avgHandler;
    }

    public async Task<PagedResult<GradeDto>> GetGradesAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var result = await _unitOfWork.Grades.GetByStudentIdAsync(studentId, page, pageSize, ct);
        var dtos = result.Items.Select(grade => grade.ToDto(ResolveSemesterNo(grade)));

        return new PagedResult<GradeDto>(dtos, page, pageSize, result.TotalCount);
    }

    public async Task<AverageGradeDto> GetAverageGradeAsync(
        int studentId,
        int? semesterNo,
        int? disciplineId,
        int? academicYearStart,
        CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
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
