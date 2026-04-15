using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class SubgroupAssignmentRepository : ISubgroupAssignmentRepository
{
    private readonly UniversityDbContext _db;
    public SubgroupAssignmentRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<StudentSubgroupAssignment?> GetByEnrollmentIdAsync(Guid enrollmentId, CancellationToken ct = default)
    {
        return await _db.StudentSubgroupAssignments
            .FirstOrDefaultAsync(sa => sa.EnrollmentId == enrollmentId, ct);
    }

    public void Upsert(StudentSubgroupAssignment? existing, StudentSubgroupAssignment assignment)
    {
        if (existing is null)
        {
            _db.StudentSubgroupAssignments.Add(assignment);
        }
        else
        {
            existing.SubgroupId = assignment.SubgroupId;
        }
    }
}

