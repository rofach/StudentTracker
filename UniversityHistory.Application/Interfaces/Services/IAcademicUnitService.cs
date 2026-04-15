using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IAcademicUnitService
{
    Task<IEnumerable<AcademicUnitDto>> GetAllAsync(CancellationToken ct = default);
    Task<AcademicUnitDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AcademicUnitDto> CreateAsync(CreateAcademicUnitDto dto, CancellationToken ct = default);
    Task<AcademicUnitDto> UpdateAsync(Guid id, UpdateAcademicUnitDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}

