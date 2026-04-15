using UniversityHistory.Domain.Common;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedData<Student>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
    Student Add(Student student);
    void Update(Student student);
    void Delete(Student student);
}

