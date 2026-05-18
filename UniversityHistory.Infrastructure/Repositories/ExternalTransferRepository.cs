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

    public async Task<IEnumerable<ExternalTransfer>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default)
    {
        return await _db.ExternalTransfers.AsNoTracking()
            .Include(t => t.Institution)
            .Where(t => t.StudentId == studentId)
            .OrderBy(t => t.TransferDate)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Institution>> GetInstitutionsAsync(CancellationToken ct = default)
    {
        return await _db.Institutions.AsNoTracking()
            .OrderBy(i => i.InstitutionName)
            .ToListAsync(ct);
    }

    public async Task<Institution?> GetInstitutionByIdAsync(Guid institutionId, CancellationToken ct = default)
    {
        return await _db.Institutions
            .FirstOrDefaultAsync(i => i.InstitutionId == institutionId, ct);
    }

    public ExternalTransfer Add(ExternalTransfer transfer)
    {
        _db.ExternalTransfers.Add(transfer);
        return transfer;
    }
    
    public Institution AddInstitution(Institution institution)
    {
        _db.Institutions.Add(institution);
        return institution;
    }
}

