using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IInstitutionService
{
    Task<IEnumerable<InstitutionDto>> GetAllAsync(CancellationToken ct = default);
    Task<InstitutionDto> CreateAsync(string institutionName, CancellationToken ct = default);
}
