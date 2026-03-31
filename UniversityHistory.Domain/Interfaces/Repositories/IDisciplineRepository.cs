using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IDisciplineRepository
{
    Task<IEnumerable<Discipline>> GetAllAsync(CancellationToken ct = default);
    Task<Discipline?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsWithNameAsync(string name, int? excludeId = null, CancellationToken ct = default);
    Discipline Add(Discipline discipline);
    void Update(Discipline discipline);
    Task<bool> IsUsedInPlanAsync(int disciplineId, CancellationToken ct = default);
    void Delete(Discipline discipline);
}
