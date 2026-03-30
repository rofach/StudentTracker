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
    private readonly IStudentRepository _studentRepo;
    private readonly IEnrollmentRepository _enrollmentRepo;
    private readonly IMovementService _movementService;
    private readonly IStudyPlanService _planService;
    private readonly IGetTimelineQueryHandler _timelineHandler;
    private readonly IGetClassmatesQueryHandler _classmatesHandler;
    private readonly IGetStudentGroupOnDateQueryHandler _groupOnDateHandler;

    public StudentService(
        IStudentRepository studentRepo,
        IEnrollmentRepository enrollmentRepo,
        IMovementService movementService,
        IStudyPlanService planService,
        IGetTimelineQueryHandler timelineHandler,
        IGetClassmatesQueryHandler classmatesHandler,
        IGetStudentGroupOnDateQueryHandler groupOnDateHandler)
    {
        _studentRepo = studentRepo;
        _enrollmentRepo = enrollmentRepo;
        _movementService = movementService;
        _planService = planService;
        _timelineHandler = timelineHandler;
        _classmatesHandler = classmatesHandler;
        _groupOnDateHandler = groupOnDateHandler;
    }

    public async Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct);
        return student is null ? null : student.ToDto();
    }

    public async Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var (items, count) = await _studentRepo.GetAllAsync(page, pageSize, ct);
        return new PagedResult<StudentDto>(items.Select(static student => student.ToDto()), page, pageSize, count);
    }

    public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
    {
        var student = dto.ToEntity();
        return (await _studentRepo.AddAsync(student, ct)).ToDto();
    }

    public async Task<StudentDto> UpdateAsync(int studentId, StudentUpdateDto dto, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.BirthDate = dto.BirthDate;
        student.Email = dto.Email;
        student.Phone = dto.Phone;

        await _studentRepo.UpdateAsync(student, ct);
        return student.ToDto();
    }

    public async Task ChangeStatusAsync(int studentId, ChangeStatusDto dto, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        if (!Enum.TryParse<StudentStatus>(dto.Status, ignoreCase: true, out var newStatus))
        {
            throw new DomainException($"Unknown status: '{dto.Status}'. Valid values: Active, OnLeave, Expelled, Graduated.");
        }

        if (student.Status is StudentStatus.Expelled or StudentStatus.Graduated)
        {
            throw new DomainException($"Cannot change status of a student with terminal status '{student.Status}'.");
        }

        student.Status = newStatus;
        await _studentRepo.UpdateAsync(student, ct);
    }

    public async Task<StudentDetailDto> GetDetailAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollments = await _enrollmentRepo.GetByStudentIdAsync(studentId, ct);
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
