using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class InstitutionService : IInstitutionService
{
    private readonly IUnitOfWork _unitOfWork;

    public InstitutionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<InstitutionDto>> GetAllAsync(CancellationToken ct = default)
    {
        var institutions = await _unitOfWork.ExternalTransfers.GetInstitutionsAsync(ct);
        return institutions.Select(static institution => institution.ToDto());
    }

    public async Task<InstitutionDto> CreateAsync(string institutionName, CancellationToken ct = default)
    {
        var institution = new UniversityHistory.Domain.Entities.Institution
        {
            InstitutionName = institutionName.Trim()
        };

        _unitOfWork.ExternalTransfers.AddInstitution(institution);
        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(institution.InstitutionId, ct);
        return created!.ToDto();
    }
}
