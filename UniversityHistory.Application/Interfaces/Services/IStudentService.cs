using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudentService
{
    Task<StudentDto?> GetByIdAsync(Guid studentId, CancellationToken ct = default);
    Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<PagedResult<StudentDto>> SearchAsync(string? fullName, string? email, string? status, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default);
    Task<StudentDto> UpdateAsync(Guid studentId, StudentUpdateDto dto, CancellationToken ct = default);
    Task ChangeStatusAsync(Guid studentId, ChangeStatusDto dto, CancellationToken ct = default);
    Task<StudentDetailDto> GetDetailAsync(Guid studentId, CancellationToken ct = default);
    Task<PagedResult<TimelineEventDto>> GetTimelineAsync(Guid studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(Guid studentId, DateOnly? dateFrom, DateOnly? dateTo, CancellationToken ct = default);
    Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(Guid studentId, DateOnly? date, CancellationToken ct = default);
}

