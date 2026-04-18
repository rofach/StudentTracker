using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class StudentSubgroupEnrollmentRepository : IStudentSubgroupEnrollmentRepository
{
    private readonly UniversityDbContext _db;
    public StudentSubgroupEnrollmentRepository(UniversityDbContext db) => _db = db;

    public async Task<StudentSubgroupEnrollment?> GetOpenByEnrollmentIdAsync(Guid enrollmentId, CancellationToken ct = default)
    {
        return await _db.StudentSubgroupEnrollments
            .Include(se => se.Subgroup)
            .FirstOrDefaultAsync(se => se.EnrollmentId == enrollmentId && se.DateTo == null, ct);
    }

    public StudentSubgroupEnrollment Add(StudentSubgroupEnrollment subgroupEnrollment)
    {
        _db.StudentSubgroupEnrollments.Add(subgroupEnrollment);
        return subgroupEnrollment;
    }

    public void Update(StudentSubgroupEnrollment subgroupEnrollment)
    {
        _db.StudentSubgroupEnrollments.Update(subgroupEnrollment);
    }
}
