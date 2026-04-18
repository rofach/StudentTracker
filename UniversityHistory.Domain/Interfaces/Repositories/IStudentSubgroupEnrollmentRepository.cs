using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudentSubgroupEnrollmentRepository
{
    Task<StudentSubgroupEnrollment?> GetOpenByEnrollmentIdAsync(Guid enrollmentId, CancellationToken ct = default);
    StudentSubgroupEnrollment Add(StudentSubgroupEnrollment subgroupEnrollment);
    void Update(StudentSubgroupEnrollment subgroupEnrollment);
}
