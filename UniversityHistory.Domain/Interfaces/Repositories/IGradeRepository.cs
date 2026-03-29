using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<(IEnumerable<GradeRecord> Items, int TotalCount)> GetByStudentIdAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<GradeRecord> AddAsync(GradeRecord grade, CancellationToken ct = default);
}
