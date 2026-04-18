using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly UniversityDbContext _db;

    public UnitOfWork(
        UniversityDbContext db,
        IStudentRepository students,
        IGroupRepository groups,
        IEnrollmentRepository enrollments,
        IAcademicLeaveRepository academicLeaves,
        IExternalTransferRepository externalTransfers,
        IStudyPlanRepository studyPlans,
        IGradeRepository grades,
        IStudentSubgroupEnrollmentRepository subgroupEnrollments,
        IDisciplineRepository disciplines,
        IAcademicUnitRepository academicUnits,
        IDepartmentRepository departments,
        IGroupPlanAssignmentRepository groupPlanAssignments,
        IStudentGroupTransferRepository groupTransfers)
    {
        _db = db;
        Students = students;
        Groups = groups;
        Enrollments = enrollments;
        AcademicLeaves = academicLeaves;
        ExternalTransfers = externalTransfers;
        StudyPlans = studyPlans;
        Grades = grades;
        SubgroupEnrollments = subgroupEnrollments;
        Disciplines = disciplines;
        AcademicUnits = academicUnits;
        Departments = departments;
        GroupPlanAssignments = groupPlanAssignments;
        GroupTransfers = groupTransfers;
    }

    public IStudentRepository Students { get; }
    public IGroupRepository Groups { get; }
    public IEnrollmentRepository Enrollments { get; }
    public IAcademicLeaveRepository AcademicLeaves { get; }
    public IExternalTransferRepository ExternalTransfers { get; }
    public IStudyPlanRepository StudyPlans { get; }
    public IGradeRepository Grades { get; }
    public IStudentSubgroupEnrollmentRepository SubgroupEnrollments { get; }
    public IDisciplineRepository Disciplines { get; }
    public IAcademicUnitRepository AcademicUnits { get; }
    public IDepartmentRepository Departments { get; }
    public IGroupPlanAssignmentRepository GroupPlanAssignments { get; }
    public IStudentGroupTransferRepository GroupTransfers { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        _db.SaveChangesAsync(ct);
}
