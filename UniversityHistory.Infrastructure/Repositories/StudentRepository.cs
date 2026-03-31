using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Common;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly UniversityDbContext _db;
    public StudentRepository(UniversityDbContext db) => _db = db;

    public async Task<Student?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.Students.FindAsync(new object[] { id }, ct);

    public async Task<PagedData<Student>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        var query = _db.Students.AsNoTracking();
        var count = await query.CountAsync(ct);
        var items = await query.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync(ct);
        return new PagedData<Student>(items, count);
    }

    public async Task<Student> AddAsync(Student student, CancellationToken ct = default)
    {
        _db.Students.Add(student);
        return await Task.FromResult(student);
    }

    public Task UpdateAsync(Student student, CancellationToken ct = default)
    {
        _db.Students.Update(student);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var student = await GetByIdAsync(id, ct);
        if (student is not null)
        {
            _db.Students.Remove(student);
        }
    }
}
