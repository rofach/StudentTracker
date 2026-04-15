using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class GroupPlanAssignmentRepository : IGroupPlanAssignmentRepository
{
    private readonly UniversityDbContext _db;
    public GroupPlanAssignmentRepository(UniversityDbContext db) => _db = db;

    public async Task<GroupPlanAssignment?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _db.GroupPlanAssignments
            .Include(a => a.Plan).ThenInclude(p => p.PlanDisciplines)
            .FirstOrDefaultAsync(a => a.GroupPlanAssignmentId == id, ct);
    }

    public async Task<GroupPlanAssignment?> GetActiveOnDateAsync(int groupId, DateOnly date, CancellationToken ct = default)
    {
        return await _db.GroupPlanAssignments
            .Include(a => a.Plan).ThenInclude(p => p.PlanDisciplines)
            .Where(a => a.GroupId == groupId
                     && a.DateFrom <= date
                     && (a.DateTo == null || a.DateTo >= date))
            .OrderByDescending(a => a.DateFrom)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<GroupPlanAssignment>> GetByGroupIdAsync(int groupId, CancellationToken ct = default)
    {
        return await _db.GroupPlanAssignments
            .AsNoTracking()
            .Include(a => a.Plan)
            .Where(a => a.GroupId == groupId)
            .OrderBy(a => a.DateFrom)
            .ToListAsync(ct);
    }

    public async Task<bool> HasOverlapAsync(int groupId, DateOnly dateFrom, DateOnly? dateTo, int? excludeId = null, CancellationToken ct = default)
    {
        return await _db.GroupPlanAssignments.AnyAsync(a =>
            a.GroupId == groupId
            && (excludeId == null || a.GroupPlanAssignmentId != excludeId)
            && a.DateFrom < (dateTo ?? DateOnly.MaxValue)
            && (a.DateTo == null || a.DateTo > dateFrom), ct);
    }

    public async Task<bool> HasCourseEnrollmentsAsync(int planId, CancellationToken ct = default)
    {
        return await _db.StudentCourseEnrollments
            .AnyAsync(ce => ce.GroupPlanAssignment.PlanId == planId, ct);
    }

    public GroupPlanAssignment Add(GroupPlanAssignment assignment)
    {
        _db.GroupPlanAssignments.Add(assignment);
        return assignment;
    }

    public void Update(GroupPlanAssignment assignment)
    {
        _db.GroupPlanAssignments.Update(assignment);
    }
}
