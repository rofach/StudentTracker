using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Application.Queries.GetClassmates;
using UniversityHistory.Application.Queries.GetStudentGroupOnDate;
using UniversityHistory.Application.Queries.GetTimeline;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMovementService _movementService;
    private readonly IStudyPlanService _planService;
    private readonly IGetTimelineQueryHandler _timelineHandler;
    private readonly IGetClassmatesQueryHandler _classmatesHandler;
    private readonly IGetStudentGroupOnDateQueryHandler _groupOnDateHandler;

    public StudentService(
        IUnitOfWork unitOfWork,
        IMovementService movementService,
        IStudyPlanService planService,
        IGetTimelineQueryHandler timelineHandler,
        IGetClassmatesQueryHandler classmatesHandler,
        IGetStudentGroupOnDateQueryHandler groupOnDateHandler)
    {
        _unitOfWork = unitOfWork;
        _movementService = movementService;
        _planService = planService;
        _timelineHandler = timelineHandler;
        _classmatesHandler = classmatesHandler;
        _groupOnDateHandler = groupOnDateHandler;
    }

    public async Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct);
        return student is null ? null : student.ToDto();
    }

    public async Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var result = await _unitOfWork.Students.GetAllAsync(page, pageSize, ct);
        return new PagedResult<StudentDto>(
            result.Items.Select(static student => student.ToDto()),
            page,
            pageSize,
            result.TotalCount);
    }

    public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
    {
        var student = dto.ToEntity();
        await _unitOfWork.Students.AddAsync(student, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return student.ToDto();
    }

    public async Task<StudentDto> UpdateAsync(int studentId, StudentUpdateDto dto, CancellationToken ct = default)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.BirthDate = dto.BirthDate;
        student.Email = dto.Email;
        student.Phone = dto.Phone;

        await _unitOfWork.Students.UpdateAsync(student, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return student.ToDto();
    }

    public async Task ChangeStatusAsync(int studentId, ChangeStatusDto dto, CancellationToken ct = default)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var newStatus = Enum.Parse<StudentStatus>(dto.Status, ignoreCase: true);

        if (student.Status is StudentStatus.Expelled or StudentStatus.Graduated)
        {
            throw new DomainException($"Cannot change status of a student with terminal status '{student.Status}'.");
        }

        student.Status = newStatus;
        await _unitOfWork.Students.UpdateAsync(student, ct);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<StudentDetailDto> GetDetailAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollments = await _unitOfWork.Enrollments.GetByStudentIdAsync(studentId, ct);
        var plans = await _planService.GetPlanAssignmentsAsync(studentId, ct);
        var movements = await _movementService.GetMovementsAsync(studentId, ct);

        return student.ToDto(
            enrollments.Select(static enrollment => enrollment.ToDto()),
            plans,
            movements.Leaves,
            movements.Transfers);
    }

    public Task<PagedResult<TimelineEventDto>> GetTimelineAsync(
        int studentId,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        return _timelineHandler.HandleAsync(new GetTimelineQuery(studentId, page, pageSize), ct);
    }

    public Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(
        int studentId,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        CancellationToken ct = default)
    {
        return _classmatesHandler.HandleAsync(new GetClassmatesQuery(studentId, dateFrom, dateTo), ct);
    }

    public Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(
        int studentId,
        DateOnly? date,
        CancellationToken ct = default)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
        return _groupOnDateHandler.HandleAsync(new GetStudentGroupOnDateQuery(studentId, targetDate), ct);
    }
}
