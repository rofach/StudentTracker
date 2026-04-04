using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly UniversityDbContext _db;

    public DepartmentRepository(UniversityDbContext db) => _db = db;

    public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken ct = default) =>
        await _db.Departments
            .AsNoTracking()
            .Include(d => d.AcademicUnit)
            .OrderBy(d => d.AcademicUnit.Name)
            .ThenBy(d => d.Name)
            .ToListAsync(ct);

    public async Task<Department?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.Departments
            .Include(d => d.AcademicUnit)
            .FirstOrDefaultAsync(d => d.DepartmentId == id, ct);

    public async Task<bool> HasGroupsAsync(int departmentId, CancellationToken ct = default) =>
        await _db.StudyGroups.AnyAsync(g => g.DepartmentId == departmentId, ct);

    public void Add(Department department) => _db.Departments.Add(department);
    public void Update(Department department) => _db.Departments.Update(department);
    public void Remove(Department department) => _db.Departments.Remove(department);
}
