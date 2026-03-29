using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetGroupComposition;

public interface IGetGroupCompositionQueryHandler
{
    Task<PagedResult<GroupCompositionMemberDto>> HandleAsync(GetGroupCompositionQuery query, CancellationToken ct = default);
}
