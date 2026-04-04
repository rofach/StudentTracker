using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IAcademicUnitService
{
    Task<IEnumerable<AcademicUnitDto>> GetAllAsync(CancellationToken ct = default);
    Task<AcademicUnitDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<AcademicUnitDto> CreateAsync(CreateAcademicUnitDto dto, CancellationToken ct = default);
    Task<AcademicUnitDto> UpdateAsync(int id, UpdateAcademicUnitDto dto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
