using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<StudentGroupEnrollment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupEnrollment>> GetByGroupIdOnDateAsync(int groupId, DateOnly date, CancellationToken ct = default);
    Task<bool> HasOverlapAsync(int studentId, DateOnly dateFrom, DateOnly? dateTo, int? excludeId = null, CancellationToken ct = default);
    Task<StudentGroupEnrollment> AddAsync(StudentGroupEnrollment enrollment, CancellationToken ct = default);
    Task UpdateAsync(StudentGroupEnrollment enrollment, CancellationToken ct = default);
}
