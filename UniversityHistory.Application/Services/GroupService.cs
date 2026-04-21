using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetActiveGroups;
using UniversityHistory.Application.Queries.GetGroupComposition;
using UniversityHistory.Application.Queries.GetStudentsInGroup;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GroupService : IGroupService
{
    private readonly IGetGroupCompositionQueryHandler _compositionHandler;
    private readonly IGetActiveGroupsQueryHandler _activeGroupsHandler;
    private readonly IGetStudentsInGroupQueryHandler _studentsInGroupHandler;
    private readonly IGroupRepository _groupRepository;

    public GroupService(
        IGetGroupCompositionQueryHandler compositionHandler,
        IGetActiveGroupsQueryHandler activeGroupsHandler,
        IGetStudentsInGroupQueryHandler studentsInGroupHandler,
        IGroupRepository groupRepository)
    {
        _compositionHandler = compositionHandler;
        _activeGroupsHandler = activeGroupsHandler;
        _studentsInGroupHandler = studentsInGroupHandler;
        _groupRepository = groupRepository;
    }

    public Task<PagedResult<GroupCompositionMemberDto>> GetCompositionAsync(
        Guid groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default) =>
        _compositionHandler.HandleAsync(
            new GetGroupCompositionQuery(groupId, date ?? DateOnly.FromDateTime(DateTime.Today), page, pageSize), ct);

    public Task<IEnumerable<ActiveGroupDto>> GetActiveGroupsAsync(
        DateOnly? date = null, CancellationToken ct = default) =>
        _activeGroupsHandler.HandleAsync(
            new GetActiveGroupsQuery(date ?? DateOnly.FromDateTime(DateTime.Today)), ct);

    public Task<PagedResult<GroupStudentDto>> GetStudentsInGroupAsync(
        Guid groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default) =>
        _studentsInGroupHandler.HandleAsync(
            new GetStudentsInGroupQuery(groupId, date ?? DateOnly.FromDateTime(DateTime.Today), page, pageSize), ct);

    public async Task<IEnumerable<SubgroupDto>> GetSubgroupsAsync(Guid groupId, CancellationToken ct = default)
    {
        var group = await _groupRepository.GetByIdAsync(groupId, ct)
            ?? throw new KeyNotFoundException("Group not found.");

        return group.Subgroups
            .OrderBy(subgroup => subgroup.SubgroupName)
            .Select(subgroup => new SubgroupDto(subgroup.SubgroupId, subgroup.SubgroupName))
            .ToList();
    }
}

