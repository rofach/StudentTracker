using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface ISubgroupAssignmentRepository
{
    Task<StudentSubgroupAssignment?> GetByEnrollmentIdAsync(Guid enrollmentId, CancellationToken ct = default);
    void Upsert(StudentSubgroupAssignment? existing, StudentSubgroupAssignment assignment);
}

