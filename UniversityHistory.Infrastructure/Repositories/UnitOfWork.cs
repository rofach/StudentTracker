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
        ISubgroupAssignmentRepository subgroupAssignments,
        IDisciplineRepository disciplines)
    {
        _db = db;
        Students = students;
        Groups = groups;
        Enrollments = enrollments;
        AcademicLeaves = academicLeaves;
        ExternalTransfers = externalTransfers;
        StudyPlans = studyPlans;
        Grades = grades;
        SubgroupAssignments = subgroupAssignments;
        Disciplines = disciplines;
    }

    public IStudentRepository Students { get; }
    public IGroupRepository Groups { get; }
    public IEnrollmentRepository Enrollments { get; }
    public IAcademicLeaveRepository AcademicLeaves { get; }
    public IExternalTransferRepository ExternalTransfers { get; }
    public IStudyPlanRepository StudyPlans { get; }
    public IGradeRepository Grades { get; }
    public ISubgroupAssignmentRepository SubgroupAssignments { get; }
    public IDisciplineRepository Disciplines { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return _db.SaveChangesAsync(ct);
    }
}
