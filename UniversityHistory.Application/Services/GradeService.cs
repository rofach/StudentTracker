using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Application.Queries.GetAverageGrade;
using UniversityHistory.Application.Queries.GetStudentDisciplines;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
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

    public async Task<GradeDto> UpsertGradeAsync(
        Guid studentId,
        Guid courseEnrollmentId,
        UpsertGradeDto dto,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.GradeValue))
            throw new DomainException("Grade value is required.");

        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var courseEnrollment = await _unitOfWork.StudyPlans.GetCourseEnrollmentByIdAsync(courseEnrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentCourseEnrollment), courseEnrollmentId);

        if (courseEnrollment.Enrollment.StudentId != studentId)
            throw new DomainException($"Course enrollment {courseEnrollmentId} does not belong to student {studentId}.");

        var existingGrade = await _unitOfWork.Grades.GetByCourseEnrollmentIdAsync(courseEnrollmentId, ct);

        GradeRecord grade;
        if (existingGrade is null)
        {
            grade = new GradeRecord
            {
                CourseEnrollmentId = courseEnrollmentId,
                CourseEnrollment = courseEnrollment,
                GradeValue = dto.GradeValue.Trim(),
                AssessmentDate = dto.AssessmentDate
            };
            _unitOfWork.Grades.Add(grade);
        }
        else
        {
            existingGrade.GradeValue = dto.GradeValue.Trim();
            existingGrade.AssessmentDate = dto.AssessmentDate;
            grade = existingGrade;
            _unitOfWork.Grades.Update(existingGrade);
        }

        courseEnrollment.Status = CourseStatus.Completed;

        var differenceItems = await _unitOfWork.GroupTransfers
            .GetOpenDifferenceItemsByStudentAndPlanDisciplineAsync(
                studentId,
                courseEnrollment.PlanDisciplineId,
                ct);

        foreach (var differenceItem in differenceItems)
        {
            differenceItem.Status = DifferenceItemStatus.Completed;
            _unitOfWork.GroupTransfers.UpdateDifferenceItem(differenceItem);
        }

        await _unitOfWork.SaveChangesAsync(ct);

        return grade.ToDto(courseEnrollment.PlanDiscipline.SemesterNo);
    }

    private static int ResolveSemesterNo(GradeRecord grade)
    {
        return grade.CourseEnrollment.PlanDiscipline?.SemesterNo ?? throw new DomainException(
            $"No semester mapping found for plan discipline {grade.CourseEnrollment.PlanDisciplineId}.");
    }
}

