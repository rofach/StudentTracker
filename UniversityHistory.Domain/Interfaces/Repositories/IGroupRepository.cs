using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<StudyGroup?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<StudyGroup>> GetAllAsync(CancellationToken ct = default);
    Task<bool> GroupCodeExistsAsync(string groupCode, Guid? excludeId, CancellationToken ct = default);
    StudyGroup Add(StudyGroup group);
    void Update(StudyGroup group);
}

