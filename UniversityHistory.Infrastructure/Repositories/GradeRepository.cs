using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly UniversityDbContext _db;
    public GradeRepository(UniversityDbContext db) => _db = db;

    public async Task<IEnumerable<GradeRecord>> GetByStudentIdAsync(int studentId, CancellationToken ct = default) =>
        await _db.GradeRecords.AsNoTracking()
            .Include(g => g.CourseEnrollment)
                .ThenInclude(ce => ce.Discipline)
            .Include(g => g.CourseEnrollment)
                .ThenInclude(ce => ce.Assignment)
                    .ThenInclude(a => a.Plan)
                        .ThenInclude(p => p.PlanDisciplines)
            .Where(g => g.CourseEnrollment.Assignment.StudentId == studentId)
            .OrderBy(g => g.AssessmentDate)
            .ToListAsync(ct);

    public async Task<GradeRecord> AddAsync(GradeRecord grade, CancellationToken ct = default)
    {
        _db.GradeRecords.Add(grade);
        await _db.SaveChangesAsync(ct);
        return grade;
    }
}
