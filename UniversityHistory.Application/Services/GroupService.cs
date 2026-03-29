using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetActiveGroups;
using UniversityHistory.Application.Queries.GetGroupComposition;
using UniversityHistory.Application.Queries.GetStudentsInGroup;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepo;
    private readonly IGetGroupCompositionQueryHandler _compositionHandler;
    private readonly IGetActiveGroupsQueryHandler _activeGroupsHandler;
    private readonly IGetStudentsInGroupQueryHandler _studentsInGroupHandler;

    public GroupService(
        IGroupRepository groupRepo,
        IGetGroupCompositionQueryHandler compositionHandler,
        IGetActiveGroupsQueryHandler activeGroupsHandler,
        IGetStudentsInGroupQueryHandler studentsInGroupHandler)
    {
        _groupRepo = groupRepo;
        _compositionHandler = compositionHandler;
        _activeGroupsHandler = activeGroupsHandler;
        _studentsInGroupHandler = studentsInGroupHandler;
    }

    public Task<PagedResult<GroupCompositionMemberDto>> GetCompositionAsync(
        int groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default) =>
        _compositionHandler.HandleAsync(
            new GetGroupCompositionQuery(groupId, date ?? DateOnly.FromDateTime(DateTime.Today), page, pageSize), ct);

    public Task<IEnumerable<ActiveGroupDto>> GetActiveGroupsAsync(
        DateOnly? date = null, CancellationToken ct = default) =>
        _activeGroupsHandler.HandleAsync(
            new GetActiveGroupsQuery(date ?? DateOnly.FromDateTime(DateTime.Today)), ct);

    public Task<PagedResult<GroupStudentDto>> GetStudentsInGroupAsync(
        int groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default) =>
        _studentsInGroupHandler.HandleAsync(
            new GetStudentsInGroupQuery(groupId, date ?? DateOnly.FromDateTime(DateTime.Today), page, pageSize), ct);
}
