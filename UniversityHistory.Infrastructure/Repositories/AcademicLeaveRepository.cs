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

    public async Task<AcademicLeave?> GetByIdAsync(int leaveId, CancellationToken ct = default)
    {
        return await _db.AcademicLeaves
            .Include(l => l.Enrollment)
                .ThenInclude(e => e.Student)
            .FirstOrDefaultAsync(l => l.LeaveId == leaveId, ct);
    }

    public async Task<AcademicLeave?> GetOpenByEnrollmentIdAsync(int enrollmentId, CancellationToken ct = default)
    {
        return await _db.AcademicLeaves
            .FirstOrDefaultAsync(l => l.EnrollmentId == enrollmentId && l.EndDate == null, ct);
    }

    public async Task<bool> HasActiveLeaveOnDateAsync(int enrollmentId, DateOnly date, CancellationToken ct = default)
    {
        return await _db.AcademicLeaves
            .AnyAsync(
                l => l.EnrollmentId == enrollmentId
                     && l.StartDate <= date
                     && (!l.EndDate.HasValue || l.EndDate.Value >= date),
                ct);
    }

    public async Task<bool> HasOverlapAsync(
        int enrollmentId,
        DateOnly startDate,
        DateOnly? endDate,
        int? excludeLeaveId = null,
        CancellationToken ct = default)
    {
        var overlapEnd = endDate ?? new DateOnly(9999, 12, 31);

        return await _db.AcademicLeaves.AnyAsync(
            l => l.EnrollmentId == enrollmentId
                 && (!excludeLeaveId.HasValue || l.LeaveId != excludeLeaveId.Value)
                 && l.StartDate <= overlapEnd
                 && (l.EndDate == null || l.EndDate >= startDate),
            ct);
    }

    public AcademicLeave Add(AcademicLeave leave)
    {
        _db.AcademicLeaves.Add(leave);
        return leave;
    }

    public void Update(AcademicLeave leave)
    {
        _db.AcademicLeaves.Update(leave);
    }
}
