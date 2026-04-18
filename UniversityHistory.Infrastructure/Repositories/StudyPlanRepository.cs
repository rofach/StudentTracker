using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class StudyPlanRepository : IStudyPlanRepository
{
    private readonly UniversityDbContext _db;
    public StudyPlanRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public StudyPlan AddPlan(StudyPlan plan) { _db.StudyPlans.Add(plan); return plan; }
    public void UpdatePlan(StudyPlan plan) => _db.StudyPlans.Update(plan);
    public void DeletePlan(StudyPlan plan) => _db.StudyPlans.Remove(plan);
    public void UpdatePlanDiscipline(PlanDiscipline pd) => _db.PlanDisciplines.Update(pd);
    public void DeletePlanDiscipline(PlanDiscipline pd) => _db.PlanDisciplines.Remove(pd);

    public async Task<IEnumerable<StudyPlan>> GetAllPlansAsync(CancellationToken ct = default)
    {
        return await _db.StudyPlans.AsNoTracking()
            .OrderBy(p => p.SpecialtyCode).ThenBy(p => p.ValidFrom)
            .ToListAsync(ct);
    }

    public async Task<StudyPlan?> GetPlanByIdAsync(Guid planId, CancellationToken ct = default)
    {
        return await _db.StudyPlans.FindAsync(new object[] { planId }, ct);
    }

    public async Task<StudyPlan?> GetPlanWithDisciplinesAsync(Guid planId, CancellationToken ct = default)
    {
        return await _db.StudyPlans.AsNoTracking()
            .Include(p => p.PlanDisciplines)
                .ThenInclude(pd => pd.Discipline)
            .FirstOrDefaultAsync(p => p.PlanId == planId, ct);
    }

    public async Task<bool> PlanHasAssignmentsAsync(Guid planId, CancellationToken ct = default)
    {
        return await _db.GroupPlanAssignments.AnyAsync(a => a.PlanId == planId, ct);
    }

    public async Task<PlanDiscipline?> GetPlanDisciplineAsync(Guid planId, Guid disciplineId, CancellationToken ct = default)
    {
        return await _db.PlanDisciplines
            .Include(pd => pd.Discipline)
            .FirstOrDefaultAsync(pd => pd.PlanId == planId && pd.DisciplineId == disciplineId, ct);
    }

    public PlanDiscipline AddPlanDiscipline(PlanDiscipline pd)
    {
        _db.PlanDisciplines.Add(pd); return pd;
    }

    public async Task<bool> PlanDisciplineIsUsedAsync(Guid planId, Guid disciplineId, CancellationToken ct = default)
    {
        return await _db.StudentCourseEnrollments
            .AnyAsync(ce => ce.PlanDiscipline.PlanId == planId
                         && ce.PlanDiscipline.DisciplineId == disciplineId
                         && ce.Status != CourseStatus.Planned, ct);
    }

    public void AddCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments)
    {
        _db.StudentCourseEnrollments.AddRange(enrollments);
    }

    public void RemoveCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments)
    {
        _db.StudentCourseEnrollments.RemoveRange(enrollments);
    }

    public async Task<IEnumerable<StudentCourseEnrollment>> GetCourseEnrollmentsByEnrollmentIdAsync(
        Guid enrollmentId, CancellationToken ct = default)
    {
        return await _db.StudentCourseEnrollments
            .Include(ce => ce.PlanDiscipline)
                .ThenInclude(pd => pd.Discipline)
            .Include(ce => ce.GradeRecords)
            .Where(ce => ce.EnrollmentId == enrollmentId)
            .ToListAsync(ct);
    }

    public async Task RemovePlannedCourseEnrollmentsForDisciplineAsync(
        Guid planId, Guid disciplineId, CancellationToken ct = default)
    {
        var rows = await _db.StudentCourseEnrollments
            .Where(ce => ce.PlanDiscipline.PlanId == planId
                      && ce.PlanDiscipline.DisciplineId == disciplineId
                      && ce.Status == CourseStatus.Planned)
            .ToListAsync(ct);

        if (rows.Count > 0)
            _db.StudentCourseEnrollments.RemoveRange(rows);
    }
}
