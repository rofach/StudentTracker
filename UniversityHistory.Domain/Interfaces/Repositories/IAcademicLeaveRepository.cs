using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IAcademicLeaveRepository
{
    Task<IEnumerable<AcademicLeave>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<AcademicLeave?> GetByIdAsync(int leaveId, CancellationToken ct = default);
    Task<AcademicLeave?> GetOpenByEnrollmentIdAsync(int enrollmentId, CancellationToken ct = default);
    AcademicLeave Add(AcademicLeave leave);
    void Update(AcademicLeave leave);
}
