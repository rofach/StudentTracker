using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Application.Queries.GetAverageGrade;
using UniversityHistory.Application.Queries.GetStudentDisciplines;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GradeService : IGradeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGetAverageGradeQueryHandler _avgHandler;
    private readonly IGetStudentDisciplinesQueryHandler _studentDisciplinesHandler;

    public GradeService(
        IUnitOfWork unitOfWork,
        IGetAverageGradeQueryHandler avgHandler,
        IGetStudentDisciplinesQueryHandler studentDisciplinesHandler)
    {
        _unitOfWork = unitOfWork;
        _avgHandler = avgHandler;
        _studentDisciplinesHandler = studentDisciplinesHandler;
    }

    public async Task<PagedResult<GradeDto>> GetGradesAsync(Guid studentId, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var result = await _unitOfWork.Grades.GetByStudentIdAsync(studentId, page, pageSize, ct);
        var dtos = result.Items.Select(grade => grade.ToDto(ResolveSemesterNo(grade)));

        return new PagedResult<GradeDto>(dtos, page, pageSize, result.TotalCount);
    }

    public async Task<AverageGradeDto> GetAverageGradeAsync(
        Guid studentId,
        int? semesterNo,
        Guid? disciplineId,
        int? academicYearStart,
        CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        return await _avgHandler.HandleAsync(
            new GetAverageGradeQuery(studentId, semesterNo, disciplineId, academicYearStart), ct);
    }

    public async Task<IReadOnlyList<StudentDisciplineOptionDto>> GetStudentDisciplinesAsync(
        Guid studentId,
        CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        return await _studentDisciplinesHandler.HandleAsync(
            new GetStudentDisciplinesQuery(studentId),
            ct);
    }

    private static int ResolveSemesterNo(GradeRecord grade)
    {
        return grade.CourseEnrollment.PlanDiscipline?.SemesterNo ?? throw new DomainException(
            $"No semester mapping found for plan discipline {grade.CourseEnrollment.PlanDisciplineId}.");
    }
}

