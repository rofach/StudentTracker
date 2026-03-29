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
            new Institution { InstitutionId = 2, InstitutionName = "Stanford University", City = "Stanford", Country = "USA" },
            new Institution { InstitutionId = 3, InstitutionName = "Kyiv Polytechnic Institute", City = "Kyiv", Country = "Ukraine" },
            new Institution { InstitutionId = 4, InstitutionName = "Lviv Polytechnic", City = "Lviv", Country = "Ukraine" }
        );

        modelBuilder.Entity<Discipline>().HasData(
            new Discipline { DisciplineId = 1, DisciplineName = "Calculus I" },
            new Discipline { DisciplineId = 2, DisciplineName = "Introduction to Computer Science" },
            new Discipline { DisciplineId = 3, DisciplineName = "Data Structures" },
            new Discipline { DisciplineId = 4, DisciplineName = "Linear Algebra" },
            new Discipline { DisciplineId = 5, DisciplineName = "Discrete Mathematics" },
            new Discipline { DisciplineId = 6, DisciplineName = "Databases" },
            new Discipline { DisciplineId = 7, DisciplineName = "Operating Systems" },
            new Discipline { DisciplineId = 8, DisciplineName = "Computer Networks" }
        );

        modelBuilder.Entity<StudyPlan>().HasData(
            new StudyPlan { PlanId = 1, SpecialtyCode = "CS-101", PlanName = "BSc Computer Science v1", ValidFrom = new DateOnly(2021, 9, 1) },
            new StudyPlan { PlanId = 2, SpecialtyCode = "CS-102", PlanName = "BSc Computer Science v2", ValidFrom = new DateOnly(2023, 9, 1) }
        );

        modelBuilder.Entity<PlanDiscipline>().HasData(
            new PlanDiscipline { PlanId = 1, DisciplineId = 1, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 2, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 5, SemesterNo = 1, ControlType = ControlType.Credit, Hours = 90, Credits = 3.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 3, SemesterNo = 2, ControlType = ControlType.Coursework, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 4, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 6, SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 7, SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },

            new PlanDiscipline { PlanId = 2, DisciplineId = 1, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 2, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 4, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 5, SemesterNo = 2, ControlType = ControlType.Credit, Hours = 90, Credits = 3.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 6, SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 8, SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m }
        );

        modelBuilder.Entity<StudyGroup>().HasData(
            new StudyGroup { GroupId = 1, GroupCode = "CS-21", Faculty = "Computer Science", DateCreated = new DateOnly(2021, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 2, GroupCode = "CS-22", Faculty = "Computer Science", DateCreated = new DateOnly(2022, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 3, GroupCode = "CS-23", Faculty = "Computer Science", DateCreated = new DateOnly(2023, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 4, GroupCode = "CS-24", Faculty = "Computer Science", DateCreated = new DateOnly(2024, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 5, GroupCode = "CS-25", Faculty = "Computer Science", DateCreated = new DateOnly(2025, 9, 1), DateClosed = null }
        );

        modelBuilder.Entity<Subgroup>().HasData(
            new Subgroup { SubgroupId = 1, GroupId = 1, SubgroupName = "Subgroup A" },
            new Subgroup { SubgroupId = 2, GroupId = 1, SubgroupName = "Subgroup B" },
            new Subgroup { SubgroupId = 3, GroupId = 2, SubgroupName = "Subgroup A" },
            new Subgroup { SubgroupId = 4, GroupId = 3, SubgroupName = "Subgroup A" },
            new Subgroup { SubgroupId = 5, GroupId = 4, SubgroupName = "Subgroup A" },
            new Subgroup { SubgroupId = 6, GroupId = 4, SubgroupName = "Subgroup B" },
            new Subgroup { SubgroupId = 7, GroupId = 5, SubgroupName = "Subgroup A" }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student { StudentId = 1, FirstName = "Alice", LastName = "Smith", BirthDate = new DateOnly(2003, 5, 14), Email = "alice@example.com", Phone = "555-0101", Status = StudentStatus.Active },
            new Student { StudentId = 2, FirstName = "Bob", LastName = "Johnson", BirthDate = new DateOnly(2002, 11, 2), Email = "bob@example.com", Phone = "555-0102", Status = StudentStatus.Graduated },
            new Student { StudentId = 3, FirstName = "Charlie", LastName = "Williams", BirthDate = new DateOnly(2004, 8, 22), Email = "charlie@example.com", Phone = "555-0103", Status = StudentStatus.OnLeave },
            new Student { StudentId = 4, FirstName = "Diana", LastName = "Brown", BirthDate = new DateOnly(2003, 1, 30), Email = "diana@example.com", Phone = "555-0104", Status = StudentStatus.Active },
            new Student { StudentId = 5, FirstName = "Evan", LastName = "Davis", BirthDate = new DateOnly(2003, 3, 2), Email = "evan@example.com", Phone = "555-0105", Status = StudentStatus.Active },
            new Student { StudentId = 6, FirstName = "Fiona", LastName = "Miller", BirthDate = new DateOnly(2004, 2, 19), Email = "fiona@example.com", Phone = "555-0106", Status = StudentStatus.Active },
            new Student { StudentId = 7, FirstName = "George", LastName = "Wilson", BirthDate = new DateOnly(2002, 9, 9), Email = "george@example.com", Phone = "555-0107", Status = StudentStatus.Graduated },
            new Student { StudentId = 8, FirstName = "Hannah", LastName = "Moore", BirthDate = new DateOnly(2004, 6, 10), Email = "hannah@example.com", Phone = "555-0108", Status = StudentStatus.OnLeave },
            new Student { StudentId = 9, FirstName = "Ivan", LastName = "Taylor", BirthDate = new DateOnly(2003, 12, 1), Email = "ivan@example.com", Phone = "555-0109", Status = StudentStatus.Expelled },
            new Student { StudentId = 10, FirstName = "Julia", LastName = "Anderson", BirthDate = new DateOnly(2005, 4, 11), Email = "julia@example.com", Phone = "555-0110", Status = StudentStatus.Active },
            new Student { StudentId = 11, FirstName = "Kevin", LastName = "Thomas", BirthDate = new DateOnly(2004, 8, 5), Email = "kevin@example.com", Phone = "555-0111", Status = StudentStatus.Active },
            new Student { StudentId = 12, FirstName = "Laura", LastName = "Jackson", BirthDate = new DateOnly(2002, 5, 23), Email = "laura@example.com", Phone = "555-0112", Status = StudentStatus.Graduated }
        );

        modelBuilder.Entity<StudentGroupEnrollment>().HasData(
            new StudentGroupEnrollment { EnrollmentId = 1, StudentId = 1, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2022, 6, 30), ReasonStart = "Admission", ReasonEnd = "Transfer to new group" },
            new StudentGroupEnrollment { EnrollmentId = 2, StudentId = 1, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = null, ReasonStart = "Transfer", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 3, StudentId = 2, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2024, 6, 30), ReasonStart = "Admission", ReasonEnd = "Graduation" },
            new StudentGroupEnrollment { EnrollmentId = 4, StudentId = 3, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = new DateOnly(2023, 1, 15), ReasonStart = "Admission", ReasonEnd = "Academic Leave" },
            new StudentGroupEnrollment { EnrollmentId = 5, StudentId = 4, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Admission", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 6, StudentId = 5, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Transfer", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 7, StudentId = 6, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Admission", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 8, StudentId = 7, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = new DateOnly(2025, 6, 30), ReasonStart = "Admission", ReasonEnd = "Graduation" },
            new StudentGroupEnrollment { EnrollmentId = 9, StudentId = 8, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Admission", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 10, StudentId = 9, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = new DateOnly(2024, 12, 20), ReasonStart = "Admission", ReasonEnd = "Expelled" },
            new StudentGroupEnrollment { EnrollmentId = 11, StudentId = 10, GroupId = 5, DateFrom = new DateOnly(2025, 9, 1), DateTo = null, ReasonStart = "Admission", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 12, StudentId = 11, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Transfer", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 13, StudentId = 12, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2024, 6, 30), ReasonStart = "Admission", ReasonEnd = "Graduation" },
            new StudentGroupEnrollment { EnrollmentId = 14, StudentId = 5, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = new DateOnly(2023, 6, 30), ReasonStart = "Admission", ReasonEnd = "Transfer to new group" }
        );

        modelBuilder.Entity<StudentSubgroupAssignment>().HasData(
            new StudentSubgroupAssignment { EnrollmentId = 1, SubgroupId = 1 },
            new StudentSubgroupAssignment { EnrollmentId = 2, SubgroupId = 3 },
            new StudentSubgroupAssignment { EnrollmentId = 3, SubgroupId = 2 },
            new StudentSubgroupAssignment { EnrollmentId = 6, SubgroupId = 4 },
            new StudentSubgroupAssignment { EnrollmentId = 7, SubgroupId = 5 },
            new StudentSubgroupAssignment { EnrollmentId = 8, SubgroupId = 3 },
            new StudentSubgroupAssignment { EnrollmentId = 9, SubgroupId = 6 },
            new StudentSubgroupAssignment { EnrollmentId = 11, SubgroupId = 7 },
            new StudentSubgroupAssignment { EnrollmentId = 12, SubgroupId = 5 },
            new StudentSubgroupAssignment { EnrollmentId = 13, SubgroupId = 2 },
            new StudentSubgroupAssignment { EnrollmentId = 14, SubgroupId = 3 }
        );

        modelBuilder.Entity<AcademicLeave>().HasData(
            new AcademicLeave { LeaveId = 1, EnrollmentId = 4, StartDate = new DateOnly(2023, 1, 16), EndDate = null, Reason = "Medical leave" },
            new AcademicLeave { LeaveId = 2, EnrollmentId = 8, StartDate = new DateOnly(2023, 11, 10), EndDate = new DateOnly(2024, 2, 10), Reason = "Internship pause" },
            new AcademicLeave { LeaveId = 3, EnrollmentId = 9, StartDate = new DateOnly(2025, 2, 1), EndDate = null, Reason = "Family circumstances" }
        );

        modelBuilder.Entity<ExternalTransfer>().HasData(
            new ExternalTransfer { TransferId = 1, StudentId = 4, InstitutionId = 2, TransferType = TransferType.In, TransferDate = new DateOnly(2023, 8, 25), Notes = "Completed first year at Stanford" },
            new ExternalTransfer { TransferId = 2, StudentId = 11, InstitutionId = 3, TransferType = TransferType.In, TransferDate = new DateOnly(2024, 8, 20), Notes = "Transferred after first semester" },
            new ExternalTransfer { TransferId = 3, StudentId = 12, InstitutionId = 4, TransferType = TransferType.Out, TransferDate = new DateOnly(2024, 7, 1), Notes = "Started master's program" }
        );

        modelBuilder.Entity<StudentPlanAssignment>().HasData(
            new StudentPlanAssignment { AssignmentId = 1, StudentId = 1, PlanId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 2, StudentId = 2, PlanId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2024, 6, 30) },
            new StudentPlanAssignment { AssignmentId = 3, StudentId = 3, PlanId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 4, StudentId = 4, PlanId = 2, DateFrom = new DateOnly(2023, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 5, StudentId = 5, PlanId = 1, DateFrom = new DateOnly(2022, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 6, StudentId = 6, PlanId = 2, DateFrom = new DateOnly(2024, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 7, StudentId = 7, PlanId = 1, DateFrom = new DateOnly(2022, 9, 1), DateTo = new DateOnly(2025, 6, 30) },
            new StudentPlanAssignment { AssignmentId = 8, StudentId = 8, PlanId = 2, DateFrom = new DateOnly(2024, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 9, StudentId = 10, PlanId = 2, DateFrom = new DateOnly(2025, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 10, StudentId = 11, PlanId = 2, DateFrom = new DateOnly(2024, 9, 1), DateTo = null },
            new StudentPlanAssignment { AssignmentId = 11, StudentId = 12, PlanId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2024, 6, 30) },
            new StudentPlanAssignment { AssignmentId = 12, StudentId = 9, PlanId = 1, DateFrom = new DateOnly(2023, 9, 1), DateTo = new DateOnly(2024, 12, 20) }
        );

        modelBuilder.Entity<StudentCourseEnrollment>().HasData(
            new StudentCourseEnrollment { CourseEnrollmentId = 1, AssignmentId = 1, DisciplineId = 1, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 2, AssignmentId = 1, DisciplineId = 2, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 3, AssignmentId = 2, DisciplineId = 1, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 4, AssignmentId = 1, DisciplineId = 3, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 5, AssignmentId = 1, DisciplineId = 4, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 6, AssignmentId = 1, DisciplineId = 6, AcademicYearStart = 2023, Status = CourseStatus.InProgress },
            new StudentCourseEnrollment { CourseEnrollmentId = 7, AssignmentId = 2, DisciplineId = 2, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 8, AssignmentId = 2, DisciplineId = 3, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 9, AssignmentId = 2, DisciplineId = 4, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 10, AssignmentId = 4, DisciplineId = 1, AcademicYearStart = 2023, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 11, AssignmentId = 4, DisciplineId = 2, AcademicYearStart = 2023, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 12, AssignmentId = 4, DisciplineId = 4, AcademicYearStart = 2024, Status = CourseStatus.InProgress },
            new StudentCourseEnrollment { CourseEnrollmentId = 13, AssignmentId = 5, DisciplineId = 1, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 14, AssignmentId = 5, DisciplineId = 2, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 15, AssignmentId = 5, DisciplineId = 3, AcademicYearStart = 2023, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 16, AssignmentId = 5, DisciplineId = 6, AcademicYearStart = 2024, Status = CourseStatus.Planned },
            new StudentCourseEnrollment { CourseEnrollmentId = 17, AssignmentId = 6, DisciplineId = 1, AcademicYearStart = 2024, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 18, AssignmentId = 6, DisciplineId = 2, AcademicYearStart = 2024, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 19, AssignmentId = 6, DisciplineId = 4, AcademicYearStart = 2025, Status = CourseStatus.InProgress },
            new StudentCourseEnrollment { CourseEnrollmentId = 20, AssignmentId = 7, DisciplineId = 1, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 21, AssignmentId = 7, DisciplineId = 2, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 22, AssignmentId = 7, DisciplineId = 3, AcademicYearStart = 2023, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 23, AssignmentId = 7, DisciplineId = 4, AcademicYearStart = 2023, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 24, AssignmentId = 7, DisciplineId = 6, AcademicYearStart = 2024, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 25, AssignmentId = 8, DisciplineId = 1, AcademicYearStart = 2024, Status = CourseStatus.Retake },
            new StudentCourseEnrollment { CourseEnrollmentId = 26, AssignmentId = 8, DisciplineId = 2, AcademicYearStart = 2024, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 27, AssignmentId = 9, DisciplineId = 1, AcademicYearStart = 2025, Status = CourseStatus.InProgress },
            new StudentCourseEnrollment { CourseEnrollmentId = 28, AssignmentId = 9, DisciplineId = 5, AcademicYearStart = 2025, Status = CourseStatus.Planned },
            new StudentCourseEnrollment { CourseEnrollmentId = 29, AssignmentId = 10, DisciplineId = 1, AcademicYearStart = 2024, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 30, AssignmentId = 10, DisciplineId = 2, AcademicYearStart = 2024, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 31, AssignmentId = 10, DisciplineId = 8, AcademicYearStart = 2025, Status = CourseStatus.InProgress },
            new StudentCourseEnrollment { CourseEnrollmentId = 32, AssignmentId = 11, DisciplineId = 1, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 33, AssignmentId = 11, DisciplineId = 2, AcademicYearStart = 2021, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 34, AssignmentId = 11, DisciplineId = 3, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 35, AssignmentId = 11, DisciplineId = 4, AcademicYearStart = 2022, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 36, AssignmentId = 12, DisciplineId = 1, AcademicYearStart = 2023, Status = CourseStatus.Completed },
            new StudentCourseEnrollment { CourseEnrollmentId = 37, AssignmentId = 12, DisciplineId = 2, AcademicYearStart = 2023, Status = CourseStatus.Retake }
        );

        modelBuilder.Entity<GradeRecord>().HasData(
            new GradeRecord { GradeId = 1, CourseEnrollmentId = 1, GradeValue = "95", AssessmentDate = new DateOnly(2022, 1, 15) },
            new GradeRecord { GradeId = 2, CourseEnrollmentId = 2, GradeValue = "88", AssessmentDate = new DateOnly(2022, 1, 18) },
            new GradeRecord { GradeId = 3, CourseEnrollmentId = 3, GradeValue = "91", AssessmentDate = new DateOnly(2022, 1, 15) },
            new GradeRecord { GradeId = 4, CourseEnrollmentId = 4, GradeValue = "89", AssessmentDate = new DateOnly(2022, 6, 10) },
            new GradeRecord { GradeId = 5, CourseEnrollmentId = 5, GradeValue = "84", AssessmentDate = new DateOnly(2022, 6, 14) },
            new GradeRecord { GradeId = 6, CourseEnrollmentId = 7, GradeValue = "90", AssessmentDate = new DateOnly(2022, 1, 16) },
            new GradeRecord { GradeId = 7, CourseEnrollmentId = 8, GradeValue = "86", AssessmentDate = new DateOnly(2022, 6, 12) },
            new GradeRecord { GradeId = 8, CourseEnrollmentId = 9, GradeValue = "79", AssessmentDate = new DateOnly(2022, 6, 15) },
            new GradeRecord { GradeId = 9, CourseEnrollmentId = 10, GradeValue = "92", AssessmentDate = new DateOnly(2024, 1, 20) },
            new GradeRecord { GradeId = 10, CourseEnrollmentId = 11, GradeValue = "87", AssessmentDate = new DateOnly(2024, 1, 22) },
            new GradeRecord { GradeId = 11, CourseEnrollmentId = 13, GradeValue = "81", AssessmentDate = new DateOnly(2023, 1, 18) },
            new GradeRecord { GradeId = 12, CourseEnrollmentId = 14, GradeValue = "85", AssessmentDate = new DateOnly(2023, 1, 20) },
            new GradeRecord { GradeId = 13, CourseEnrollmentId = 15, GradeValue = "88", AssessmentDate = new DateOnly(2023, 6, 19) },
            new GradeRecord { GradeId = 14, CourseEnrollmentId = 17, GradeValue = "93", AssessmentDate = new DateOnly(2025, 1, 21) },
            new GradeRecord { GradeId = 15, CourseEnrollmentId = 18, GradeValue = "89", AssessmentDate = new DateOnly(2025, 1, 23) },
            new GradeRecord { GradeId = 16, CourseEnrollmentId = 20, GradeValue = "76", AssessmentDate = new DateOnly(2023, 1, 19) },
            new GradeRecord { GradeId = 17, CourseEnrollmentId = 21, GradeValue = "82", AssessmentDate = new DateOnly(2023, 1, 22) },
            new GradeRecord { GradeId = 18, CourseEnrollmentId = 22, GradeValue = "80", AssessmentDate = new DateOnly(2023, 6, 15) },
            new GradeRecord { GradeId = 19, CourseEnrollmentId = 23, GradeValue = "78", AssessmentDate = new DateOnly(2023, 6, 20) },
            new GradeRecord { GradeId = 20, CourseEnrollmentId = 24, GradeValue = "84", AssessmentDate = new DateOnly(2024, 12, 18) },
            new GradeRecord { GradeId = 21, CourseEnrollmentId = 25, GradeValue = "74", AssessmentDate = new DateOnly(2025, 1, 28) },
            new GradeRecord { GradeId = 22, CourseEnrollmentId = 26, GradeValue = "83", AssessmentDate = new DateOnly(2025, 1, 25) },
            new GradeRecord { GradeId = 23, CourseEnrollmentId = 29, GradeValue = "91", AssessmentDate = new DateOnly(2025, 1, 24) },
            new GradeRecord { GradeId = 24, CourseEnrollmentId = 30, GradeValue = "88", AssessmentDate = new DateOnly(2025, 1, 26) },
            new GradeRecord { GradeId = 25, CourseEnrollmentId = 32, GradeValue = "94", AssessmentDate = new DateOnly(2022, 1, 18) },
            new GradeRecord { GradeId = 26, CourseEnrollmentId = 33, GradeValue = "90", AssessmentDate = new DateOnly(2022, 1, 21) },
            new GradeRecord { GradeId = 27, CourseEnrollmentId = 34, GradeValue = "87", AssessmentDate = new DateOnly(2022, 6, 16) },
            new GradeRecord { GradeId = 28, CourseEnrollmentId = 35, GradeValue = "85", AssessmentDate = new DateOnly(2022, 6, 18) },
            new GradeRecord { GradeId = 29, CourseEnrollmentId = 36, GradeValue = "69", AssessmentDate = new DateOnly(2024, 1, 15) },
            new GradeRecord { GradeId = 30, CourseEnrollmentId = 37, GradeValue = "72", AssessmentDate = new DateOnly(2024, 1, 19) }
        );
    }
}
