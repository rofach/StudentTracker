using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class DisciplineRepository : IDisciplineRepository
{
    private readonly UniversityDbContext _db;
    public DisciplineRepository(UniversityDbContext db) => _db = db;

    public async Task<IEnumerable<Discipline>> GetAllAsync(CancellationToken ct = default) =>
        await _db.Disciplines.AsNoTracking().OrderBy(d => d.DisciplineName).ToListAsync(ct);

    public async Task<Discipline?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.Disciplines.FindAsync(new object[] { id }, ct);

    public async Task<bool> ExistsWithNameAsync(string name, int? excludeId = null, CancellationToken ct = default) =>
        await _db.Disciplines.AnyAsync(
            d => d.DisciplineName == name && (excludeId == null || d.DisciplineId != excludeId), ct);

    public async Task<Discipline> AddAsync(Discipline discipline, CancellationToken ct = default)
    {
        _db.Disciplines.Add(discipline);
        await _db.SaveChangesAsync(ct);
        return discipline;
    }

    public async Task UpdateAsync(Discipline discipline, CancellationToken ct = default)
    {
        _db.Disciplines.Update(discipline);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> IsUsedInPlanAsync(int disciplineId, CancellationToken ct = default) =>
        await _db.PlanDisciplines.AnyAsync(pd => pd.DisciplineId == disciplineId, ct);

    public async Task DeleteAsync(Discipline discipline, CancellationToken ct = default)
    {
        _db.Disciplines.Remove(discipline);
        await _db.SaveChangesAsync(ct);
    }
}
