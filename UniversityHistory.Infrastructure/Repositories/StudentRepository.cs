using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Common;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly UniversityDbContext _db;
    public StudentRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Students.FindAsync(new object[] { id }, ct);
    }

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

    public Student Add(Student student)
    {
        _db.Students.Add(student);
        return student;
    }

    public void Update(Student student)
    {
        _db.Students.Update(student);
    }

    public void Delete(Student student)
    {
        _db.Students.Remove(student);
    }
}

