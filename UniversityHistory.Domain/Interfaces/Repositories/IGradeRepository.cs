using UniversityHistory.Domain.Common;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<PagedData<GradeRecord>> GetByStudentIdAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<GradeRecord> AddAsync(GradeRecord grade, CancellationToken ct = default);
}
