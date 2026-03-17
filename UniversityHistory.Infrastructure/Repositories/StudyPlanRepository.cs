using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class StudyPlanRepository : IStudyPlanRepository
{
    private readonly UniversityDbContext _db;
    public StudyPlanRepository(UniversityDbContext db) => _db = db;

    public async Task<IEnumerable<StudentPlanAssignment>> GetAssignmentsByStudentIdAsync(int studentId, CancellationToken ct = default) =>
        await _db.StudentPlanAssignments.AsNoTracking()
            .Include(a => a.Plan)
            .Where(a => a.StudentId == studentId)
            .OrderBy(a => a.DateFrom)
            .ToListAsync(ct);

    public async Task<StudentPlanAssignment> AddAssignmentAsync(StudentPlanAssignment assignment, CancellationToken ct = default)
    {
        _db.StudentPlanAssignments.Add(assignment);
        await _db.SaveChangesAsync(ct);
        return assignment;
    }
}
