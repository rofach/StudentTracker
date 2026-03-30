using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudentService
{
    Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default);
    Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default);
    Task<StudentDto> UpdateAsync(int studentId, StudentUpdateDto dto, CancellationToken ct = default);
    Task ChangeStatusAsync(int studentId, ChangeStatusDto dto, CancellationToken ct = default);
    Task<StudentDetailDto> GetDetailAsync(int studentId, CancellationToken ct = default);
    Task<PagedResult<TimelineEventDto>> GetTimelineAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(int studentId, DateOnly? dateFrom, DateOnly? dateTo, CancellationToken ct = default);
    Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(int studentId, DateOnly? date, CancellationToken ct = default);
}
