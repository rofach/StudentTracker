using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class DisciplineRepository : IDisciplineRepository
{
    private readonly UniversityDbContext _db;
    public DisciplineRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Discipline>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Disciplines.AsNoTracking().OrderBy(d => d.DisciplineName).ToListAsync(ct);
    }

    public async Task<Discipline?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Disciplines.FindAsync(new object[] { id }, ct);
    }

    public async Task<bool> ExistsWithNameAsync(string name, Guid? excludeId = null, CancellationToken ct = default)
    {
        return await _db.Disciplines.AnyAsync(
            d => d.DisciplineName == name && (excludeId == null || d.DisciplineId != excludeId), ct);
    }

    public Discipline Add(Discipline discipline)
    {
        _db.Disciplines.Add(discipline);
        return discipline;
    }

    public void Update(Discipline discipline)
    {
        _db.Disciplines.Update(discipline);
    }

    public async Task<bool> IsUsedInPlanAsync(Guid disciplineId, CancellationToken ct = default)
    {
        return await _db.PlanDisciplines.AnyAsync(pd => pd.DisciplineId == disciplineId, ct);
    }

    public void Delete(Discipline discipline)
    {
        _db.Disciplines.Remove(discipline);
    }
}


