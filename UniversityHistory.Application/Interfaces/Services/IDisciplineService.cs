using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IDisciplineService
{
    Task<IEnumerable<DisciplineDto>> GetAllAsync(CancellationToken ct = default);
    Task<PagedResult<DisciplineSearchItemDto>> SearchAsync(string? name, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<DisciplineDto?> GetByIdAsync(Guid disciplineId, CancellationToken ct = default);
    Task<DisciplineDto> CreateAsync(CreateDisciplineDto dto, CancellationToken ct = default);
    Task<DisciplineDto> UpdateAsync(Guid disciplineId, UpdateDisciplineDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid disciplineId, CancellationToken ct = default);
}

