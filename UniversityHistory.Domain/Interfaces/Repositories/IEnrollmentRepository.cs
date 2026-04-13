using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<StudentGroupEnrollment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<StudentGroupEnrollment?> GetActiveByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetByGroupIdOnDateAsync(int groupId, DateOnly date, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetActiveByGroupIdAsync(int groupId, CancellationToken ct = default);
    Task<bool> HasOverlapAsync(int studentId, DateOnly dateFrom, DateOnly? dateTo, int? excludeId = null, CancellationToken ct = default);
    StudentGroupEnrollment Add(StudentGroupEnrollment enrollment);
    void Update(StudentGroupEnrollment enrollment);
}
