using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudyPlanRepository
{
    Task<IEnumerable<StudyPlan>> GetAllPlansAsync(CancellationToken ct = default);
    Task<StudyPlan?> GetPlanByIdAsync(Guid planId, CancellationToken ct = default);
    Task<StudyPlan?> GetPlanWithDisciplinesAsync(Guid planId, CancellationToken ct = default);
    StudyPlan AddPlan(StudyPlan plan);
    void UpdatePlan(StudyPlan plan);
    Task<bool> PlanHasAssignmentsAsync(Guid planId, CancellationToken ct = default);
    void DeletePlan(StudyPlan plan);

    Task<PlanDiscipline?> GetPlanDisciplineAsync(Guid planId, Guid disciplineId, CancellationToken ct = default);
    PlanDiscipline AddPlanDiscipline(PlanDiscipline pd);
    void UpdatePlanDiscipline(PlanDiscipline pd);
    void DeletePlanDiscipline(PlanDiscipline pd);
    Task<bool> PlanDisciplineIsUsedAsync(Guid planId, Guid disciplineId, CancellationToken ct = default);

    void AddCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments);
    Task<IEnumerable<StudentCourseEnrollment>> GetCourseEnrollmentsByEnrollmentIdAsync(Guid enrollmentId, CancellationToken ct = default);
    Task<StudentCourseEnrollment?> GetCourseEnrollmentByIdAsync(Guid courseEnrollmentId, CancellationToken ct = default);
    void RemoveCourseEnrollments(IEnumerable<StudentCourseEnrollment> enrollments);
    Task RemovePlannedCourseEnrollmentsForDisciplineAsync(Guid planId, Guid disciplineId, CancellationToken ct = default);
}

