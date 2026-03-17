using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IExternalTransferRepository
{
    Task<IEnumerable<ExternalTransfer>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<ExternalTransfer> AddAsync(ExternalTransfer transfer, CancellationToken ct = default);
}
