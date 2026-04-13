using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudyPlanRepository
{
    // Plan CRUD
    Task<IEnumerable<StudyPlan>> GetAllPlansAsync(CancellationToken ct = default);
    Task<StudyPlan?> GetPlanByIdAsync(int planId, CancellationToken ct = default);
    Task<StudyPlan?> GetPlanWithDisciplinesAsync(int planId, CancellationToken ct = default);
    StudyPlan AddPlan(StudyPlan plan);
    void UpdatePlan(StudyPlan plan);
    Task<bool> PlanHasAssignmentsAsync(int planId, CancellationToken ct = default);
    void DeletePlan(StudyPlan plan);

    // Plan discipline
    Task<PlanDiscipline?> GetPlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default);
    PlanDiscipline AddPlanDiscipline(PlanDiscipline pd);
    void UpdatePlanDiscipline(PlanDiscipline pd);
    void DeletePlanDiscipline(PlanDiscipline pd);
    Task<bool> PlanDisciplineIsUsedAsync(int planId, int disciplineId, CancellationToken ct = default);

    // Course enrollments
    void AddCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments);
    Task<IEnumerable<StudentCourseEnrollment>> GetCourseEnrollmentsByEnrollmentIdAsync(int enrollmentId, CancellationToken ct = default);
    void RemoveCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments);
}
