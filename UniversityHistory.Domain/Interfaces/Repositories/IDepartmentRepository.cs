using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync(CancellationToken ct = default);
    Task<Department?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> HasGroupsAsync(int departmentId, CancellationToken ct = default);
    void Add(Department department);
    void Update(Department department);
    void Remove(Department department);
}
