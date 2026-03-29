using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudyPlanRepository
{
    Task<IEnumerable<StudentPlanAssignment>> GetAssignmentsByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<StudentPlanAssignment?> GetOpenAssignmentByStudentIdAsync(int studentId, CancellationToken ct = default);
    Task<StudyPlan?> GetPlanByIdAsync(int planId, CancellationToken ct = default);
    Task<StudentPlanAssignment> AddAssignmentAsync(StudentPlanAssignment assignment, CancellationToken ct = default);
    Task UpdateAssignmentAsync(StudentPlanAssignment assignment, CancellationToken ct = default);
}
