using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class ExternalTransferRepository : IExternalTransferRepository
{
    private readonly UniversityDbContext _db;
    public ExternalTransferRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ExternalTransfer>> GetByStudentIdAsync(int studentId, CancellationToken ct = default)
    {
        return await _db.ExternalTransfers.AsNoTracking()
            .Include(t => t.Institution)
            .Where(t => t.StudentId == studentId)
            .OrderBy(t => t.TransferDate)
            .ToListAsync(ct);
    }

    public async Task<Institution?> GetInstitutionByIdAsync(int institutionId, CancellationToken ct = default)
    {
        return await _db.Institutions.FindAsync(new object[] { institutionId }, ct);
    }

    public ExternalTransfer Add(ExternalTransfer transfer)
    {
        _db.ExternalTransfers.Add(transfer);
        return transfer;
    }
}
