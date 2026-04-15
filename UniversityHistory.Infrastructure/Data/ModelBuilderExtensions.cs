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
        Guid Id(int id) => new Guid(id, 0, 0, new byte[8]);

        var institutions = new[]
        {
            new Institution { InstitutionId = Id(1), InstitutionName = "Національний технічний університет України \"Київський політехнічний інститут імені Ігоря Сікорського\"", City = "Київ", Country = "Україна" },
            new Institution { InstitutionId = Id(2), InstitutionName = "Національний університет \"Львівська політехніка\"", City = "Львів", Country = "Україна" },
            new Institution { InstitutionId = Id(3), InstitutionName = "Київський національний університет імені Тараса Шевченка", City = "Київ", Country = "Україна" },
            new Institution { InstitutionId = Id(4), InstitutionName = "Харківський національний університет радіоелектроніки", City = "Харків", Country = "Україна" },
            new Institution { InstitutionId = Id(5), InstitutionName = "Національний університет \"Одеська політехніка\"", City = "Одеса", Country = "Україна" },
            new Institution { InstitutionId = Id(6), InstitutionName = "Національний університет \"Чернігівська політехніка\"", City = "Чернігів", Country = "Україна" }
        };

        var disciplines = new[]
        {
            new Discipline { DisciplineId = Id(1), DisciplineName = "Вища математика" },
            new Discipline { DisciplineId = Id(2), DisciplineName = "Основи програмування" },
            new Discipline { DisciplineId = Id(3), DisciplineName = "Дискретна математика" },
            new Discipline { DisciplineId = Id(4), DisciplineName = "Алгоритми та структури даних" },
            new Discipline { DisciplineId = Id(5), DisciplineName = "Лінійна алгебра" },
            new Discipline { DisciplineId = Id(6), DisciplineName = "Об'єктно-орієнтоване програмування" },
            new Discipline { DisciplineId = Id(7), DisciplineName = "Бази даних" },
            new Discipline { DisciplineId = Id(8), DisciplineName = "Операційні системи" },
            new Discipline { DisciplineId = Id(9), DisciplineName = "Комп'ютерні мережі" },
            new Discipline { DisciplineId = Id(10), DisciplineName = "Вебтехнології" },
            new Discipline { DisciplineId = Id(11), DisciplineName = "Архітектура комп'ютерів" },
            new Discipline { DisciplineId = Id(12), DisciplineName = "Теорія ймовірностей" }
        };

        var studyPlans = new[]
        {
            new StudyPlan { PlanId = Id(1), SpecialtyCode = "122", PlanName = "Комп'ютерні науки 2021", ValidFrom = new DateOnly(2021, 9, 1) },
            new StudyPlan { PlanId = Id(2), SpecialtyCode = "121", PlanName = "Інженерія програмного забезпечення 2023", ValidFrom = new DateOnly(2023, 9, 1) },
            new StudyPlan { PlanId = Id(3), SpecialtyCode = "123", PlanName = "Комп'ютерна інженерія 2024", ValidFrom = new DateOnly(2024, 9, 1) }
        };

        var planDisciplines = new[]
        {
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(1), SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(2), SemesterNo = 1, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(3), SemesterNo = 1, ControlType = ControlType.Credit, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(5), SemesterNo = 2, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(4), SemesterNo = 2, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(6), SemesterNo = 3, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(7), SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(12), SemesterNo = 4, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(8), SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(10), SemesterNo = 5, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(1), DisciplineId = Id(9), SemesterNo = 5, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },

            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(1), SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(2), SemesterNo = 1, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(3), SemesterNo = 1, ControlType = ControlType.Credit, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(5), SemesterNo = 2, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(6), SemesterNo = 2, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(7), SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(10), SemesterNo = 3, ControlType = ControlType.Coursework, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(8), SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(2), DisciplineId = Id(9), SemesterNo = 4, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },

            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(1), SemesterNo = 1, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(2), SemesterNo = 1, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(5), SemesterNo = 1, ControlType = ControlType.Credit, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(11), SemesterNo = 2, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(4), SemesterNo = 2, ControlType = ControlType.Exam, Hours = 180, Credits = 6.0m },
            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(8), SemesterNo = 3, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m },
            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(9), SemesterNo = 3, ControlType = ControlType.Exam, Hours = 120, Credits = 4.0m },
            new PlanDiscipline { PlanId = Id(3), DisciplineId = Id(7), SemesterNo = 4, ControlType = ControlType.Exam, Hours = 150, Credits = 5.0m }
        };

        var academicUnits = new[]
        {
            new AcademicUnit { AcademicUnitId = Id(1), Name = "Факультет інформатики та обчислювальної техніки", Type = AcademicUnitType.Faculty },
            new AcademicUnit { AcademicUnitId = Id(2), Name = "Факультет прикладної математики", Type = AcademicUnitType.Faculty },
            new AcademicUnit { AcademicUnitId = Id(3), Name = "Факультет комп'ютерної інженерії", Type = AcademicUnitType.Faculty }
        };

        var departments = new[]
        {
            new Department { DepartmentId = Id(1), AcademicUnitId = Id(1), Name = "Кафедра програмування" },
            new Department { DepartmentId = Id(2), AcademicUnitId = Id(1), Name = "Кафедра комп'ютерних наук" },
            new Department { DepartmentId = Id(3), AcademicUnitId = Id(2), Name = "Кафедра прикладної математики" },
            new Department { DepartmentId = Id(4), AcademicUnitId = Id(2), Name = "Кафедра програмного забезпечення" },
            new Department { DepartmentId = Id(5), AcademicUnitId = Id(3), Name = "Кафедра комп'ютерної інженерії" },
            new Department { DepartmentId = Id(6), AcademicUnitId = Id(3), Name = "Кафедра вбудованих систем" }
        };

        var studyGroups = new[]
        {
            new StudyGroup { GroupId = Id(1), GroupCode = "КН-21", DepartmentId = Id(1), DateCreated = new DateOnly(2021, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = Id(2), GroupCode = "КН-22", DepartmentId = Id(2), DateCreated = new DateOnly(2022, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = Id(3), GroupCode = "ПЗ-23", DepartmentId = Id(4), DateCreated = new DateOnly(2023, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = Id(4), GroupCode = "КІ-24", DepartmentId = Id(5), DateCreated = new DateOnly(2024, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = Id(5), GroupCode = "ПЗ-24", DepartmentId = Id(4), DateCreated = new DateOnly(2024, 9, 1), DateClosed = null },
            new StudyGroup { GroupId = Id(6), GroupCode = "КІ-25", DepartmentId = Id(5), DateCreated = new DateOnly(2025, 9, 1), DateClosed = null }
        };

        var subgroups = new[]
        {
            new Subgroup { SubgroupId = Id(1), GroupId = Id(1), SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = Id(2), GroupId = Id(1), SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = Id(3), GroupId = Id(2), SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = Id(4), GroupId = Id(2), SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = Id(5), GroupId = Id(3), SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = Id(6), GroupId = Id(3), SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = Id(7), GroupId = Id(4), SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = Id(8), GroupId = Id(4), SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = Id(9), GroupId = Id(5), SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = Id(10), GroupId = Id(5), SubgroupName = "Підгрупа 2" },
            new Subgroup { SubgroupId = Id(11), GroupId = Id(6), SubgroupName = "Підгрупа 1" },
            new Subgroup { SubgroupId = Id(12), GroupId = Id(6), SubgroupName = "Підгрупа 2" }
        };

        var students = new[]
        {
            new Student { StudentId = Id(1), FirstName = "Андрій", LastName = "Мельник", Patronymic = "Олександрович", BirthDate = new DateOnly(2003, 4, 15), Email = "andrii.melnyk@campus.ua", Phone = "+380671000001", Status = StudentStatus.Active },
            new Student { StudentId = Id(2), FirstName = "Олена", LastName = "Коваль", Patronymic = "Ігорівна", BirthDate = new DateOnly(2002, 9, 3), Email = "olena.koval@campus.ua", Phone = "+380671000002", Status = StudentStatus.Graduated },
            new Student { StudentId = Id(3), FirstName = "Максим", LastName = "Шевченко", Patronymic = "Сергійович", BirthDate = new DateOnly(2004, 1, 27), Email = "maksym.shevchenko@campus.ua", Phone = "+380671000003", Status = StudentStatus.Active },
            new Student { StudentId = Id(4), FirstName = "Ірина", LastName = "Бойко", Patronymic = "Василівна", BirthDate = new DateOnly(2004, 6, 11), Email = "iryna.boiko@campus.ua", Phone = "+380671000004", Status = StudentStatus.Active },
            new Student { StudentId = Id(5), FirstName = "Богдан", LastName = "Ткаченко", Patronymic = "Романович", BirthDate = new DateOnly(2004, 12, 1), Email = "bohdan.tkachenko@campus.ua", Phone = "+380671000005", Status = StudentStatus.OnLeave },
            new Student { StudentId = Id(6), FirstName = "Марія", LastName = "Кравець", Patronymic = "Андріївна", BirthDate = new DateOnly(2004, 2, 18), Email = "mariia.kravets@campus.ua", Phone = "+380671000006", Status = StudentStatus.Active },
            new Student { StudentId = Id(7), FirstName = "Дмитро", LastName = "Поліщук", Patronymic = "Олегович", BirthDate = new DateOnly(2004, 8, 5), Email = "dmytro.polishchuk@campus.ua", Phone = "+380671000007", Status = StudentStatus.Active },
            new Student { StudentId = Id(8), FirstName = "Наталія", LastName = "Савчук", Patronymic = "Вікторівна", BirthDate = new DateOnly(2004, 11, 22), Email = "nataliia.savchuk@campus.ua", Phone = "+380671000008", Status = StudentStatus.Active },
            new Student { StudentId = Id(9), FirstName = "Владислав", LastName = "Романюк", Patronymic = "Миколайович", BirthDate = new DateOnly(2003, 7, 14), Email = "vladyslav.romaniuk@campus.ua", Phone = "+380671000009", Status = StudentStatus.Active },
            new Student { StudentId = Id(10), FirstName = "Софія", LastName = "Козак", Patronymic = "Петрівна", BirthDate = new DateOnly(2005, 3, 8), Email = "sofiia.kozak@campus.ua", Phone = "+380671000010", Status = StudentStatus.Active },
            new Student { StudentId = Id(11), FirstName = "Артем", LastName = "Литвин", Patronymic = "Володимирович", BirthDate = new DateOnly(2005, 9, 21), Email = "artem.lytvyn@campus.ua", Phone = "+380671000011", Status = StudentStatus.Active },
            new Student { StudentId = Id(12), FirstName = "Катерина", LastName = "Павленко", Patronymic = "Юріївна", BirthDate = new DateOnly(2005, 5, 12), Email = "kateryna.pavlenko@campus.ua", Phone = "+380671000012", Status = StudentStatus.Active },
            new Student { StudentId = Id(13), FirstName = "Юрій", LastName = "Мороз", Patronymic = "Степанович", BirthDate = new DateOnly(2002, 2, 17), Email = "yurii.moroz@campus.ua", Phone = "+380671000013", Status = StudentStatus.Graduated },
            new Student { StudentId = Id(14), FirstName = "Анастасія", LastName = "Іванчук", Patronymic = "Павлівна", BirthDate = new DateOnly(2005, 1, 30), Email = "anastasiia.ivanchuk@campus.ua", Phone = "+380671000014", Status = StudentStatus.Active },
            new Student { StudentId = Id(15), FirstName = "Денис", LastName = "Олійник", Patronymic = "Олексійович", BirthDate = new DateOnly(2004, 10, 6), Email = "denys.oliinyk@campus.ua", Phone = "+380671000015", Status = StudentStatus.Expelled },
            new Student { StudentId = Id(16), FirstName = "Вероніка", LastName = "Гнатюк", Patronymic = "Іванівна", BirthDate = new DateOnly(2005, 7, 19), Email = "veronika.hnatiuk@campus.ua", Phone = "+380671000016", Status = StudentStatus.Active },
            new Student { StudentId = Id(17), FirstName = "Тарас", LastName = "Бондар", Patronymic = "Михайлович", BirthDate = new DateOnly(2004, 4, 2), Email = "taras.bondar@campus.ua", Phone = "+380671000017", Status = StudentStatus.Active },
            new Student { StudentId = Id(18), FirstName = "Христина", LastName = "Федорук", Patronymic = "Богданівна", BirthDate = new DateOnly(2005, 8, 9), Email = "khrystyna.fedoruk@campus.ua", Phone = "+380671000018", Status = StudentStatus.OnLeave },
            new Student { StudentId = Id(19), FirstName = "Роман", LastName = "Сорока", Patronymic = "Дмитрович", BirthDate = new DateOnly(2004, 3, 25), Email = "roman.soroka@campus.ua", Phone = "+380671000019", Status = StudentStatus.Active },
            new Student { StudentId = Id(20), FirstName = "Дарина", LastName = "Мазур", Patronymic = "Сергіївна", BirthDate = new DateOnly(2005, 11, 13), Email = "daryna.mazur@campus.ua", Phone = "+380671000020", Status = StudentStatus.Active },
            new Student { StudentId = Id(21), FirstName = "Павло", LastName = "Дяченко", Patronymic = "Віталійович", BirthDate = new DateOnly(2006, 2, 16), Email = "pavlo.diachenko@campus.ua", Phone = "+380671000021", Status = StudentStatus.Active },
            new Student { StudentId = Id(22), FirstName = "Юлія", LastName = "Власенко", Patronymic = "Анатоліївна", BirthDate = new DateOnly(2006, 4, 1), Email = "yuliia.vlasenko@campus.ua", Phone = "+380671000022", Status = StudentStatus.Active },
            new Student { StudentId = Id(23), FirstName = "Ілля", LastName = "Ковтун", Patronymic = "Романович", BirthDate = new DateOnly(2006, 6, 6), Email = "illia.kovtun@campus.ua", Phone = "+380671000023", Status = StudentStatus.Active },
            new Student { StudentId = Id(24), FirstName = "Оксана", LastName = "Чумак", Patronymic = "Миколаївна", BirthDate = new DateOnly(2005, 9, 28), Email = "oksana.chumak@campus.ua", Phone = "+380671000024", Status = StudentStatus.Active }
        };

        var enrollments = new[]
        {
            new StudentGroupEnrollment { EnrollmentId = Id(1), StudentId = Id(1), GroupId = Id(1), DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2022, 8, 31), ReasonStart = "Вступ", ReasonEnd = "Переведення" },
            new StudentGroupEnrollment { EnrollmentId = Id(2), StudentId = Id(1), GroupId = Id(2), DateFrom = new DateOnly(2022, 9, 1), DateTo = null, ReasonStart = "Переведення", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(3), StudentId = Id(2), GroupId = Id(1), DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2025, 6, 30), ReasonStart = "Вступ", ReasonEnd = "Випуск" },
            new StudentGroupEnrollment { EnrollmentId = Id(4), StudentId = Id(3), GroupId = Id(2), DateFrom = new DateOnly(2022, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(5), StudentId = Id(4), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(6), StudentId = Id(5), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = new DateOnly(2024, 1, 31), ReasonStart = "Вступ", ReasonEnd = "Академвідпустка" },
            new StudentGroupEnrollment { EnrollmentId = Id(7), StudentId = Id(6), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(8), StudentId = Id(7), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(9), StudentId = Id(8), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(10), StudentId = Id(9), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(11), StudentId = Id(10), GroupId = Id(4), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(12), StudentId = Id(11), GroupId = Id(4), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(13), StudentId = Id(12), GroupId = Id(5), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(14), StudentId = Id(13), GroupId = Id(1), DateFrom = new DateOnly(2021, 9, 1), DateTo = new DateOnly(2025, 6, 30), ReasonStart = "Вступ", ReasonEnd = "Випуск" },
            new StudentGroupEnrollment { EnrollmentId = Id(15), StudentId = Id(14), GroupId = Id(5), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(16), StudentId = Id(15), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = new DateOnly(2025, 2, 14), ReasonStart = "Вступ", ReasonEnd = "Відрахування" },
            new StudentGroupEnrollment { EnrollmentId = Id(17), StudentId = Id(16), GroupId = Id(4), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(18), StudentId = Id(17), GroupId = Id(2), DateFrom = new DateOnly(2022, 9, 1), DateTo = new DateOnly(2024, 8, 31), ReasonStart = "Вступ", ReasonEnd = "Переведення" },
            new StudentGroupEnrollment { EnrollmentId = Id(19), StudentId = Id(17), GroupId = Id(3), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Переведення", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(20), StudentId = Id(18), GroupId = Id(5), DateFrom = new DateOnly(2024, 9, 1), DateTo = new DateOnly(2025, 2, 1), ReasonStart = "Вступ", ReasonEnd = "Академвідпустка" },
            new StudentGroupEnrollment { EnrollmentId = Id(21), StudentId = Id(19), GroupId = Id(3), DateFrom = new DateOnly(2023, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(22), StudentId = Id(20), GroupId = Id(4), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(23), StudentId = Id(21), GroupId = Id(6), DateFrom = new DateOnly(2025, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(24), StudentId = Id(22), GroupId = Id(6), DateFrom = new DateOnly(2025, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(25), StudentId = Id(23), GroupId = Id(6), DateFrom = new DateOnly(2025, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null },
            new StudentGroupEnrollment { EnrollmentId = Id(26), StudentId = Id(24), GroupId = Id(4), DateFrom = new DateOnly(2024, 9, 1), DateTo = null, ReasonStart = "Вступ", ReasonEnd = null }
        };

        var subgroupAssignments = new[]
        {
            new StudentSubgroupAssignment { EnrollmentId = Id(1), SubgroupId = Id(1) },
            new StudentSubgroupAssignment { EnrollmentId = Id(2), SubgroupId = Id(3) },
            new StudentSubgroupAssignment { EnrollmentId = Id(3), SubgroupId = Id(2) },
            new StudentSubgroupAssignment { EnrollmentId = Id(4), SubgroupId = Id(4) },
            new StudentSubgroupAssignment { EnrollmentId = Id(5), SubgroupId = Id(5) },
            new StudentSubgroupAssignment { EnrollmentId = Id(7), SubgroupId = Id(6) },
            new StudentSubgroupAssignment { EnrollmentId = Id(8), SubgroupId = Id(5) },
            new StudentSubgroupAssignment { EnrollmentId = Id(9), SubgroupId = Id(6) },
            new StudentSubgroupAssignment { EnrollmentId = Id(10), SubgroupId = Id(5) },
            new StudentSubgroupAssignment { EnrollmentId = Id(11), SubgroupId = Id(7) },
            new StudentSubgroupAssignment { EnrollmentId = Id(12), SubgroupId = Id(8) },
            new StudentSubgroupAssignment { EnrollmentId = Id(13), SubgroupId = Id(9) },
            new StudentSubgroupAssignment { EnrollmentId = Id(14), SubgroupId = Id(1) },
            new StudentSubgroupAssignment { EnrollmentId = Id(15), SubgroupId = Id(10) },
            new StudentSubgroupAssignment { EnrollmentId = Id(17), SubgroupId = Id(8) },
            new StudentSubgroupAssignment { EnrollmentId = Id(18), SubgroupId = Id(3) },
            new StudentSubgroupAssignment { EnrollmentId = Id(19), SubgroupId = Id(6) },
            new StudentSubgroupAssignment { EnrollmentId = Id(20), SubgroupId = Id(9) },
            new StudentSubgroupAssignment { EnrollmentId = Id(21), SubgroupId = Id(6) },
            new StudentSubgroupAssignment { EnrollmentId = Id(22), SubgroupId = Id(7) },
            new StudentSubgroupAssignment { EnrollmentId = Id(23), SubgroupId = Id(11) },
            new StudentSubgroupAssignment { EnrollmentId = Id(24), SubgroupId = Id(12) },
            new StudentSubgroupAssignment { EnrollmentId = Id(25), SubgroupId = Id(11) },
            new StudentSubgroupAssignment { EnrollmentId = Id(26), SubgroupId = Id(8) }
        };

        var academicLeaves = new[]
        {
            new AcademicLeave { LeaveId = Id(1), EnrollmentId = Id(6), StartDate = new DateOnly(2024, 2, 1), EndDate = null, Reason = "Стан здоров'я" },
            new AcademicLeave { LeaveId = Id(2), EnrollmentId = Id(20), StartDate = new DateOnly(2025, 2, 2), EndDate = null, Reason = "Сімейні обставини" },
            new AcademicLeave { LeaveId = Id(3), EnrollmentId = Id(9), StartDate = new DateOnly(2024, 11, 4), EndDate = new DateOnly(2025, 1, 20), Reason = "Програма академічної мобільності" }
        };

        var externalTransfers = new[]
        {
            new ExternalTransfer { TransferId = Id(1), StudentId = Id(4), InstitutionId = Id(4), TransferType = TransferType.In, TransferDate = new DateOnly(2023, 8, 25), Notes = "Переведення після другого семестру" },
            new ExternalTransfer { TransferId = Id(2), StudentId = Id(11), InstitutionId = Id(3), TransferType = TransferType.In, TransferDate = new DateOnly(2024, 8, 28), Notes = "Продовження навчання після переїзду" },
            new ExternalTransfer { TransferId = Id(3), StudentId = Id(13), InstitutionId = Id(2), TransferType = TransferType.Out, TransferDate = new DateOnly(2025, 7, 2), Notes = "Подальше навчання в магістратурі" },
            new ExternalTransfer { TransferId = Id(4), StudentId = Id(24), InstitutionId = Id(6), TransferType = TransferType.In, TransferDate = new DateOnly(2024, 8, 26), Notes = "Переведення з іншого закладу" }
        };

        var groupPlanAssignments = new[]
        {
            new GroupPlanAssignment { GroupPlanAssignmentId = Id(1), GroupId = Id(1), PlanId = Id(1), DateFrom = new DateOnly(2021, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = Id(2), GroupId = Id(2), PlanId = Id(1), DateFrom = new DateOnly(2022, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = Id(3), GroupId = Id(3), PlanId = Id(2), DateFrom = new DateOnly(2023, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = Id(4), GroupId = Id(4), PlanId = Id(3), DateFrom = new DateOnly(2024, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = Id(5), GroupId = Id(5), PlanId = Id(2), DateFrom = new DateOnly(2024, 9, 1), DateTo = null },
            new GroupPlanAssignment { GroupPlanAssignmentId = Id(6), GroupId = Id(6), PlanId = Id(3), DateFrom = new DateOnly(2025, 9, 1), DateTo = null },
        };

        var courseEnrollments = new List<StudentCourseEnrollment>();
        var gradeRecords = new List<GradeRecord>();
        int nextCourseEnrollmentId = 1;
        int nextGradeId = 1;

        DateOnly BuildAssessmentDate(int academicYearStart, Guid disciplineId)
        {
            return new DateOnly(academicYearStart + 1, (Math.Abs(disciplineId.GetHashCode()) % 12) + 1, Math.Min(10 + (Math.Abs(disciplineId.GetHashCode()) % 15), 28));
        }

        void AddCourse(Guid enrollmentId, Guid gpaId, Guid disciplineId, int academicYearStart, CourseStatus status, string? gradeValue = null)
        {
            var ceId = nextCourseEnrollmentId++;
            courseEnrollments.Add(new StudentCourseEnrollment
            {
                CourseEnrollmentId = Id(ceId),
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
                    GradeId = Id(nextGradeId++),
                    CourseEnrollmentId = Id(ceId),
                    GradeValue = gradeValue,
                    AssessmentDate = BuildAssessmentDate(academicYearStart, disciplineId)
                });
            }
        }

        void AddCourses(Guid enrollmentId, Guid gpaId, int academicYearStart,
            params (Guid DisciplineId, CourseStatus Status, string? GradeValue)[] courses)
        {
            foreach (var c in courses)
                AddCourse(enrollmentId, gpaId, c.DisciplineId, academicYearStart, c.Status, c.GradeValue);
        }

        AddCourses(Id(2), Id(2), 2021,
            (Id(1), CourseStatus.Completed, "96"),
            (Id(2), CourseStatus.Completed, "94"),
            (Id(3), CourseStatus.Completed, "90"));
        AddCourses(Id(2), Id(2), 2022,
            (Id(5), CourseStatus.Completed, "91"),
            (Id(4), CourseStatus.Completed, "93"));
        AddCourses(Id(2), Id(2), 2023,
            (Id(6), CourseStatus.Completed, "95"));
        AddCourses(Id(2), Id(2), 2024,
            (Id(7), CourseStatus.InProgress, null),
            (Id(12), CourseStatus.Planned, null));

        AddCourses(Id(3), Id(1), 2021,
            (Id(1), CourseStatus.Completed, "98"),
            (Id(2), CourseStatus.Completed, "95"),
            (Id(3), CourseStatus.Completed, "92"));
        AddCourses(Id(3), Id(1), 2022,
            (Id(5), CourseStatus.Completed, "94"),
            (Id(4), CourseStatus.Completed, "96"));
        AddCourses(Id(3), Id(1), 2023,
            (Id(6), CourseStatus.Completed, "93"),
            (Id(7), CourseStatus.Completed, "91"));
        AddCourses(Id(3), Id(1), 2024,
            (Id(12), CourseStatus.Completed, "90"),
            (Id(8), CourseStatus.Completed, "89"));
        AddCourses(Id(3), Id(1), 2025,
            (Id(10), CourseStatus.Completed, "92"),
            (Id(9), CourseStatus.Completed, "88"));

        AddCourses(Id(4), Id(2), 2022,
            (Id(1), CourseStatus.Completed, "87"),
            (Id(2), CourseStatus.Completed, "89"),
            (Id(3), CourseStatus.Completed, "84"));
        AddCourses(Id(4), Id(2), 2023,
            (Id(5), CourseStatus.Completed, "86"),
            (Id(4), CourseStatus.Completed, "88"));
        AddCourses(Id(4), Id(2), 2024,
            (Id(6), CourseStatus.InProgress, null));

        AddCourses(Id(5), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "90"),
            (Id(2), CourseStatus.Completed, "92"),
            (Id(3), CourseStatus.Completed, "88"));
        AddCourses(Id(5), Id(3), 2024,
            (Id(5), CourseStatus.Completed, "91"),
            (Id(6), CourseStatus.InProgress, null));

        AddCourses(Id(6), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "83"),
            (Id(2), CourseStatus.Completed, "85"),
            (Id(3), CourseStatus.Completed, "81"));
        AddCourses(Id(6), Id(3), 2024,
            (Id(5), CourseStatus.Completed, "84"));

        AddCourses(Id(7), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "88"),
            (Id(2), CourseStatus.Completed, "87"),
            (Id(3), CourseStatus.Completed, "86"));
        AddCourses(Id(7), Id(3), 2024,
            (Id(5), CourseStatus.Completed, "89"),
            (Id(6), CourseStatus.InProgress, null));

        AddCourses(Id(8), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "82"),
            (Id(2), CourseStatus.Completed, "84"),
            (Id(3), CourseStatus.Completed, "80"));
        AddCourses(Id(8), Id(3), 2024,
            (Id(5), CourseStatus.Completed, "83"),
            (Id(6), CourseStatus.Completed, "85"));
        AddCourses(Id(8), Id(3), 2025,
            (Id(7), CourseStatus.InProgress, null));

        AddCourses(Id(9), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "86"),
            (Id(2), CourseStatus.Completed, "88"),
            (Id(3), CourseStatus.Completed, "85"));
        AddCourses(Id(9), Id(3), 2024,
            (Id(5), CourseStatus.Completed, "87"),
            (Id(10), CourseStatus.Retake, null));

        AddCourses(Id(10), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "79"),
            (Id(2), CourseStatus.Completed, "82"),
            (Id(3), CourseStatus.Completed, "78"));
        AddCourses(Id(10), Id(3), 2024,
            (Id(5), CourseStatus.Completed, "81"),
            (Id(6), CourseStatus.InProgress, null));

        AddCourses(Id(11), Id(4), 2024,
            (Id(1), CourseStatus.Completed, "93"),
            (Id(2), CourseStatus.Completed, "95"),
            (Id(5), CourseStatus.Completed, "90"));
        AddCourses(Id(11), Id(4), 2025,
            (Id(11), CourseStatus.InProgress, null));

        AddCourses(Id(12), Id(4), 2024,
            (Id(1), CourseStatus.Completed, "91"),
            (Id(2), CourseStatus.Completed, "92"),
            (Id(5), CourseStatus.Completed, "89"));
        AddCourses(Id(12), Id(4), 2025,
            (Id(11), CourseStatus.InProgress, null));

        AddCourses(Id(13), Id(5), 2024,
            (Id(1), CourseStatus.Completed, "94"),
            (Id(2), CourseStatus.Completed, "90"),
            (Id(5), CourseStatus.Completed, "88"));
        AddCourses(Id(13), Id(5), 2025,
            (Id(11), CourseStatus.InProgress, null));

        AddCourses(Id(14), Id(1), 2021,
            (Id(1), CourseStatus.Completed, "97"),
            (Id(2), CourseStatus.Completed, "96"),
            (Id(3), CourseStatus.Completed, "93"));
        AddCourses(Id(14), Id(1), 2022,
            (Id(5), CourseStatus.Completed, "95"),
            (Id(4), CourseStatus.Completed, "94"));
        AddCourses(Id(14), Id(1), 2023,
            (Id(6), CourseStatus.Completed, "92"),
            (Id(7), CourseStatus.Completed, "91"));
        AddCourses(Id(14), Id(1), 2024,
            (Id(12), CourseStatus.Completed, "90"),
            (Id(8), CourseStatus.Completed, "89"));

        AddCourses(Id(15), Id(5), 2024,
            (Id(1), CourseStatus.Completed, "90"),
            (Id(2), CourseStatus.Completed, "91"),
            (Id(3), CourseStatus.InProgress, null));

        AddCourses(Id(16), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "73"),
            (Id(2), CourseStatus.Completed, "76"),
            (Id(3), CourseStatus.Completed, "71"));
        AddCourses(Id(16), Id(3), 2024,
            (Id(5), CourseStatus.Completed, "74"));

        AddCourses(Id(17), Id(4), 2024,
            (Id(1), CourseStatus.Completed, "89"),
            (Id(2), CourseStatus.Completed, "91"),
            (Id(5), CourseStatus.Completed, "87"));
        AddCourses(Id(17), Id(4), 2025,
            (Id(11), CourseStatus.InProgress, null));

        AddCourses(Id(18), Id(2), 2022,
            (Id(1), CourseStatus.Completed, "88"),
            (Id(2), CourseStatus.Completed, "86"),
            (Id(3), CourseStatus.Completed, "84"));
        AddCourses(Id(18), Id(2), 2023,
            (Id(5), CourseStatus.Completed, "87"),
            (Id(4), CourseStatus.Completed, "85"));
        AddCourses(Id(18), Id(2), 2024,
            (Id(6), CourseStatus.Completed, "89"));
        AddCourses(Id(18), Id(2), 2025,
            (Id(7), CourseStatus.InProgress, null));

        AddCourses(Id(20), Id(5), 2024,
            (Id(1), CourseStatus.Completed, "85"),
            (Id(2), CourseStatus.Completed, "84"));
        AddCourses(Id(20), Id(5), 2025,
            (Id(3), CourseStatus.Planned, null));

        AddCourses(Id(21), Id(3), 2023,
            (Id(1), CourseStatus.Completed, "84"),
            (Id(2), CourseStatus.Completed, "86"));
        AddCourses(Id(21), Id(3), 2024,
            (Id(3), CourseStatus.Completed, "82"),
            (Id(5), CourseStatus.Completed, "85"));
        AddCourses(Id(21), Id(3), 2025,
            (Id(6), CourseStatus.InProgress, null));

        AddCourses(Id(22), Id(4), 2024,
            (Id(1), CourseStatus.Completed, "92"),
            (Id(2), CourseStatus.Completed, "90"),
            (Id(5), CourseStatus.Completed, "88"));

        AddCourses(Id(23), Id(6), 2025,
            (Id(1), CourseStatus.InProgress, null),
            (Id(2), CourseStatus.InProgress, null));

        AddCourses(Id(24), Id(6), 2025,
            (Id(1), CourseStatus.InProgress, null),
            (Id(2), CourseStatus.InProgress, null));

        AddCourses(Id(25), Id(6), 2025,
            (Id(1), CourseStatus.InProgress, null),
            (Id(2), CourseStatus.InProgress, null));

        AddCourses(Id(26), Id(4), 2024,
            (Id(1), CourseStatus.Completed, "90"),
            (Id(2), CourseStatus.Completed, "89"),
            (Id(5), CourseStatus.Completed, "87"));
        AddCourses(Id(26), Id(4), 2025,
            (Id(11), CourseStatus.InProgress, null));

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


