using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class AcademicUnitRepository : IAcademicUnitRepository
{
    private readonly UniversityDbContext _db;

    public AcademicUnitRepository(UniversityDbContext db) => _db = db;

    public async Task<IEnumerable<AcademicUnit>> GetAllAsync(CancellationToken ct = default) =>
        await _db.AcademicUnits
            .AsNoTracking()
            .Include(u => u.Departments)
            .OrderBy(u => u.Name)
            .ToListAsync(ct);

    public async Task<AcademicUnit?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.AcademicUnits
            .Include(u => u.Departments)
            .FirstOrDefaultAsync(u => u.AcademicUnitId == id, ct);

    public async Task<bool> HasDepartmentsAsync(int academicUnitId, CancellationToken ct = default) =>
        await _db.Departments.AnyAsync(d => d.AcademicUnitId == academicUnitId, ct);

    public void Add(AcademicUnit unit) => _db.AcademicUnits.Add(unit);
    public void Update(AcademicUnit unit) => _db.AcademicUnits.Update(unit);
    public void Remove(AcademicUnit unit) => _db.AcademicUnits.Remove(unit);
}
