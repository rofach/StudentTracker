using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudyPlanRepository
{
    Task<IEnumerable<StudentPlanAssignment>> GetAssignmentsByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<StudentPlanAssignment?> GetOpenAssignmentByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<StudentPlanAssignment> AddAssignmentAsync(StudentPlanAssignment assignment, CancellationToken ct = default);
    Task UpdateAssignmentAsync(StudentPlanAssignment assignment, CancellationToken ct = default);

    Task<IEnumerable<StudyPlan>> GetAllPlansAsync(CancellationToken ct = default);
    Task<StudyPlan?> GetPlanByIdAsync(int planId, CancellationToken ct = default);
    Task<StudyPlan?> GetPlanWithDisciplinesAsync(int planId, CancellationToken ct = default);
    Task<StudyPlan> AddPlanAsync(StudyPlan plan, CancellationToken ct = default);
    Task UpdatePlanAsync(StudyPlan plan, CancellationToken ct = default);
    Task<bool> PlanHasAssignmentsAsync(int planId, CancellationToken ct = default);
    Task DeletePlanAsync(StudyPlan plan, CancellationToken ct = default);

    Task<PlanDiscipline?> GetPlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default);
    Task<PlanDiscipline> AddPlanDisciplineAsync(PlanDiscipline pd, CancellationToken ct = default);
    Task UpdatePlanDisciplineAsync(PlanDiscipline pd, CancellationToken ct = default);
    Task DeletePlanDisciplineAsync(PlanDiscipline pd, CancellationToken ct = default);
    Task<bool> PlanDisciplineIsUsedAsync(int planId, int disciplineId, CancellationToken ct = default);
}
