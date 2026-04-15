using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IDisciplineRepository
{
    Task<IEnumerable<Discipline>> GetAllAsync(CancellationToken ct = default);
    Task<Discipline?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsWithNameAsync(string name, Guid? excludeId = null, CancellationToken ct = default);
    Discipline Add(Discipline discipline);
    void Update(Discipline discipline);
    Task<bool> IsUsedInPlanAsync(Guid disciplineId, CancellationToken ct = default);
    void Delete(Discipline discipline);
}


