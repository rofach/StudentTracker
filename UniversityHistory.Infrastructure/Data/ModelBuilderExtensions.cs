using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var institutions = new[]
        {
            new Institution { InstitutionId = 1, InstitutionName = "Національний технічний університет України \"Київський політехнічний інститут імені Ігоря Сікорського\"", City = "Київ", Country = "Україна" },
            new Institution { InstitutionId = 2, InstitutionName = "Національний університет \"Львівська політехніка\"", City = "Львів", Country = "Україна" },
            new Institution { InstitutionId = 3, InstitutionName = "Київський національний університет імені Тараса Шевченка", City = "Київ", Country = "Україна" },
            new Institution { InstitutionId = 4, InstitutionName = "Харківський національний університет радіоелектроніки", City = "Харків", Country = "Україна" },
            new Institution { InstitutionId = 5, InstitutionName = "Національний університет \"Одеська політехніка\"", City = "Одеса", Country = "Україна" },
            new Institution { InstitutionId = 6, InstitutionName = "Національний університет \"Чернігівська політехніка\"", City = "Чернігів", Country = "Україна" }
        };

        var disciplines = new[]
        {
            new Discipline { DisciplineId = 1, DisciplineName = "Вища математика" },
            new Discipline { DisciplineId = 2, DisciplineName = "Основи програмування" },
            new Discipline { DisciplineId = 3, DisciplineName = "Дискретна математика" },
            new Discipline { DisciplineId = 4, DisciplineName = "Алгоритми та структури даних" },
            new Discipline { DisciplineId = 5, DisciplineName = "Лінійна алгебра" },
            new Discipline { DisciplineId = 6, DisciplineName = "Об'єктно-орієнтоване програмування" },
            new Discipline { DisciplineId = 7, DisciplineName = "Бази даних" },
            new Discipline { DisciplineId = 8, DisciplineName = "Операційні системи" },
            new Discipline { DisciplineId = 9, DisciplineName = "Комп'ютерні мережі" },
            new Discipline { DisciplineId = 10, DisciplineName = "Вебтехнології" },
            new Discipline { DisciplineId = 11, DisciplineName = "Архітектура комп'ютерів" },
            new Discipline { DisciplineId = 12, DisciplineName = "Теорія ймовірностей" }
        };

        var studyPlans = new[]
        {
            new StudyPlan { PlanId = 1, SpecialtyCode = "122", PlanName = "Комп'ютерні науки 2021", ValidFrom = new DateOnly(2021, 9, 1) },
            new StudyPlan { PlanId = 2, SpecialtyCode = "121", PlanName = "Інженерія програмного забезпечення 2023", ValidFrom = new DateOnly(2023, 9, 1) },
            new StudyPlan { PlanId = 3, SpecialtyCode = "123", PlanName = "Комп'ютерна інженерія 2024", ValidFrom = new DateOnly(2024, 9, 1) }
        };

        var planDisciplines = new[]
        {
            new PlanDiscipline { PlanId = 1, DisciplineId = 1, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 2, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 3, SemesterNo = 1, ControlType = ControlType.Credit, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 5, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 4, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 6, SemesterNo = 3, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 7, SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 12, SemesterNo = 4, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 8, SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 10, SemesterNo = 5, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 1, DisciplineId = 9, SemesterNo = 5, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },

            new PlanDiscipline { PlanId = 2, DisciplineId = 1, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 2, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 3, SemesterNo = 1, ControlType = ControlType.Credit, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 5, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 6, SemesterNo = 2, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 7, SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 10, SemesterNo = 3, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 8, SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 2, DisciplineId = 9, SemesterNo = 4, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },

            new PlanDiscipline { PlanId = 3, DisciplineId = 1, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 3, DisciplineId = 2, SemesterNo = 1, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = 3, DisciplineId = 5, SemesterNo = 1, ControlType = ControlType.Credit, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 3, DisciplineId = 11, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 3, DisciplineId = 4, SemesterNo = 2, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = 3, DisciplineId = 8, SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = 3, DisciplineId = 9, SemesterNo = 3, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = 3, DisciplineId = 7, SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m }
        };

        var academicUnits = new[]
        {
            new AcademicUnit { AcademicUnitId = 1, Name = "Факультет інформатики та обчислювальної техніки", Type = AcademicUnitType.Faculty },
            new AcademicUnit { AcademicUnitId = 2, Name = "Факультет прикладної математики", Type = AcademicUnitType.Faculty },
            new AcademicUnit { AcademicUnitId = 3, Name = "Факультет комп'ютерної інженерії", Type = AcademicUnitType.Faculty }
        };

        var departments = new[]
        {
            new Department { DepartmentId = 1, AcademicUnitId = 1, Name = "Кафедра програмування" },
            new Department { DepartmentId = 2, AcademicUnitId = 1, Name = "Кафедра комп'ютерних наук" },
            new Department { DepartmentId = 3, AcademicUnitId = 2, Name = "Кафедра прикладної математики" },
            new Department { DepartmentId = 4, AcademicUnitId = 2, Name = "Кафедра програмного забезпечення" },
            new Department { DepartmentId = 5, AcademicUnitId = 3, Name = "Кафедра комп'ютерної інженерії" },
            new Department { DepartmentId = 6, AcademicUnitId = 3, Name = "Кафедра вбудованих систем" }
        };

        var studyGroups = new[]
        {
            new StudyGroup { GroupId = 1, GroupCode = "КН-21", DepartmentId = 1, DateCreated = new DateOnly(2021, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 2, GroupCode = "КН-22", DepartmentId = 2, DateCreated = new DateOnly(2022, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 3, GroupCode = "ПЗ-23", DepartmentId = 4, DateCreated = new DateOnly(2023, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 4, GroupCode = "КІ-24", DepartmentId = 5, DateCreated = new DateOnly(2024, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 5, GroupCode = "ПЗ-24", DepartmentId = 4, DateCreated = new DateOnly(2024, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = 6, GroupCode = "КІ-25", DepartmentId = 5, DateCreated = new DateOnly(2025, 9, 1), DateClosed = null }
        };

        var subgroups = new[]
        {
            new Subgroup { SubgroupId = 1, GroupId = 1, SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = 2, GroupId = 1, SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = 3, GroupId = 2, SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = 4, GroupId = 2, SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = 5, GroupId = 3, SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = 6, GroupId = 3, SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = 7, GroupId = 4, SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = 8, GroupId = 4, SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = 9, GroupId = 5, SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = 10, GroupId = 5, SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = 11, GroupId = 6, SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = 12, GroupId = 6, SubgroupName = "Підгрупа 2" }
        };

        var students = new[]
        {
            new Student { StudentId = 1, FirstName = "Андрій", LastName = "Мельник", Patronymic = "Олександрович", BirthDate = new DateOnly(2003, 4, 15), Email = "andrii.melnyk@campus.ua", Phone = "+380671000001", Status = StudentStatus.Active },
            new Student { StudentId = 2, FirstName = "Олена", LastName = "Коваль", Patronymic = "Ігорівна", BirthDate = new DateOnly(2002, 9, 3), Email = "olena.koval@campus.ua", Phone = "+380671000002", Status = StudentStatus.Graduated },
            new Student { StudentId = 3, FirstName = "Максим", LastName = "Шевченко", Patronymic = "Сергійович", BirthDate = new DateOnly(2004, 1, 27), Email = "maksym.shevchenko@campus.ua", Phone = "+380671000003", Status = StudentStatus.Active },
            new Student { StudentId = 4, FirstName = "Ірина", LastName = "Бойко", Patronymic = "Василівна", BirthDate = new DateOnly(2004, 6, 11), Email = "iryna.boiko@campus.ua", Phone = "+380671000004", Status = StudentStatus.Active },
            new Student { StudentId = 5, FirstName = "Богдан", LastName = "Ткаченко", Patronymic = "Романович", BirthDate = new DateOnly(2004, 12, 1), Email = "bohdan.tkachenko@campus.ua", Phone = "+380671000005", Status = StudentStatus.OnLeave },
            new Student { StudentId = 6, FirstName = "Марія", LastName = "Кравець", Patronymic = "Андріївна", BirthDate = new DateOnly(2004, 2, 18), Email = "mariia.kravets@campus.ua", Phone = "+380671000006", Status = StudentStatus.Active },
            new Student { StudentId = 7, FirstName = "Дмитро", LastName = "Поліщук", Patronymic = "Олегович", BirthDate = new DateOnly(2004, 8, 5), Email = "dmytro.polishchuk@campus.ua", Phone = "+380671000007", Status = StudentStatus.Active },
            new Student { StudentId = 8, FirstName = "Наталія", LastName = "Савчук", Patronymic = "Вікторівна", BirthDate = new DateOnly(2004, 11, 22), Email = "nataliia.savchuk@campus.ua", Phone = "+380671000008", Status = StudentStatus.Active },
            new Student { StudentId = 9, FirstName = "Владислав", LastName = "Романюк", Patronymic = "Миколайович", BirthDate = new DateOnly(2003, 7, 14), Email = "vladyslav.romaniuk@campus.ua", Phone = "+380671000009", Status = StudentStatus.Active },
            new Student { StudentId = 10, FirstName = "Софія", LastName = "Козак", Patronymic = "Петрівна", BirthDate = new DateOnly(2005, 3, 8), Email = "sofiia.kozak@campus.ua", Phone = "+380671000010", Status = StudentStatus.Active },
            new Student { StudentId = 11, FirstName = "Артем", LastName = "Литвин", Patronymic = "Володимирович", BirthDate = new DateOnly(2005, 9, 21), Email = "artem.lytvyn@campus.ua", Phone = "+380671000011", Status = StudentStatus.Active },
            new Student { StudentId = 12, FirstName = "Катерина", LastName = "Павленко", Patronymic = "Юріївна", BirthDate = new DateOnly(2005, 5, 12), Email = "kateryna.pavlenko@campus.ua", Phone = "+380671000012", Status = StudentStatus.Active },
            new Student { StudentId = 13, FirstName = "Юрій", LastName = "Мороз", Patronymic = "Степанович", BirthDate = new DateOnly(2002, 2, 17), Email = "yurii.moroz@campus.ua", Phone = "+380671000013", Status = StudentStatus.Graduated },
            new Student { StudentId = 14, FirstName = "Анастасія", LastName = "Іванчук", Patronymic = "Павлівна", BirthDate = new DateOnly(2005, 1, 30), Email = "anastasiia.ivanchuk@campus.ua", Phone = "+380671000014", Status = StudentStatus.Active },
            new Student { StudentId = 15, FirstName = "Денис", LastName = "Олійник", Patronymic = "Олексійович", BirthDate = new DateOnly(2004, 10, 6), Email = "denys.oliinyk@campus.ua", Phone = "+380671000015", Status = StudentStatus.Expelled },
            new Student { StudentId = 16, FirstName = "Вероніка", LastName = "Гнатюк", Patronymic = "Іванівна", BirthDate = new DateOnly(2005, 7, 19), Email = "veronika.hnatiuk@campus.ua", Phone = "+380671000016", Status = StudentStatus.Active },
            new Student { StudentId = 17, FirstName = "Тарас", LastName = "Бондар", Patronymic = "Михайлович", BirthDate = new DateOnly(2004, 4, 2), Email = "taras.bondar@campus.ua", Phone = "+380671000017", Status = StudentStatus.Active },
            new Student { StudentId = 18, FirstName = "Христина", LastName = "Федорук", Patronymic = "Богданівна", BirthDate = new DateOnly(2005, 8, 9), Email = "khrystyna.fedoruk@campus.ua", Phone = "+380671000018", Status = StudentStatus.OnLeave },
            new Student { StudentId = 19, FirstName = "Роман", LastName = "Сорока", Patronymic = "Дмитрович", BirthDate = new DateOnly(2004, 3, 25), Email = "roman.soroka@campus.ua", Phone = "+380671000019", Status = StudentStatus.Active },
            new Student { StudentId = 20, FirstName = "Дарина", LastName = "Мазур", Patronymic = "Сергіївна", BirthDate = new DateOnly(2005, 11, 13), Email = "daryna.mazur@campus.ua", Phone = "+380671000020", Status = StudentStatus.Active },
            new Student { StudentId = 21, FirstName = "Павло", LastName = "Дяченко", Patronymic = "Віталійович", BirthDate = new DateOnly(2006, 2, 16), Email = "pavlo.diachenko@campus.ua", Phone = "+380671000021", Status = StudentStatus.Active },
            new Student { StudentId = 22, FirstName = "Юлія", LastName = "Власенко", Patronymic = "Анатоліївна", BirthDate = new DateOnly(2006, 4, 1), Email = "yuliia.vlasenko@campus.ua", Phone = "+380671000022", Status = StudentStatus.Active },
            new Student { StudentId = 23, FirstName = "Ілля", LastName = "Ковтун", Patronymic = "Романович", BirthDate = new DateOnly(2006, 6, 6), Email = "illia.kovtun@campus.ua", Phone = "+380671000023", Status = StudentStatus.Active },
            new Student { StudentId = 24, FirstName = "Оксана", LastName = "Чумак", Patronymic = "Миколаївна", BirthDate = new DateOnly(2005, 9, 28), Email = "oksana.chumak@campus.ua", Phone = "+380671000024", Status = StudentStatus.Active }
        };

        var enrollments = new[]
        {
            new StudentGroupEnrollment { EnrollmentId = 1, StudentId = 1, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2022, 8, 31), ReasonStart = "Вступ", ReasonEnd = "Переведення" },
            new StudentGroupEnrollment { EnrollmentId = 2, StudentId = 1, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = null, ReasonStart = "Переведення", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 3, StudentId = 2, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2025, 6, 30), ReasonStart = "Вступ", ReasonEnd = "Випуск" },
            new StudentGroupEnrollment { EnrollmentId = 4, StudentId = 3, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 5, StudentId = 4, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 6, StudentId = 5, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = new DateOnly(2024, 1, 31), ReasonStart = "Вступ", ReasonEnd = "Академвідпустка" },
            new StudentGroupEnrollment { EnrollmentId = 7, StudentId = 6, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 8, StudentId = 7, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 9, StudentId = 8, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 10, StudentId = 9, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 11, StudentId = 10, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 12, StudentId = 11, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 13, StudentId = 12, GroupId = 5, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 14, StudentId = 13, GroupId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2025, 6, 30), ReasonStart = "Вступ", ReasonEnd = "Випуск" },
            new StudentGroupEnrollment { EnrollmentId = 15, StudentId = 14, GroupId = 5, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 16, StudentId = 15, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = new DateOnly(2025, 2, 14), ReasonStart = "Вступ", ReasonEnd = "Відрахування" },
            new StudentGroupEnrollment { EnrollmentId = 17, StudentId = 16, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 18, StudentId = 17, GroupId = 2, DateFrom = new DateOnly(2022, 9, 1), DateTo = new DateOnly(2024, 8, 31), ReasonStart = "Вступ", ReasonEnd = "Переведення" },
            new StudentGroupEnrollment { EnrollmentId = 19, StudentId = 17, GroupId = 3, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Переведення", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 20, StudentId = 18, GroupId = 5, DateFrom = new DateOnly(2024, 9, 1), DateTo = new DateOnly(2025, 2, 1), ReasonStart = "Вступ", ReasonEnd = "Академвідпустка" },
            new StudentGroupEnrollment { EnrollmentId = 21, StudentId = 19, GroupId = 3, DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 22, StudentId = 20, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 23, StudentId = 21, GroupId = 6, DateFrom = new DateOnly(2025, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 24, StudentId = 22, GroupId = 6, DateFrom = new DateOnly(2025, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 25, StudentId = 23, GroupId = 6, DateFrom = new DateOnly(2025, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = 26, StudentId = 24, GroupId = 4, DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null }
        };

        var subgroupAssignments = new[]
        {
            new StudentSubgroupAssignment { EnrollmentId = 1, SubgroupId = 1 },
            new StudentSubgroupAssignment { EnrollmentId = 2, SubgroupId = 3 },
            new StudentSubgroupAssignment { EnrollmentId = 3, SubgroupId = 2 },
            new StudentSubgroupAssignment { EnrollmentId = 4, SubgroupId = 4 },
            new StudentSubgroupAssignment { EnrollmentId = 5, SubgroupId = 5 },
            new StudentSubgroupAssignment { EnrollmentId = 7, SubgroupId = 6 },
            new StudentSubgroupAssignment { EnrollmentId = 8, SubgroupId = 5 },
            new StudentSubgroupAssignment { EnrollmentId = 9, SubgroupId = 6 },
            new StudentSubgroupAssignment { EnrollmentId = 10, SubgroupId = 5 },
            new StudentSubgroupAssignment { EnrollmentId = 11, SubgroupId = 7 },
            new StudentSubgroupAssignment { EnrollmentId = 12, SubgroupId = 8 },
            new StudentSubgroupAssignment { EnrollmentId = 13, SubgroupId = 9 },
            new StudentSubgroupAssignment { EnrollmentId = 14, SubgroupId = 1 },
            new StudentSubgroupAssignment { EnrollmentId = 15, SubgroupId = 10 },
            new StudentSubgroupAssignment { EnrollmentId = 17, SubgroupId = 8 },
            new StudentSubgroupAssignment { EnrollmentId = 18, SubgroupId = 3 },
            new StudentSubgroupAssignment { EnrollmentId = 19, SubgroupId = 6 },
            new StudentSubgroupAssignment { EnrollmentId = 20, SubgroupId = 9 },
            new StudentSubgroupAssignment { EnrollmentId = 21, SubgroupId = 6 },
            new StudentSubgroupAssignment { EnrollmentId = 22, SubgroupId = 7 },
            new StudentSubgroupAssignment { EnrollmentId = 23, SubgroupId = 11 },
            new StudentSubgroupAssignment { EnrollmentId = 24, SubgroupId = 12 },
            new StudentSubgroupAssignment { EnrollmentId = 25, SubgroupId = 11 },
            new StudentSubgroupAssignment { EnrollmentId = 26, SubgroupId = 8 }
        };

        var academicLeaves = new[]
        {
            new AcademicLeave { LeaveId = 1, EnrollmentId = 6, StartDate = new DateOnly(2024, 2, 1), EndDate = null, Reason = "Стан здоров'я" },
            new AcademicLeave { LeaveId = 2, EnrollmentId = 20, StartDate = new DateOnly(2025, 2, 2), EndDate = null, Reason = "Сімейні обставини" },
            new AcademicLeave { LeaveId = 3, EnrollmentId = 9, StartDate = new DateOnly(2024, 11, 4), EndDate = new DateOnly(2025, 1, 20), Reason = "Програма академічної мобільності" }
        };

        var externalTransfers = new[]
        {
            new ExternalTransfer { TransferId = 1, StudentId = 4, InstitutionId = 4, TransferType = TransferType.In, TransferDate = new DateOnly(2023, 8, 25), Notes = "Переведення після другого семестру" },
            new ExternalTransfer { TransferId = 2, StudentId = 11, InstitutionId = 3, TransferType = TransferType.In, TransferDate = new DateOnly(2024, 8, 28), Notes = "Продовження навчання після переїзду" },
            new ExternalTransfer { TransferId = 3, StudentId = 13, InstitutionId = 2, TransferType = TransferType.Out, TransferDate = new DateOnly(2025, 7, 2), Notes = "Подальше навчання в магістратурі" },
            new ExternalTransfer { TransferId = 4, StudentId = 24, InstitutionId = 6, TransferType = TransferType.In, TransferDate = new DateOnly(2024, 8, 26), Notes = "Переведення з іншого закладу" }
        };

        var groupPlanAssignments = new[]
        {
            new GroupPlanAssignment { GroupPlanAssignmentId = 1, GroupId = 1, PlanId = 1, DateFrom = new DateOnly(2021, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = 2, GroupId = 2, PlanId = 1, DateFrom = new DateOnly(2022, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = 3, GroupId = 3, PlanId = 2, DateFrom = new DateOnly(2023, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = 4, GroupId = 4, PlanId = 3, DateFrom = new DateOnly(2024, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = 5, GroupId = 5, PlanId = 2, DateFrom = new DateOnly(2024, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = 6, GroupId = 6, PlanId = 3, DateFrom = new DateOnly(2025, 9, 1), DateTo = null },
        };

        var courseEnrollments = new List<StudentCourseEnrollment>();
        var gradeRecords = new List<GradeRecord>();
        var nextCourseEnrollmentId = 1;
        var nextGradeId = 1;

        DateOnly BuildAssessmentDate(int academicYearStart, int disciplineId)
        {
            return new DateOnly(academicYearStart + 1, (disciplineId % 12) + 1, Math.Min(10 + disciplineId, 28));
        }

        void AddCourse(int enrollmentId, int gpaId, int disciplineId, int academicYearStart, CourseStatus status, string? gradeValue = null)
        {
            var ceId = nextCourseEnrollmentId++;
            courseEnrollments.Add(new StudentCourseEnrollment
            {
                CourseEnrollmentId = ceId,
                EnrollmentId = enrollmentId,
                GroupPlanAssignmentId = gpaId,
                DisciplineId = disciplineId,
                AcademicYearStart = academicYearStart,
                Status = status
            });
            if (!string.IsNullOrWhiteSpace(gradeValue))
            {
                gradeRecords.Add(new GradeRecord
                {
                    GradeId = nextGradeId++,
                    CourseEnrollmentId = ceId,
                    GradeValue = gradeValue,
                    AssessmentDate = BuildAssessmentDate(academicYearStart, disciplineId)
                });
            }
        }

        void AddCourses(int enrollmentId, int gpaId, int academicYearStart,
            params (int DisciplineId, CourseStatus Status, string? GradeValue)[] courses)
        {
            foreach (var c in courses)
                AddCourse(enrollmentId, gpaId, c.DisciplineId, academicYearStart, c.Status, c.GradeValue);
        }

        AddCourses(2, 2, 2021,
            (1, CourseStatus.Completed, "96"),
            (2, CourseStatus.Completed, "94"),
            (3, CourseStatus.Completed, "90"));
        AddCourses(2, 2, 2022,
            (5, CourseStatus.Completed, "91"),
            (4, CourseStatus.Completed, "93"));
        AddCourses(2, 2, 2023,
            (6, CourseStatus.Completed, "95"));
        AddCourses(2, 2, 2024,
            (7, CourseStatus.InProgress, null),
            (12, CourseStatus.Planned, null));

        AddCourses(3, 1, 2021,
            (1, CourseStatus.Completed, "98"),
            (2, CourseStatus.Completed, "95"),
            (3, CourseStatus.Completed, "92"));
        AddCourses(3, 1, 2022,
            (5, CourseStatus.Completed, "94"),
            (4, CourseStatus.Completed, "96"));
        AddCourses(3, 1, 2023,
            (6, CourseStatus.Completed, "93"),
            (7, CourseStatus.Completed, "91"));
        AddCourses(3, 1, 2024,
            (12, CourseStatus.Completed, "90"),
            (8, CourseStatus.Completed, "89"));
        AddCourses(3, 1, 2025,
            (10, CourseStatus.Completed, "92"),
            (9, CourseStatus.Completed, "88"));

        AddCourses(4, 2, 2022,
            (1, CourseStatus.Completed, "87"),
            (2, CourseStatus.Completed, "89"),
            (3, CourseStatus.Completed, "84"));
        AddCourses(4, 2, 2023,
            (5, CourseStatus.Completed, "86"),
            (4, CourseStatus.Completed, "88"));
        AddCourses(4, 2, 2024,
            (6, CourseStatus.InProgress, null));

        AddCourses(5, 3, 2023,
            (1, CourseStatus.Completed, "90"),
            (2, CourseStatus.Completed, "92"),
            (3, CourseStatus.Completed, "88"));
        AddCourses(5, 3, 2024,
            (5, CourseStatus.Completed, "91"),
            (6, CourseStatus.InProgress, null));

        AddCourses(6, 3, 2023,
            (1, CourseStatus.Completed, "83"),
            (2, CourseStatus.Completed, "85"),
            (3, CourseStatus.Completed, "81"));
        AddCourses(6, 3, 2024,
            (5, CourseStatus.Completed, "84"));

        AddCourses(7, 3, 2023,
            (1, CourseStatus.Completed, "88"),
            (2, CourseStatus.Completed, "87"),
            (3, CourseStatus.Completed, "86"));
        AddCourses(7, 3, 2024,
            (5, CourseStatus.Completed, "89"),
            (6, CourseStatus.InProgress, null));

        AddCourses(8, 3, 2023,
            (1, CourseStatus.Completed, "82"),
            (2, CourseStatus.Completed, "84"),
            (3, CourseStatus.Completed, "80"));
        AddCourses(8, 3, 2024,
            (5, CourseStatus.Completed, "83"),
            (6, CourseStatus.Completed, "85"));
        AddCourses(8, 3, 2025,
            (7, CourseStatus.InProgress, null));

        AddCourses(9, 3, 2023,
            (1, CourseStatus.Completed, "86"),
            (2, CourseStatus.Completed, "88"),
            (3, CourseStatus.Completed, "85"));
        AddCourses(9, 3, 2024,
            (5, CourseStatus.Completed, "87"),
            (10, CourseStatus.Retake, null));

        AddCourses(10, 3, 2023,
            (1, CourseStatus.Completed, "79"),
            (2, CourseStatus.Completed, "82"),
            (3, CourseStatus.Completed, "78"));
        AddCourses(10, 3, 2024,
            (5, CourseStatus.Completed, "81"),
            (6, CourseStatus.InProgress, null));

        AddCourses(11, 4, 2024,
            (1, CourseStatus.Completed, "93"),
            (2, CourseStatus.Completed, "95"),
            (5, CourseStatus.Completed, "90"));
        AddCourses(11, 4, 2025,
            (11, CourseStatus.InProgress, null));

        AddCourses(12, 4, 2024,
            (1, CourseStatus.Completed, "91"),
            (2, CourseStatus.Completed, "92"),
            (5, CourseStatus.Completed, "89"));
        AddCourses(12, 4, 2025,
            (11, CourseStatus.InProgress, null));

        AddCourses(13, 5, 2024,
            (1, CourseStatus.Completed, "94"),
            (2, CourseStatus.Completed, "90"),
            (5, CourseStatus.Completed, "88"));
        AddCourses(13, 5, 2025,
            (11, CourseStatus.InProgress, null));

        AddCourses(14, 1, 2021,
            (1, CourseStatus.Completed, "97"),
            (2, CourseStatus.Completed, "96"),
            (3, CourseStatus.Completed, "93"));
        AddCourses(14, 1, 2022,
            (5, CourseStatus.Completed, "95"),
            (4, CourseStatus.Completed, "94"));
        AddCourses(14, 1, 2023,
            (6, CourseStatus.Completed, "92"),
            (7, CourseStatus.Completed, "91"));
        AddCourses(14, 1, 2024,
            (12, CourseStatus.Completed, "90"),
            (8, CourseStatus.Completed, "89"));

        AddCourses(15, 5, 2024,
            (1, CourseStatus.Completed, "90"),
            (2, CourseStatus.Completed, "91"),
            (3, CourseStatus.InProgress, null));

        AddCourses(16, 3, 2023,
            (1, CourseStatus.Completed, "73"),
            (2, CourseStatus.Completed, "76"),
            (3, CourseStatus.Completed, "71"));
        AddCourses(16, 3, 2024,
            (5, CourseStatus.Completed, "74"));

        AddCourses(17, 4, 2024,
            (1, CourseStatus.Completed, "89"),
            (2, CourseStatus.Completed, "91"),
            (5, CourseStatus.Completed, "87"));
        AddCourses(17, 4, 2025,
            (11, CourseStatus.InProgress, null));

        AddCourses(18, 2, 2022,
            (1, CourseStatus.Completed, "88"),
            (2, CourseStatus.Completed, "86"),
            (3, CourseStatus.Completed, "84"));
        AddCourses(18, 2, 2023,
            (5, CourseStatus.Completed, "87"),
            (4, CourseStatus.Completed, "85"));
        AddCourses(18, 2, 2024,
            (6, CourseStatus.Completed, "89"));
        AddCourses(18, 2, 2025,
            (7, CourseStatus.InProgress, null));

        AddCourses(20, 5, 2024,
            (1, CourseStatus.Completed, "85"),
            (2, CourseStatus.Completed, "84"));
        AddCourses(20, 5, 2025,
            (3, CourseStatus.Planned, null));

        AddCourses(21, 3, 2023,
            (1, CourseStatus.Completed, "84"),
            (2, CourseStatus.Completed, "86"));
        AddCourses(21, 3, 2024,
            (3, CourseStatus.Completed, "82"),
            (5, CourseStatus.Completed, "85"));
        AddCourses(21, 3, 2025,
            (6, CourseStatus.InProgress, null));

        AddCourses(22, 4, 2024,
            (1, CourseStatus.Completed, "92"),
            (2, CourseStatus.Completed, "90"),
            (5, CourseStatus.Completed, "88"));

        AddCourses(23, 6, 2025,
            (1, CourseStatus.InProgress, null),
            (2, CourseStatus.InProgress, null));

        AddCourses(24, 6, 2025,
            (1, CourseStatus.InProgress, null),
            (2, CourseStatus.InProgress, null));

        AddCourses(25, 6, 2025,
            (1, CourseStatus.InProgress, null),
            (2, CourseStatus.InProgress, null));

        AddCourses(26, 4, 2024,
            (1, CourseStatus.Completed, "90"),
            (2, CourseStatus.Completed, "89"),
            (5, CourseStatus.Completed, "87"));
        AddCourses(26, 4, 2025,
            (11, CourseStatus.InProgress, null));

        modelBuilder.Entity<Institution>().HasData(institutions);
        modelBuilder.Entity<AcademicUnit>().HasData(academicUnits);
        modelBuilder.Entity<Department>().HasData(departments);
        modelBuilder.Entity<Discipline>().HasData(disciplines);
        modelBuilder.Entity<StudyPlan>().HasData(studyPlans);
        modelBuilder.Entity<PlanDiscipline>().HasData(planDisciplines);
        modelBuilder.Entity<StudyGroup>().HasData(studyGroups);
        modelBuilder.Entity<Subgroup>().HasData(subgroups);
        modelBuilder.Entity<Student>().HasData(students);
        modelBuilder.Entity<StudentGroupEnrollment>().HasData(enrollments);
        modelBuilder.Entity<StudentSubgroupAssignment>().HasData(subgroupAssignments);
        modelBuilder.Entity<AcademicLeave>().HasData(academicLeaves);
        modelBuilder.Entity<ExternalTransfer>().HasData(externalTransfers);
        modelBuilder.Entity<GroupPlanAssignment>().HasData(groupPlanAssignments);
        modelBuilder.Entity<StudentCourseEnrollment>().HasData(courseEnrollments.ToArray());
        modelBuilder.Entity<GradeRecord>().HasData(gradeRecords.ToArray());
    }
}
