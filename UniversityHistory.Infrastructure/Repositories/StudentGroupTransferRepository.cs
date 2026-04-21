using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class StudentGroupTransferRepository : IStudentGroupTransferRepository
{
    private readonly UniversityDbContext _db;
    public StudentGroupTransferRepository(UniversityDbContext db) => _db = db;

    public StudentGroupTransfer Add(StudentGroupTransfer transfer)
    {
        _db.StudentGroupTransfers.Add(transfer);
        return transfer;
    }

    public void AddDifferenceItems(IEnumerable<AcademicDifferenceItem> items)
    {
        _db.AcademicDifferenceItems.AddRange(items);
    }

    public async Task<StudentGroupTransfer?> GetByIdAsync(Guid transferId, CancellationToken ct = default)
    {
        return await _db.StudentGroupTransfers
            .Include(t => t.OldEnrollment).ThenInclude(e => e.Group)
            .Include(t => t.NewEnrollment).ThenInclude(e => e.Group)
            .Include(t => t.DifferenceItems)
                .ThenInclude(d => d.PlanDiscipline)
                    .ThenInclude(pd => pd.Discipline)
            .FirstOrDefaultAsync(t => t.TransferId == transferId, ct);
    }

    public async Task<IEnumerable<StudentGroupTransfer>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default)
    {
        return await _db.StudentGroupTransfers
            .AsNoTracking()
            .Include(t => t.OldEnrollment).ThenInclude(e => e.Group)
            .Include(t => t.NewEnrollment).ThenInclude(e => e.Group)
            .Include(t => t.DifferenceItems)
            .Where(t => t.OldEnrollment.StudentId == studentId)
            .OrderByDescending(t => t.TransferDate)
            .ToListAsync(ct);
    }

    public async Task<AcademicDifferenceItem?> GetDifferenceItemByIdAsync(Guid differenceItemId, CancellationToken ct = default)
    {
        return await _db.AcademicDifferenceItems
            .Include(d => d.Transfer)
                .ThenInclude(t => t.OldEnrollment)
            .Include(d => d.PlanDiscipline).ThenInclude(pd => pd.Discipline)
            .FirstOrDefaultAsync(d => d.DifferenceItemId == differenceItemId, ct);
    }

    public async Task<IEnumerable<AcademicDifferenceItem>> GetOpenDifferenceItemsByStudentAndPlanDisciplineAsync(
        Guid studentId,
        Guid planDisciplineId,
        CancellationToken ct = default)
    {
        return await _db.AcademicDifferenceItems
            .Include(d => d.Transfer)
                .ThenInclude(t => t.OldEnrollment)
            .Where(d => d.PlanDisciplineId == planDisciplineId
                     && d.Transfer.OldEnrollment.StudentId == studentId
                     && d.Status != DifferenceItemStatus.Completed)
            .ToListAsync(ct);
    }

    public void UpdateDifferenceItem(AcademicDifferenceItem item)
    {
        _db.AcademicDifferenceItems.Update(item);
    }
}
