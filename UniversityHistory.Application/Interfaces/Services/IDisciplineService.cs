using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IDisciplineService
{
    Task<IEnumerable<DisciplineDto>> GetAllAsync(CancellationToken ct = default);
    Task<DisciplineDto?> GetByIdAsync(int disciplineId, CancellationToken ct = default);
    Task<DisciplineDto> CreateAsync(CreateDisciplineDto dto, CancellationToken ct = default);
    Task<DisciplineDto> UpdateAsync(int disciplineId, UpdateDisciplineDto dto, CancellationToken ct = default);
    Task DeleteAsync(int disciplineId, CancellationToken ct = default);
}
