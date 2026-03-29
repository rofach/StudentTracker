using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly UniversityDbContext _db;
    private readonly string _connectionString;

    public EnrollmentRepository(UniversityDbContext db)
    {
        _db = db;
        _connectionString = db.Database.GetConnectionString()!;
    }

    public async Task<StudentGroupEnrollment?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.StudentGroupEnrollments
            .Include(e => e.Student)
            .Include(e => e.Group)
                .ThenInclude(g => g.Subgroups)
            .Include(e => e.SubgroupAssignment!)
                .ThenInclude(sa => sa.Subgroup)
            .FirstOrDefaultAsync(e => e.EnrollmentId == id, ct);

    public async Task<StudentGroupEnrollment?> GetActiveByStudentIdAsync(int studentId, CancellationToken ct = default) =>
        await _db.StudentGroupEnrollments
            .Include(e => e.Group)
                .ThenInclude(g => g.Subgroups)
            .Include(e => e.SubgroupAssignment!)
                .ThenInclude(sa => sa.Subgroup)
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.DateTo == null, ct);

    public async Task<IEnumerable<StudentGroupEnrollment>> GetByStudentIdAsync(int studentId, CancellationToken ct = default) =>
        await _db.StudentGroupEnrollments
            .AsNoTracking()
            .Include(e => e.Group)
            .Include(e => e.SubgroupAssignment!)
                .ThenInclude(sa => sa.Subgroup)
            .Where(e => e.StudentId == studentId)
            .OrderBy(e => e.DateFrom)
            .ToListAsync(ct);

    public async Task<IEnumerable<StudentGroupEnrollment>> GetByGroupIdOnDateAsync(
        int groupId, DateOnly date, CancellationToken ct = default) =>
        await _db.StudentGroupEnrollments
            .AsNoTracking()
            .Include(e => e.Student)
            .Include(e => e.SubgroupAssignment!)
                .ThenInclude(sa => sa.Subgroup)
            .Where(e => e.GroupId == groupId
                     && e.DateFrom <= date
                     && (e.DateTo == null || e.DateTo >= date))
            .OrderBy(e => e.Student.LastName)
            .ThenBy(e => e.Student.FirstName)
            .ToListAsync(ct);

    public async Task<IEnumerable<StudentGroupEnrollment>> GetAllForGroupsAsync(
        IEnumerable<int> groupIds, CancellationToken ct = default) =>
        await _db.StudentGroupEnrollments
            .AsNoTracking()
            .Include(e => e.Student)
            .Include(e => e.Group)
            .Where(e => groupIds.Contains(e.GroupId))
            .ToListAsync(ct);

    public async Task<bool> HasOverlapAsync(int studentId, DateOnly dateFrom, DateOnly? dateTo,
        int? excludeId = null, CancellationToken ct = default)
    {
        var count = await _db.Database
            .SqlQuery<int>($"""
            SELECT COUNT(1) FROM Student_Group_Enrollment
            WHERE student_id         = {studentId}
            AND (@ExcludeId IS NULL OR enrollment_id <> {excludeId})
            AND date_from         <= ISNULL({dateTo}, '9999-12-31')
            AND ISNULL(date_to, '9999-12-31') >= {dateFrom}
            """)
            .FirstAsync(ct);

        return count > 0;
    }

    public async Task<StudentGroupEnrollment> AddAsync(StudentGroupEnrollment enrollment, CancellationToken ct = default)
    {
        _db.StudentGroupEnrollments.Add(enrollment);
        await _db.SaveChangesAsync(ct);
        return enrollment;
    }

    public async Task UpdateAsync(StudentGroupEnrollment enrollment, CancellationToken ct = default)
    {
        _db.StudentGroupEnrollments.Update(enrollment);
        await _db.SaveChangesAsync(ct);
    }
}
