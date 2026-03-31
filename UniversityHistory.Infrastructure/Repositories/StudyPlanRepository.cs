using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
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

    public async Task<IEnumerable<StudentPlanAssignment>> GetAssignmentsByStudentIdAsync(int studentId, CancellationToken ct = default)
    {
        return await _db.StudentPlanAssignments.AsNoTracking()
            .Include(a => a.Plan)
            .Where(a => a.StudentId == studentId)
            .OrderBy(a => a.DateFrom)
            .ToListAsync(ct);
    }

    public async Task<StudentPlanAssignment?> GetOpenAssignmentByStudentIdAsync(int studentId, CancellationToken ct = default)
    {
        return await _db.StudentPlanAssignments
            .Include(a => a.Plan)
            .FirstOrDefaultAsync(a => a.StudentId == studentId && a.DateTo == null, ct);
    }

    public StudentPlanAssignment AddAssignment(StudentPlanAssignment assignment)
    {
        _db.StudentPlanAssignments.Add(assignment);
        return assignment;
    }

    public void UpdateAssignment(StudentPlanAssignment assignment)
    {
        _db.StudentPlanAssignments.Update(assignment);
    }

    public async Task<IEnumerable<StudyPlan>> GetAllPlansAsync(CancellationToken ct = default)
    {
        return await _db.StudyPlans.AsNoTracking()
            .OrderBy(p => p.SpecialtyCode).ThenBy(p => p.ValidFrom)
            .ToListAsync(ct);
    }

    public async Task<StudyPlan?> GetPlanByIdAsync(int planId, CancellationToken ct = default)
    {
        return await _db.StudyPlans.FindAsync(new object[] { planId }, ct);
    }

    public async Task<StudyPlan?> GetPlanWithDisciplinesAsync(int planId, CancellationToken ct = default)
    {
        return await _db.StudyPlans.AsNoTracking()
            .Include(p => p.PlanDisciplines)
                .ThenInclude(pd => pd.Discipline)
            .FirstOrDefaultAsync(p => p.PlanId == planId, ct);
    }

    public StudyPlan AddPlan(StudyPlan plan)
    {
        _db.StudyPlans.Add(plan);
        return plan;
    }

    public void UpdatePlan(StudyPlan plan)
    {
        _db.StudyPlans.Update(plan);
    }

    public async Task<bool> PlanHasAssignmentsAsync(int planId, CancellationToken ct = default)
    {
        return await _db.StudentPlanAssignments.AnyAsync(a => a.PlanId == planId, ct);
    }

    public void DeletePlan(StudyPlan plan)
    {
        _db.StudyPlans.Remove(plan);
    }

    public async Task<PlanDiscipline?> GetPlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default)
    {
        return await _db.PlanDisciplines
            .Include(pd => pd.Discipline)
            .FirstOrDefaultAsync(pd => pd.PlanId == planId && pd.DisciplineId == disciplineId, ct);
    }

    public PlanDiscipline AddPlanDiscipline(PlanDiscipline pd)
    {
        _db.PlanDisciplines.Add(pd);
        return pd;
    }

    public void UpdatePlanDiscipline(PlanDiscipline pd)
    {
        _db.PlanDisciplines.Update(pd);
    }

    public void DeletePlanDiscipline(PlanDiscipline pd)
    {
        _db.PlanDisciplines.Remove(pd);
    }

    public async Task<bool> PlanDisciplineIsUsedAsync(int planId, int disciplineId, CancellationToken ct = default)
    {
        return await _db.StudentCourseEnrollments
            .AnyAsync(ce => ce.Assignment.PlanId == planId && ce.DisciplineId == disciplineId, ct);
    }
}
