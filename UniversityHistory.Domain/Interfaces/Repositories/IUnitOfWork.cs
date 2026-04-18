namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IStudentRepository Students { get; }
    IGroupRepository Groups { get; }
    IEnrollmentRepository Enrollments { get; }
    IAcademicLeaveRepository AcademicLeaves { get; }
    IExternalTransferRepository ExternalTransfers { get; }
    IStudyPlanRepository StudyPlans { get; }
    IGradeRepository Grades { get; }
    IStudentSubgroupEnrollmentRepository SubgroupEnrollments { get; }
    IDisciplineRepository Disciplines { get; }
    IAcademicUnitRepository AcademicUnits { get; }
    IDepartmentRepository Departments { get; }
    IGroupPlanAssignmentRepository GroupPlanAssignments { get; }
    IStudentGroupTransferRepository GroupTransfers { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
