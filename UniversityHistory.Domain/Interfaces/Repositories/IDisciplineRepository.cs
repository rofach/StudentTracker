using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IDisciplineRepository
{
    Task<IEnumerable<Discipline>> GetAllAsync(CancellationToken ct = default);
    Task<Discipline?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsWithNameAsync(string name, int? excludeId = null, CancellationToken ct = default);
    Task<Discipline> AddAsync(Discipline discipline, CancellationToken ct = default);
    Task UpdateAsync(Discipline discipline, CancellationToken ct = default);
    Task<bool> IsUsedInPlanAsync(int disciplineId, CancellationToken ct = default);
    Task DeleteAsync(Discipline discipline, CancellationToken ct = default);
}
