using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetGroupComposition;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepo;
    private readonly IGetGroupCompositionQueryHandler _compositionHandler;

    public GroupService(IGroupRepository groupRepo, IGetGroupCompositionQueryHandler compositionHandler)
    {
        _groupRepo          = groupRepo;
        _compositionHandler = compositionHandler;
    }

    public Task<IEnumerable<GroupCompositionMemberDto>> GetCompositionAsync(
        int groupId, DateOnly? date = null, CancellationToken ct = default)
    {
        return _compositionHandler.HandleAsync(
            new GetGroupCompositionQuery(groupId, date ?? DateOnly.FromDateTime(DateTime.Today)), ct);
    }
}
