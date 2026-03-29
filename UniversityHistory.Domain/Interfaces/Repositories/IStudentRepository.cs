using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<(IEnumerable<Student> Items, int TotalCount)> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<Student> AddAsync(Student student, CancellationToken ct = default);
    Task UpdateAsync(Student student, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
