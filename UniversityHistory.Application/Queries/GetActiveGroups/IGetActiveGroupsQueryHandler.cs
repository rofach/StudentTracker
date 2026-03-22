using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetActiveGroups;

public interface IGetActiveGroupsQueryHandler
{
    Task<IEnumerable<ActiveGroupDto>> HandleAsync(GetActiveGroupsQuery query, CancellationToken ct = default);
}
