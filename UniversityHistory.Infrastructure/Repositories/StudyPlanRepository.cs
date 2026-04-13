using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class StudyPlanRepository : IStudyPlanRepository
{
    private readonly UniversityDbContext _db;
    public StudyPlanRepository(UniversityDbContext db) => _db = db;

    // ── Plan CRUD ──────────────────────────────────────────────────────────────

    public async Task<IEnumerable<StudyPlan>> GetAllPlansAsync(CancellationToken ct = default) =>
        await _db.StudyPlans.AsNoTracking()
            .OrderBy(p => p.SpecialtyCode).ThenBy(p => p.ValidFrom)
            .ToListAsync(ct);

    public async Task<StudyPlan?> GetPlanByIdAsync(int planId, CancellationToken ct = default) =>
        await _db.StudyPlans.FindAsync(new object[] { planId }, ct);

    public async Task<StudyPlan?> GetPlanWithDisciplinesAsync(int planId, CancellationToken ct = default) =>
        await _db.StudyPlans.AsNoTracking()
            .Include(p => p.PlanDisciplines)
                .ThenInclude(pd => pd.Discipline)
            .FirstOrDefaultAsync(p => p.PlanId == planId, ct);

    public StudyPlan AddPlan(StudyPlan plan) { _db.StudyPlans.Add(plan); return plan; }
    public void UpdatePlan(StudyPlan plan) => _db.StudyPlans.Update(plan);
    public void DeletePlan(StudyPlan plan) => _db.StudyPlans.Remove(plan);

    public async Task<bool> PlanHasAssignmentsAsync(int planId, CancellationToken ct = default) =>
        await _db.GroupPlanAssignments.AnyAsync(a => a.PlanId == planId, ct);

    // ── Plan disciplines ───────────────────────────────────────────────────────

    public async Task<PlanDiscipline?> GetPlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default) =>
        await _db.PlanDisciplines
            .Include(pd => pd.Discipline)
            .FirstOrDefaultAsync(pd => pd.PlanId == planId && pd.DisciplineId == disciplineId, ct);

    public PlanDiscipline AddPlanDiscipline(PlanDiscipline pd) { _db.PlanDisciplines.Add(pd); return pd; }
    public void UpdatePlanDiscipline(PlanDiscipline pd) => _db.PlanDisciplines.Update(pd);
    public void DeletePlanDiscipline(PlanDiscipline pd) => _db.PlanDisciplines.Remove(pd);

    public async Task<bool> PlanDisciplineIsUsedAsync(int planId, int disciplineId, CancellationToken ct = default) =>
        await _db.StudentCourseEnrollments
            .AnyAsync(ce => ce.GroupPlanAssignment.PlanId == planId
                         && ce.DisciplineId == disciplineId
                         && ce.Status != CourseStatus.Planned, ct);

    // ── Course enrollments ─────────────────────────────────────────────────────

    public void AddCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments) =>
        _db.StudentCourseEnrollments.AddRange(enrollments);

    public async Task<IEnumerable<StudentCourseEnrollment>> GetCourseEnrollmentsByEnrollmentIdAsync(
        int enrollmentId, CancellationToken ct = default) =>
        await _db.StudentCourseEnrollments
            .Where(ce => ce.EnrollmentId == enrollmentId)
            .ToListAsync(ct);

    public void RemoveCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments) =>
        _db.StudentCourseEnrollments.RemoveRange(enrollments);
}
