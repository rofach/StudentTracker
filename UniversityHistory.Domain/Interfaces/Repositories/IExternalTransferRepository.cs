using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IExternalTransferRepository
{
    Task<IEnumerable<ExternalTransfer>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    Task<Institution?> GetInstitutionByIdAsync(Guid institutionId, CancellationToken ct = default);
    ExternalTransfer Add(ExternalTransfer transfer);
}

