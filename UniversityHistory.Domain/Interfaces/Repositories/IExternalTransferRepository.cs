using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IExternalTransferRepository
{
    Task<IEnumerable<ExternalTransfer>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<Institution?> GetInstitutionByIdAsync(int institutionId, CancellationToken ct = default);
    ExternalTransfer Add(ExternalTransfer transfer);
}
