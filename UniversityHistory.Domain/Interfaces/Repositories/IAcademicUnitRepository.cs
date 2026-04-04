using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IAcademicUnitRepository
{
    Task<IEnumerable<AcademicUnit>> GetAllAsync(CancellationToken ct = default);
    Task<AcademicUnit?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> HasDepartmentsAsync(int academicUnitId, CancellationToken ct = default);
    void Add(AcademicUnit unit);
    void Update(AcademicUnit unit);
    void Remove(AcademicUnit unit);
}
