using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class SubgroupAssignmentRepository : ISubgroupAssignmentRepository
{
    private readonly UniversityDbContext _db;
    public SubgroupAssignmentRepository(UniversityDbContext db) => _db = db;

    public async Task<StudentSubgroupAssignment?> GetByEnrollmentIdAsync(int enrollmentId, CancellationToken ct = default) =>
        await _db.StudentSubgroupAssignments
            .FirstOrDefaultAsync(sa => sa.EnrollmentId == enrollmentId, ct);

    public async Task UpsertAsync(StudentSubgroupAssignment assignment, CancellationToken ct = default)
    {
        var existing = await GetByEnrollmentIdAsync(assignment.EnrollmentId, ct);
        if (existing is null)
            _db.StudentSubgroupAssignments.Add(assignment);
        else
            existing.SubgroupId = assignment.SubgroupId;
    }
}
