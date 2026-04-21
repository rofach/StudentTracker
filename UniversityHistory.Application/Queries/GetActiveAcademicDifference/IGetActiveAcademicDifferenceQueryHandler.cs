using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetActiveAcademicDifference;

public interface IGetActiveAcademicDifferenceQueryHandler
{
    Task<PagedResult<ActiveAcademicDifferenceDto>> HandleAsync(
        GetActiveAcademicDifferenceQuery query,
        CancellationToken ct = default);
}
