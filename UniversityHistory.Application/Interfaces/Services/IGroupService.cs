using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IGroupService
{
    Task<PagedResult<GroupCompositionMemberDto>> GetCompositionAsync(Guid groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<IEnumerable<ActiveGroupDto>> GetActiveGroupsAsync(DateOnly? date = null, CancellationToken ct = default);
    Task<PagedResult<GroupStudentDto>> GetStudentsInGroupAsync(Guid groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default);
}

