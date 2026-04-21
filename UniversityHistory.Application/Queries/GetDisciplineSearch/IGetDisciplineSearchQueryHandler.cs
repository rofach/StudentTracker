using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetDisciplineSearch;

public interface IGetDisciplineSearchQueryHandler
{
    Task<PagedResult<DisciplineSearchItemDto>> HandleAsync(
        GetDisciplineSearchQuery query,
        CancellationToken ct = default);
}
