using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetActiveGroups;
using UniversityHistory.Application.Queries.GetGroupComposition;
using UniversityHistory.Application.Queries.GetStudentsInGroup;
using UniversityHistory.Application.Utilities;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class GroupService : IGroupService
{
    private readonly IGetGroupCompositionQueryHandler _compositionHandler;
    private readonly IGetActiveGroupsQueryHandler _activeGroupsHandler;
    private readonly IGetStudentsInGroupQueryHandler _studentsInGroupHandler;
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GroupService(
        IGetGroupCompositionQueryHandler compositionHandler,
        IGetActiveGroupsQueryHandler activeGroupsHandler,
        IGetStudentsInGroupQueryHandler studentsInGroupHandler,
        IGroupRepository groupRepository,
        IUnitOfWork unitOfWork)
    {
        _compositionHandler = compositionHandler;
        _activeGroupsHandler = activeGroupsHandler;
        _studentsInGroupHandler = studentsInGroupHandler;
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
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

    public async Task<ActiveGroupDto> CreateGroupAsync(CreateGroupDto dto, CancellationToken ct = default)
    {
        if (await _groupRepository.GroupCodeExistsAsync(dto.GroupCode, null, ct))
            throw new DomainException($"Group with code '{dto.GroupCode}' already exists.");

        var group = new StudyGroup
        {
            GroupCode = dto.GroupCode.Trim(),
            DepartmentId = dto.DepartmentId,
            DateCreated = dto.DateCreated
        };

        _groupRepository.Add(group);
        await _unitOfWork.SaveChangesAsync(ct);

        var saved = await _groupRepository.GetByIdAsync(group.GroupId, ct)
            ?? throw new InvalidOperationException("Failed to reload group after creation.");

        return ToActiveDto(saved);
    }

    public async Task<ActiveGroupDto> UpdateGroupAsync(Guid groupId, UpdateGroupDto dto, CancellationToken ct = default)
    {
        var group = await _groupRepository.GetByIdAsync(groupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), groupId);

        if (!string.Equals(group.GroupCode, dto.GroupCode, StringComparison.OrdinalIgnoreCase) &&
            await _groupRepository.GroupCodeExistsAsync(dto.GroupCode, groupId, ct))
            throw new DomainException($"Group with code '{dto.GroupCode}' already exists.");

        group.GroupCode = dto.GroupCode.Trim();
        _groupRepository.Update(group);
        await _unitOfWork.SaveChangesAsync(ct);

        return ToActiveDto(group);
    }

    private static ActiveGroupDto ToActiveDto(StudyGroup g) =>
        new(g.GroupId, g.GroupCode,
            g.Department?.Name ?? string.Empty,
            g.Department?.AcademicUnit?.Name ?? string.Empty,
            g.Department?.AcademicUnit?.Type.ToString() ?? string.Empty,
            g.DateCreated,
            g.DateClosed,
            CourseYearCalculator.Calculate(g.DateCreated));
}

