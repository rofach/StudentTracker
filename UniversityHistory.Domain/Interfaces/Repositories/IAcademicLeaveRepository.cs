using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IAcademicLeaveRepository
{
    Task<IEnumerable<AcademicLeave>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    Task<AcademicLeave?> GetByIdAsync(Guid leaveId, CancellationToken ct = default);
    Task<AcademicLeave?> GetOpenByEnrollmentIdAsync(Guid enrollmentId, CancellationToken ct = default);
    Task<bool> HasActiveLeaveOnDateAsync(Guid enrollmentId, DateOnly date, CancellationToken ct = default);
    Task<bool> HasOverlapAsync(
        Guid enrollmentId,
        DateOnly startDate,
        DateOnly? endDate,
        Guid? excludeLeaveId = null,
        CancellationToken ct = default);
    AcademicLeave Add(AcademicLeave leave);
    void Update(AcademicLeave leave);
}


