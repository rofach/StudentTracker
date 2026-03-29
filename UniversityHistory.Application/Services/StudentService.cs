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
    private readonly IGetTimelineQueryHandler _timelineHandler;
    private readonly IGetClassmatesQueryHandler _classmatesHandler;
    private readonly IGetStudentGroupOnDateQueryHandler _groupOnDateHandler;

    public StudentService(
        IStudentRepository studentRepo,
        IGetTimelineQueryHandler timelineHandler,
        IGetClassmatesQueryHandler classmatesHandler,
        IGetStudentGroupOnDateQueryHandler groupOnDateHandler)
    {
        _studentRepo       = studentRepo;
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

    public Task<PagedResult<TimelineEventDto>> GetTimelineAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        return _timelineHandler.HandleAsync(new GetTimelineQuery(studentId, page, pageSize), ct);
    }

    public Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(int studentId, DateOnly? dateFrom, DateOnly? dateTo, CancellationToken ct = default)
    {
        return _classmatesHandler.HandleAsync(new GetClassmatesQuery(studentId, dateFrom, dateTo), ct);
    }

    public Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(int studentId, DateOnly? date, CancellationToken ct = default)
    {
        return _groupOnDateHandler.HandleAsync(
            new GetStudentGroupOnDateQuery(studentId, date ?? DateOnly.FromDateTime(DateTime.Today)), ct);
    }

    private static StudentDto MapToDto(Student s)
    {
        return new(s.StudentId, s.FirstName, s.LastName, s.BirthDate, s.Email, s.Phone, s.Status.ToString());
    }
}
