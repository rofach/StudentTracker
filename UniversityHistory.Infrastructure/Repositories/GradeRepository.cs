using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Common;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly UniversityDbContext _db;
    public GradeRepository(UniversityDbContext db) => _db = db;

    public async Task<PagedData<GradeRecord>> GetByStudentIdAsync(Guid studentId, int page, int pageSize, CancellationToken ct = default)
    {
        var query = _db.GradeRecords.AsNoTracking()
            .Include(g => g.CourseEnrollment)
                .ThenInclude(ce => ce.Discipline)
            .Include(g => g.CourseEnrollment)
                .ThenInclude(ce => ce.GroupPlanAssignment)
                    .ThenInclude(a => a.Plan)
                        .ThenInclude(p => p.PlanDisciplines)
            .Where(g => g.CourseEnrollment.Enrollment.StudentId == studentId);

        var count = await query.CountAsync(ct);
        var items = await query.OrderBy(g => g.AssessmentDate)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync(ct);

        return new PagedData<GradeRecord>(items, count);
    }

    public GradeRecord Add(GradeRecord grade)
    {
        _db.GradeRecords.Add(grade);
        return grade;
    }
}

