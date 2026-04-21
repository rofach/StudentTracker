using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Infrastructure.Data.Seed;

namespace UniversityHistory.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var seed = DemoSeedData.Build();

        modelBuilder.Entity<Institution>().HasData(seed.Institutions);
        modelBuilder.Entity<AcademicUnit>().HasData(seed.AcademicUnits);
        modelBuilder.Entity<Department>().HasData(seed.Departments);
        modelBuilder.Entity<Discipline>().HasData(seed.Disciplines);
        modelBuilder.Entity<StudyPlan>().HasData(seed.StudyPlans);
        modelBuilder.Entity<PlanDiscipline>().HasData(seed.PlanDisciplines);
        modelBuilder.Entity<StudyGroup>().HasData(seed.StudyGroups);
        modelBuilder.Entity<Subgroup>().HasData(seed.Subgroups);
        modelBuilder.Entity<Student>().HasData(seed.Students);
        modelBuilder.Entity<StudentGroupEnrollment>().HasData(seed.Enrollments);
        modelBuilder.Entity<StudentSubgroupEnrollment>().HasData(seed.SubgroupEnrollments);
        modelBuilder.Entity<AcademicLeave>().HasData(seed.AcademicLeaves);
        modelBuilder.Entity<ExternalTransfer>().HasData(seed.ExternalTransfers);
        modelBuilder.Entity<GroupPlanAssignment>().HasData(seed.GroupPlanAssignments);
        modelBuilder.Entity<StudentCourseEnrollment>().HasData(seed.CourseEnrollments);
        modelBuilder.Entity<GradeRecord>().HasData(seed.GradeRecords);
        modelBuilder.Entity<StudentGroupTransfer>().HasData(seed.GroupTransfers);
        modelBuilder.Entity<AcademicDifferenceItem>().HasData(seed.AcademicDifferenceItems);
    }
}
