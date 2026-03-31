using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<StudyGroup?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<StudyGroup>> GetAllAsync(CancellationToken ct = default);
    StudyGroup Add(StudyGroup group);
}
