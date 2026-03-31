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

    public async Task<StudentPlanAssignment?> GetOpenAssignmentByStudentIdAsync(int studentId, CancellationToken ct = default) =>
        await _db.StudentPlanAssignments
            .Include(a => a.Plan)
            .FirstOrDefaultAsync(a => a.StudentId == studentId && a.DateTo == null, ct);

    public async Task<StudentPlanAssignment> AddAssignmentAsync(StudentPlanAssignment assignment, CancellationToken ct = default)
    {
        _db.StudentPlanAssignments.Add(assignment);
        return await Task.FromResult(assignment);
    }

    public Task UpdateAssignmentAsync(StudentPlanAssignment assignment, CancellationToken ct = default)
    {
        _db.StudentPlanAssignments.Update(assignment);
        return Task.CompletedTask;
    }

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

    public async Task<StudyPlan> AddPlanAsync(StudyPlan plan, CancellationToken ct = default)
    {
        _db.StudyPlans.Add(plan);
        return await Task.FromResult(plan);
    }

    public Task UpdatePlanAsync(StudyPlan plan, CancellationToken ct = default)
    {
        _db.StudyPlans.Update(plan);
        return Task.CompletedTask;
    }

    public async Task<bool> PlanHasAssignmentsAsync(int planId, CancellationToken ct = default) =>
        await _db.StudentPlanAssignments.AnyAsync(a => a.PlanId == planId, ct);

    public Task DeletePlanAsync(StudyPlan plan, CancellationToken ct = default)
    {
        _db.StudyPlans.Remove(plan);
        return Task.CompletedTask;
    }

    public async Task<PlanDiscipline?> GetPlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default) =>
        await _db.PlanDisciplines
            .Include(pd => pd.Discipline)
            .FirstOrDefaultAsync(pd => pd.PlanId == planId && pd.DisciplineId == disciplineId, ct);

    public async Task<PlanDiscipline> AddPlanDisciplineAsync(PlanDiscipline pd, CancellationToken ct = default)
    {
        _db.PlanDisciplines.Add(pd);
        return await Task.FromResult(pd);
    }

    public Task UpdatePlanDisciplineAsync(PlanDiscipline pd, CancellationToken ct = default)
    {
        _db.PlanDisciplines.Update(pd);
        return Task.CompletedTask;
    }

    public Task DeletePlanDisciplineAsync(PlanDiscipline pd, CancellationToken ct = default)
    {
        _db.PlanDisciplines.Remove(pd);
        return Task.CompletedTask;
    }

    public async Task<bool> PlanDisciplineIsUsedAsync(int planId, int disciplineId, CancellationToken ct = default) =>
        await _db.StudentCourseEnrollments
            .AnyAsync(ce => ce.Assignment.PlanId == planId && ce.DisciplineId == disciplineId, ct);
}
