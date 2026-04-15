using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<StudentGroupEnrollment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    Task<StudentGroupEnrollment?> GetActiveByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetByGroupIdOnDateAsync(Guid groupId, DateOnly date, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetActiveByGroupIdAsync(Guid groupId, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetActiveByGroupIdOnDateAsync(Guid groupId, DateOnly date, CancellationToken ct = default);
    Task<bool> HasOverlapAsync(Guid studentId, DateOnly dateFrom, DateOnly? dateTo, Guid? excludeId = null, CancellationToken ct = default);
    StudentGroupEnrollment Add(StudentGroupEnrollment enrollment);
    void Update(StudentGroupEnrollment enrollment);
}



