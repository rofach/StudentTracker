using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGroupPlanAssignmentRepository
{
    Task<GroupPlanAssignment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<GroupPlanAssignment?> GetActiveOnDateAsync(Guid groupId, DateOnly date, CancellationToken ct = default);
    Task<IEnumerable<GroupPlanAssignment>> GetByGroupIdAsync(Guid groupId, CancellationToken ct = default);
    Task<bool> HasOverlapAsync(Guid groupId, DateOnly dateFrom, DateOnly? dateTo, Guid? excludeId = null, CancellationToken ct = default);
    Task<bool> HasCourseEnrollmentsAsync(Guid planId, CancellationToken ct = default);
    GroupPlanAssignment Add(GroupPlanAssignment assignment);
    void Update(GroupPlanAssignment assignment);
}


