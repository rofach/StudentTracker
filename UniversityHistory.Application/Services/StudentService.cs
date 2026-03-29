using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
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
    private readonly IAcademicLeaveRepository _leaveRepo;
    private readonly IExternalTransferRepository _transferRepo;
    private readonly IStudyPlanRepository _planRepo;
    private readonly IGetTimelineQueryHandler _timelineHandler;
    private readonly IGetClassmatesQueryHandler _classmatesHandler;
    private readonly IGetStudentGroupOnDateQueryHandler _groupOnDateHandler;

    public StudentService(
        IStudentRepository studentRepo,
        IEnrollmentRepository enrollmentRepo,
        IAcademicLeaveRepository leaveRepo,
        IExternalTransferRepository transferRepo,
        IStudyPlanRepository planRepo,
        IGetTimelineQueryHandler timelineHandler,
        IGetClassmatesQueryHandler classmatesHandler,
        IGetStudentGroupOnDateQueryHandler groupOnDateHandler)
    {
        _studentRepo       = studentRepo;
        _enrollmentRepo    = enrollmentRepo;
        _leaveRepo         = leaveRepo;
        _transferRepo      = transferRepo;
        _planRepo          = planRepo;
        _timelineHandler   = timelineHandler;
        _classmatesHandler = classmatesHandler;
        _groupOnDateHandler = groupOnDateHandler;
    }

    public async Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct);
        return student is null ? null : MapToDto(student);
    }

    public async Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var (items, count) = await _studentRepo.GetAllAsync(page, pageSize, ct);
        return new PagedResult<StudentDto>(items.Select(MapToDto), page, pageSize, count);
    }

    public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
    {
        var student = new Student
        {
            FirstName = dto.FirstName,
            LastName  = dto.LastName,
            BirthDate = dto.BirthDate,
            Email     = dto.Email,
            Phone     = dto.Phone,
            Status    = StudentStatus.Active
        };
        return MapToDto(await _studentRepo.AddAsync(student, ct));
    }

    public async Task<StudentDto> UpdateAsync(int studentId, StudentUpdateDto dto, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        student.FirstName = dto.FirstName;
        student.LastName  = dto.LastName;
        student.BirthDate = dto.BirthDate;
        student.Email     = dto.Email;
        student.Phone     = dto.Phone;

        await _studentRepo.UpdateAsync(student, ct);
        return MapToDto(student);
    }

    public async Task ChangeStatusAsync(int studentId, ChangeStatusDto dto, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        if (!Enum.TryParse<StudentStatus>(dto.Status, ignoreCase: true, out var newStatus))
            throw new DomainException($"Unknown status: '{dto.Status}'. Valid values: Active, OnLeave, Expelled, Graduated.");

        if (student.Status is StudentStatus.Expelled or StudentStatus.Graduated)
            throw new DomainException($"Cannot change status of a student with terminal status '{student.Status}'.");

        student.Status = newStatus;
        await _studentRepo.UpdateAsync(student, ct);
    }

    public Task<PagedResult<TimelineEventDto>> GetTimelineAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default) =>
        _timelineHandler.HandleAsync(new GetTimelineQuery(studentId, page, pageSize), ct);

    public Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(int studentId, DateOnly? dateFrom, DateOnly? dateTo, CancellationToken ct = default) =>
        _classmatesHandler.HandleAsync(new GetClassmatesQuery(studentId, dateFrom, dateTo), ct);

    public Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(int studentId, DateOnly? date, CancellationToken ct = default) =>
        _groupOnDateHandler.HandleAsync(
            new GetStudentGroupOnDateQuery(studentId, date ?? DateOnly.FromDateTime(DateTime.Today)), ct);

    public async Task<StudentDetailDto> GetDetailAsync(int studentId, CancellationToken ct = default)
    {
        var student = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollments = await _enrollmentRepo.GetByStudentIdAsync(studentId, ct);
        var plans       = await _planRepo.GetAssignmentsByStudentIdAsync(studentId, ct);
        var leaves      = await _leaveRepo.GetByStudentIdAsync(studentId, ct);
        var transfers   = await _transferRepo.GetByStudentIdAsync(studentId, ct);

        return new StudentDetailDto(
            student.StudentId,
            student.FirstName,
            student.LastName,
            student.BirthDate,
            student.Email,
            student.Phone,
            student.Status.ToString(),
            enrollments.Select(e => new EnrollmentSummaryDto(
                e.EnrollmentId,
                e.GroupId,
                e.Group.GroupCode,
                e.Group.Faculty,
                e.DateFrom,
                e.DateTo,
                e.SubgroupAssignment?.SubgroupId,
                e.SubgroupAssignment?.Subgroup.SubgroupName)),
            plans.Select(a => new StudyPlanAssignmentDto(
                a.AssignmentId, a.Plan.SpecialtyCode, a.Plan.PlanName, a.DateFrom, a.DateTo)),
            leaves.Select(l => new AcademicLeaveDto(l.LeaveId, l.StartDate, l.EndDate, l.Reason)),
            transfers.Select(t => new ExternalTransferDto(
                t.TransferId, t.TransferType.ToString(), t.TransferDate, t.Institution.InstitutionName, t.Notes))
        );
    }

    private static StudentDto MapToDto(Student s) =>
        new(s.StudentId, s.FirstName, s.LastName, s.BirthDate, s.Email, s.Phone, s.Status.ToString());
}
