using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetGroupComposition;

public interface IGetGroupCompositionQueryHandler
{
    Task<IEnumerable<GroupCompositionMemberDto>> HandleAsync(GetGroupCompositionQuery query, CancellationToken ct = default);
}
