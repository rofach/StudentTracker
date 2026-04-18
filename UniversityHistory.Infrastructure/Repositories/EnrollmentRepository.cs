using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly UniversityDbContext _db;

    public EnrollmentRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<StudentGroupEnrollment?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.StudentGroupEnrollments
            .Include(e => e.Student)
            .Include(e => e.Group)
                .ThenInclude(g => g.Subgroups)
            .Include(e => e.SubgroupEnrollments)
                .ThenInclude(se => se.Subgroup)
            .FirstOrDefaultAsync(e => e.EnrollmentId == id, ct);
    }

    public async Task<StudentGroupEnrollment?> GetActiveByStudentIdAsync(Guid studentId, CancellationToken ct = default)
    {
        return await _db.StudentGroupEnrollments
            .Include(e => e.Group)
                .ThenInclude(g => g.Subgroups)
            .Include(e => e.SubgroupEnrollments)
                .ThenInclude(se => se.Subgroup)
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.DateTo == null, ct);
    }

    public async Task<IEnumerable<StudentGroupEnrollment>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default)
    {
        return await _db.StudentGroupEnrollments
            .AsNoTracking()
            .Include(e => e.Group)
                .ThenInclude(g => g.Department)
                    .ThenInclude(d => d.AcademicUnit)
            .Include(e => e.SubgroupEnrollments)
                .ThenInclude(se => se.Subgroup)
            .Where(e => e.StudentId == studentId)
            .OrderBy(e => e.DateFrom)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<StudentGroupEnrollment>> GetByGroupIdOnDateAsync(
        Guid groupId, DateOnly date, CancellationToken ct = default)
    {
        return await _db.StudentGroupEnrollments
            .AsNoTracking()
            .Include(e => e.Student)
            .Include(e => e.SubgroupEnrollments)
                .ThenInclude(se => se.Subgroup)
            .Where(e => e.GroupId == groupId
                     && e.DateFrom <= date
                     && (e.DateTo == null || e.DateTo >= date))
            .OrderBy(e => e.Student.LastName)
            .ThenBy(e => e.Student.FirstName)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<StudentGroupEnrollment>> GetAllForGroupsAsync(
        IEnumerable<Guid> groupIds, CancellationToken ct = default)
    {
        return await _db.StudentGroupEnrollments
            .AsNoTracking()
            .Include(e => e.Student)
            .Include(e => e.Group)
            .Where(e => groupIds.Contains(e.GroupId))
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<StudentGroupEnrollment>> GetActiveByGroupIdAsync(Guid groupId, CancellationToken ct = default)
    {
        return await _db.StudentGroupEnrollments
            .Where(e => e.GroupId == groupId && e.DateTo == null)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<StudentGroupEnrollment>> GetActiveByGroupIdOnDateAsync(Guid groupId, DateOnly date, CancellationToken ct = default)
    {
        return await _db.StudentGroupEnrollments
            .Where(e => e.GroupId == groupId
                     && e.DateFrom <= date
                     && (e.DateTo == null || e.DateTo >= date))
            .ToListAsync(ct);
    }

    public async Task<bool> HasOverlapAsync(Guid studentId, DateOnly dateFrom, DateOnly? dateTo,
        Guid? excludeId = null, CancellationToken ct = default)
    {
        var overlapDateTo = dateTo ?? new DateOnly(9999, 12, 31);

        return await _db.StudentGroupEnrollments.AnyAsync(
            e => e.StudentId == studentId
                 && (!excludeId.HasValue || e.EnrollmentId != excludeId.Value)
                 && e.DateFrom <= overlapDateTo
                 && (e.DateTo == null || e.DateTo >= dateFrom),
            ct);
    }

    public StudentGroupEnrollment Add(StudentGroupEnrollment enrollment)
    {
        _db.StudentGroupEnrollments.Add(enrollment);
        return enrollment;
    }

    public void Update(StudentGroupEnrollment enrollment)
    {
        _db.StudentGroupEnrollments.Update(enrollment);
    }
}
