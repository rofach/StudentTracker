using UniversityHistory.Domain.Common;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<PagedData<GradeRecord>> GetByStudentIdAsync(Guid studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    GradeRecord Add(GradeRecord grade);
}

