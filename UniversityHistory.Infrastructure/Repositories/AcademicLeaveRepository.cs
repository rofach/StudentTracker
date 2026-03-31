using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class AcademicLeaveRepository : IAcademicLeaveRepository
{
    private readonly UniversityDbContext _db;
    public AcademicLeaveRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AcademicLeave>> GetByStudentIdAsync(int studentId, CancellationToken ct = default)
    {
        return await _db.AcademicLeaves.AsNoTracking()
            .Where(l => l.Enrollment.StudentId == studentId)
            .OrderBy(l => l.StartDate)
            .ToListAsync(ct);
    }

    public async Task<AcademicLeave?> GetOpenByEnrollmentIdAsync(int enrollmentId, CancellationToken ct = default)
    {
        return await _db.AcademicLeaves
            .FirstOrDefaultAsync(l => l.EnrollmentId == enrollmentId && l.EndDate == null, ct);
    }

    public AcademicLeave Add(AcademicLeave leave)
    {
        _db.AcademicLeaves.Add(leave);
        return leave;
    }
}
