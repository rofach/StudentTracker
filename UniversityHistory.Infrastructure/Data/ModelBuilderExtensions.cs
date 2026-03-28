using System;
using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Institution>().HasData(
            new Institution { InstitutionId = 1, InstitutionName = "MIT", City = "Cambridge", Country = "USA" },
            new Institution { InstitutionId = 2, InstitutionName = "Stanford University", City = "Stanford", Country = "USA" }
        );

        modelBuilder.Entity<Discipline>().HasData(
            new Discipline { DisciplineId = 1, DisciplineName = "Calculus I" },
            new Discipline { DisciplineId = 2, DisciplineName = "Introduction to Computer Science" },
            new Discipline { DisciplineId = 3, DisciplineName = "Data Structures" },
            new Discipline { DisciplineId = 4, DisciplineName = "Linear Algebra" }
        );

        modelBuilder.Entity<StudyPlan>().HasData(
            new StudyPlan { PlanId = 1, SpecialtyCode = "CS-101", PlanName = "BSc Computer Science v1", ValidFrom = new DateOnly(2021, 9, 1) },
            new StudyPlan { PlanId = 2, SpecialtyCode = "CS-102", PlanName = "BSc Computer Science v2", ValidFrom = new DateOnly(2023, 9, 1) }
        );

        modelBuilder.Entity<PlanDiscipline>().HasData(
            new PlanDiscipline { PlanId = 1, DisciplineId = 1, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 2, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 3, SemesterNo = 2, ControlType = ControlType.Coursework, Hours = 180, Credits = 6.0m },
            
            new PlanDiscipline { PlanId = 2, DisciplineId = 1, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 2, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 4, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m }
        );

        modelBuilder.Entity<StudyGroup>().HasData(
            new StudyGroup { GroupId = 1, GroupCode = "CS-21", Faculty = "Computer Science", DateCreated = new DateOnly(2021, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 2, GroupCode = "CS-22", Faculty = "Computer Science", DateCreated = new DateOnly(2022, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 3, GroupCode = "CS-23", Faculty = "Computer Science", DateCreated = new DateOnly(2023, 9, 1), DateClosed = null }
        );

        modelBuilder.Entity<Subgroup>().HasData(
            new Subgroup { SubgroupId = 1, GroupId = 1, SubgroupName = "Subgroup A" },
            new Subgroup { SubgroupId = 2, GroupId = 1, SubgroupName = "Subgroup B" },
            new Subgroup { SubgroupId = 3, GroupId = 2, SubgroupName = "Subgroup A" }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student { StudentId = 1, FirstName = "Alice", LastName = "Smith", BirthDate = new DateOnly(2003, 5, 14), Email = "alice@example.com", Phone = "555-0101", Status = StudentStatus.Active },
            new Student { StudentId = 2, FirstName = "Bob", LastName = "Johnson", BirthDate = new DateOnly(2002, 11, 2), Email = "bob@example.com", Phone = "555-0102", Status = StudentStatus.Graduated },
            new Student { StudentId = 3, FirstName = "Charlie", LastName = "Williams", BirthDate = new DateOnly(2004, 8, 22), Email = "charlie@example.com", Phone = "555-0103", Status = StudentStatus.OnLeave },
            new Student { StudentId = 4, FirstName = "Diana", LastName = "Brown", BirthDate = new DateOnly(2003, 1, 30), Email = "diana@example.com", Phone = "555-0104", Status = StudentStatus.Active }
        );

        modelBuilder.Entity<StudentGroupEnrollment>().HasData(
            new StudentGroupEnrollment { EnrollmentId = 1, StudentId = 1, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2022, 6, 30), ReasonStart = "Admission", ReasonEnd = "Transfer to new group" },
            new StudentGroupEnrollment { EnrollmentId = 2, StudentId = 1, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = null, ReasonStart = "Transfer", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 3, StudentId = 2, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2024, 6, 30), ReasonStart = "Admission", ReasonEnd = "Graduation" },
            new StudentGroupEnrollment { EnrollmentId = 4, StudentId = 3, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = new DateOnly(2023, 1, 15), ReasonStart = "Admission", ReasonEnd = "Academic Leave" },
            new StudentGroupEnrollment { EnrollmentId = 5, StudentId = 4, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Admission", ReasonEnd = null }
        );

        modelBuilder.Entity<StudentSubgroupAssignment>().HasData(
            new StudentSubgroupAssignment { EnrollmentId = 1, SubgroupId = 1 },
            new StudentSubgroupAssignment { EnrollmentId = 2, SubgroupId = 3 },
            new StudentSubgroupAssignment { EnrollmentId = 3, SubgroupId = 2 }
        );

        modelBuilder.Entity<AcademicLeave>().HasData(
            new AcademicLeave { LeaveId = 1, EnrollmentId = 4, StartDate = new DateOnly(2023, 1, 16), EndDate = null, Reason = "Medical leave" }
        );

        modelBuilder.Entity<ExternalTransfer>().HasData(
            new ExternalTransfer { TransferId = 1, StudentId = 4, InstitutionId = 2, TransferType = TransferType.In, TransferDate = new DateOnly(2023, 8, 25), EnrollmentId = 5, Notes = "Completed first year at Stanford" }
        );

        modelBuilder.Entity<StudentPlanAssignment>().HasData(
            new StudentPlanAssignment { AssignmentId = 1, StudentId = 1, PlanId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 2, StudentId = 2, PlanId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2024, 6, 30) }
        );

        modelBuilder.Entity<StudentCourseEnrollment>().HasData(
            new StudentCourseEnrollment { CourseEnrollmentId = 1, AssignmentId = 1, DisciplineId = 1, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 2, AssignmentId = 1, DisciplineId = 2, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 3, AssignmentId = 2, DisciplineId = 1, AcademicYearStart = 2021, Status = CourseStatus.Completed }
        );

        modelBuilder.Entity<GradeRecord>().HasData(
            new GradeRecord { GradeId = 1, CourseEnrollmentId = 1, GradeValue = "95", AssessmentDate = new DateOnly(2022, 1, 15) },
            new GradeRecord { GradeId = 2, CourseEnrollmentId = 2, GradeValue = "88", AssessmentDate = new DateOnly(2022, 1, 18) },
            new GradeRecord { GradeId = 3, CourseEnrollmentId = 3, GradeValue = "91", AssessmentDate = new DateOnly(2022, 1, 15) }
        );
    }
}
