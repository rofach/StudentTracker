using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetStudentSearch;
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
    private readonly IGetTimelineQueryHandler _timelineHandler;
    private readonly IGetClassmatesQueryHandler _classmatesHandler;
    private readonly IGetStudentGroupOnDateQueryHandler _groupOnDateHandler;
    private readonly IGetStudentSearchQueryHandler _studentSearchHandler;

    public StudentService(
        IUnitOfWork unitOfWork,
        IMovementService movementService,
        IGetTimelineQueryHandler timelineHandler,
        IGetClassmatesQueryHandler classmatesHandler,
        IGetStudentGroupOnDateQueryHandler groupOnDateHandler,
        IGetStudentSearchQueryHandler studentSearchHandler)
    {
        _unitOfWork = unitOfWork;
        _movementService = movementService;
        _timelineHandler = timelineHandler;
        _classmatesHandler = classmatesHandler;
        _groupOnDateHandler = groupOnDateHandler;
        _studentSearchHandler = studentSearchHandler;
    }

    public async Task<StudentDto?> GetByIdAsync(Guid studentId, CancellationToken ct = default)
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

    public Task<PagedResult<StudentDto>> SearchAsync(
        string? fullName,
        string? email,
        string? status,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        return _studentSearchHandler.HandleAsync(
            new GetStudentSearchQuery(fullName, email, status, page, pageSize),
            ct);
    }

    public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
    {
        var student = dto.ToEntity();
        _unitOfWork.Students.Add(student);
        await _unitOfWork.SaveChangesAsync(ct);
        return student.ToDto();
    }

    public async Task<StudentDto> UpdateAsync(Guid studentId, StudentUpdateDto dto, CancellationToken ct = default)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.Patronymic = dto.Patronymic;
        student.BirthDate = dto.BirthDate;
        student.Email = dto.Email;
        student.Phone = dto.Phone;

        _unitOfWork.Students.Update(student);
        await _unitOfWork.SaveChangesAsync(ct);
        return student.ToDto();
    }

    public async Task ChangeStatusAsync(Guid studentId, ChangeStatusDto dto, CancellationToken ct = default)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var newStatus = Enum.Parse<StudentStatus>(dto.Status, ignoreCase: true);

        if (student.Status is StudentStatus.Expelled or StudentStatus.Graduated)
        {
            throw new DomainException($"Cannot change status of a student with terminal status '{student.Status}'.");
        }

        student.Status = newStatus;
        _unitOfWork.Students.Update(student);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<StudentDetailDto> GetDetailAsync(Guid studentId, CancellationToken ct = default)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollments = await _unitOfWork.Enrollments.GetByStudentIdAsync(studentId, ct);
        var movements   = await _movementService.GetMovementsAsync(studentId, ct);

        var plans = new List<GroupPlanAssignmentDto>();
        foreach (var enrollment in enrollments)
        {
            var groupPlans = await _unitOfWork.GroupPlanAssignments.GetByGroupIdAsync(enrollment.GroupId, ct);
            foreach (var gpa in groupPlans)
            {
                var enrollEnd = enrollment.DateTo ?? DateOnly.MaxValue;
                var planEnd   = gpa.DateTo ?? DateOnly.MaxValue;
                if (gpa.DateFrom <= enrollEnd && planEnd >= enrollment.DateFrom)
                    plans.Add(gpa.ToDto());
            }
        }

        return student.ToDto(
            enrollments.Select(static e => e.ToDto()),
            plans.DistinctBy(p => p.GroupPlanAssignmentId),
            movements.Leaves,
            movements.Transfers);
    }

    public Task<PagedResult<TimelineEventDto>> GetTimelineAsync(
        Guid studentId,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        return _timelineHandler.HandleAsync(new GetTimelineQuery(studentId, page, pageSize), ct);
    }

    public Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(
        Guid studentId,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        CancellationToken ct = default)
    {
        return _classmatesHandler.HandleAsync(new GetClassmatesQuery(studentId, dateFrom, dateTo), ct);
    }

    public Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(
        Guid studentId,
        DateOnly? date,
        CancellationToken ct = default)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
        return _groupOnDateHandler.HandleAsync(new GetStudentGroupOnDateQuery(studentId, targetDate), ct);
    }
}

