using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<IEnumerable<GradeRecord>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<GradeRecord> AddAsync(GradeRecord grade, CancellationToken ct = default);
}
