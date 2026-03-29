using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IAcademicLeaveRepository
{
    Task<IEnumerable<AcademicLeave>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<AcademicLeave?> GetOpenByEnrollmentIdAsync(int enrollmentId, CancellationToken ct = default);
    Task<AcademicLeave> AddAsync(AcademicLeave leave, CancellationToken ct = default);
}
