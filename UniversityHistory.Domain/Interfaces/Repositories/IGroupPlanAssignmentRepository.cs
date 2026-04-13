using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IGroupPlanAssignmentRepository
{
    Task<GroupPlanAssignment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<GroupPlanAssignment?> GetActiveOnDateAsync(int groupId, DateOnly date, CancellationToken ct = default);
    Task<IEnumerable<GroupPlanAssignment>> GetByGroupIdAsync(int groupId, CancellationToken ct = default);
    Task<bool> HasOverlapAsync(int groupId, DateOnly dateFrom, DateOnly? dateTo, int? excludeId = null, CancellationToken ct = default);
    Task<bool> HasCourseEnrollmentsAsync(int planId, CancellationToken ct = default);
    GroupPlanAssignment Add(GroupPlanAssignment assignment);
    void Update(GroupPlanAssignment assignment);
}
