using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken ct = default);
    Task<DepartmentDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, CancellationToken ct = default);
    Task<DepartmentDto> UpdateAsync(Guid id, UpdateDepartmentDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}

