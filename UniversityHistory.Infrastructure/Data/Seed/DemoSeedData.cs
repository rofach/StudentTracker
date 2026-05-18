using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Infrastructure.Data.Seed;

internal static partial class DemoSeedData
{
    private static readonly DateOnly ReferenceDate = new(2026, 4, 21);
    private const string EntryReason = "Вступ";
    private const string TransferReason = "Внутрішнє переведення";

    private sealed record GroupDefinition(int Id, string Code, int DepartmentId, int PlanId, DateOnly CreatedOn);
    private sealed record StudentDefinition(int Id, string FirstName, string LastName, string Patronymic, DateOnly BirthDate, StudentStatus Status = StudentStatus.Active);
    private sealed record EnrollmentDefinition(
        int Id,
        int StudentId,
        int GroupId,
        DateOnly DateFrom,
        DateOnly? DateTo,
        string ReasonStart,
        string? ReasonEnd,
        int? SubgroupNumber,
        bool AutoCourses = true);
    private sealed record PlanCourseDefinition(string Name, decimal Credits, ControlType ControlType);
    private sealed record PlanDefinition(int Id, string SpecialtyCode, string PlanName, DateOnly ValidFrom, IReadOnlyList<PlanCourseDefinition> Courses);

    internal sealed class Snapshot
    {
        public Institution[] Institutions { get; init; } = Array.Empty<Institution>();
        public AcademicUnit[] AcademicUnits { get; init; } = Array.Empty<AcademicUnit>();
        public Department[] Departments { get; init; } = Array.Empty<Department>();
        public Discipline[] Disciplines { get; init; } = Array.Empty<Discipline>();
        public StudyPlan[] StudyPlans { get; init; } = Array.Empty<StudyPlan>();
        public PlanDiscipline[] PlanDisciplines { get; init; } = Array.Empty<PlanDiscipline>();
        public StudyGroup[] StudyGroups { get; init; } = Array.Empty<StudyGroup>();
        public Subgroup[] Subgroups { get; init; } = Array.Empty<Subgroup>();
        public Student[] Students { get; init; } = Array.Empty<Student>();
        public StudentGroupEnrollment[] Enrollments { get; init; } = Array.Empty<StudentGroupEnrollment>();
        public StudentSubgroupEnrollment[] SubgroupEnrollments { get; init; } = Array.Empty<StudentSubgroupEnrollment>();
        public AcademicLeave[] AcademicLeaves { get; init; } = Array.Empty<AcademicLeave>();
        public ExternalTransfer[] ExternalTransfers { get; init; } = Array.Empty<ExternalTransfer>();
        public GroupPlanAssignment[] GroupPlanAssignments { get; init; } = Array.Empty<GroupPlanAssignment>();
        public StudentCourseEnrollment[] CourseEnrollments { get; init; } = Array.Empty<StudentCourseEnrollment>();
        public GradeRecord[] GradeRecords { get; init; } = Array.Empty<GradeRecord>();
        public StudentGroupTransfer[] GroupTransfers { get; init; } = Array.Empty<StudentGroupTransfer>();
        public AcademicDifferenceItem[] AcademicDifferenceItems { get; init; } = Array.Empty<AcademicDifferenceItem>();
    }

    public static Snapshot Build()
    {
        var institutions = BuildInstitutions();
        var academicUnits = BuildAcademicUnits();
        var departments = BuildDepartments();
        var plans = BuildPlans();
        var studyPlans = plans
            .Select(plan => new StudyPlan
            {
                PlanId = Id(plan.Id),
                SpecialtyCode = plan.SpecialtyCode,
                PlanName = plan.PlanName,
                ValidFrom = plan.ValidFrom
            })
            .ToArray();

        var disciplines = BuildDisciplines(plans);
        var disciplineIdByName = disciplines.ToDictionary(item => item.DisciplineName, item => item.DisciplineId);
        var disciplineNameById = disciplines.ToDictionary(item => item.DisciplineId, item => item.DisciplineName);
        var planDisciplines = BuildPlanDisciplines(plans, disciplineIdByName);
        var planDisciplineIdByPlanAndName = planDisciplines.ToDictionary(
            item => (item.PlanId, disciplineNameById[item.DisciplineId]),
            item => item.PlanDisciplineId);

        var groups = BuildGroups();
        var studyGroups = groups
            .Select(group => new StudyGroup
            {
                GroupId = Id(group.Id),
                GroupCode = group.Code,
                DepartmentId = Id(group.DepartmentId),
                DateCreated = group.CreatedOn
            })
            .ToArray();

        var subgroups = BuildSubgroups(groups);
        var students = BuildStudents();
        var enrollments = BuildEnrollments()
            .Select(item => new StudentGroupEnrollment
            {
                EnrollmentId = Id(item.Id),
                StudentId = Id(item.StudentId),
                GroupId = Id(item.GroupId),
                DateFrom = item.DateFrom,
                DateTo = item.DateTo,
                ReasonStart = item.ReasonStart,
                ReasonEnd = item.ReasonEnd
            })
            .ToArray();

        var subgroupEnrollments = BuildSubgroupEnrollments(BuildEnrollments(), subgroups);
        var academicLeaves = BuildAcademicLeaves();
        var externalTransfers = BuildExternalTransfers();
        var groupPlanAssignments = groups
            .Select(group => new GroupPlanAssignment
            {
                GroupPlanAssignmentId = Id(group.Id),
                GroupId = Id(group.Id),
                PlanId = Id(group.PlanId),
                DateFrom = group.CreatedOn
            })
            .ToArray();

        var (automaticCourses, automaticGrades) = BuildAutomaticCourseData(BuildEnrollments(), groupPlanAssignments, planDisciplines);
        var (manualCourses, manualGrades) = BuildManualTransferCourses(planDisciplineIdByPlanAndName, groupPlanAssignments);
        var courseEnrollments = automaticCourses.Concat(manualCourses).ToArray();
        var gradeRecords = automaticGrades.Concat(manualGrades).ToArray();

        var groupTransfers = BuildGroupTransfers();
        var academicDifferenceItems = BuildAcademicDifferenceItems(planDisciplineIdByPlanAndName);

        return new Snapshot
        {
            Institutions = institutions,
            AcademicUnits = academicUnits,
            Departments = departments,
            Disciplines = disciplines,
            StudyPlans = studyPlans,
            PlanDisciplines = planDisciplines,
            StudyGroups = studyGroups,
            Subgroups = subgroups,
            Students = students,
            Enrollments = enrollments,
            SubgroupEnrollments = subgroupEnrollments,
            AcademicLeaves = academicLeaves,
            ExternalTransfers = externalTransfers,
            GroupPlanAssignments = groupPlanAssignments,
            CourseEnrollments = courseEnrollments,
            GradeRecords = gradeRecords,
            GroupTransfers = groupTransfers,
            AcademicDifferenceItems = academicDifferenceItems
        };
    }

    private static Institution[] BuildInstitutions() =>
    [
        new Institution { InstitutionId = Id(1), InstitutionName = "Національний технічний університет України «КПІ імені Ігоря Сікорського»" },
        new Institution { InstitutionId = Id(2), InstitutionName = "Львівська політехніка" },
        new Institution { InstitutionId = Id(3), InstitutionName = "Харківський національний університет радіоелектроніки" }
    ];

    private static AcademicUnit[] BuildAcademicUnits() =>
    [
        new AcademicUnit
        {
            AcademicUnitId = Id(1),
            Name = "Факультет комп'ютерних наук та інженерії",
            Type = AcademicUnitType.Faculty
        },
        new AcademicUnit
        {
            AcademicUnitId = Id(2),
            Name = "Інститут прикладної математики та кібернетики",
            Type = AcademicUnitType.Institute
        },
        new AcademicUnit
        {
            AcademicUnitId = Id(3),
            Name = "Навчально-науковий інститут економіки та менеджменту",
            Type = AcademicUnitType.Institute
        },
        new AcademicUnit
        {
            AcademicUnitId = Id(4),
            Name = "Факультет лінгвістики та права",
            Type = AcademicUnitType.Faculty
        }
    ];

    private static Department[] BuildDepartments() =>
    [
        new Department { DepartmentId = Id(1), AcademicUnitId = Id(1), Name = "Кафедра програмної інженерії" },
        new Department { DepartmentId = Id(2), AcademicUnitId = Id(1), Name = "Кафедра комп'ютерних наук" },
        new Department { DepartmentId = Id(3), AcademicUnitId = Id(1), Name = "Кафедра комп'ютерної інженерії" },
        new Department { DepartmentId = Id(4), AcademicUnitId = Id(1), Name = "Кафедра комп'ютерних технологій" },
        new Department { DepartmentId = Id(5), AcademicUnitId = Id(2), Name = "Кафедра кібернетики та штучного інтелекту" },
        new Department { DepartmentId = Id(6), AcademicUnitId = Id(3), Name = "Кафедра міжнародної економіки" },
        new Department { DepartmentId = Id(7), AcademicUnitId = Id(3), Name = "Кафедра менеджменту та маркетингу" },
        new Department { DepartmentId = Id(8), AcademicUnitId = Id(4), Name = "Кафедра теорії та практики перекладу" },
        new Department { DepartmentId = Id(9), AcademicUnitId = Id(4), Name = "Кафедра інформаційного права" }
    ];

    private static PlanDefinition[] BuildPlans() =>
    [
        new PlanDefinition(1, "121", "Програмна інженерія 2025", new DateOnly(2025, 9, 1), BuildSoftwareEngineeringCourses()),
        new PlanDefinition(2, "122", "Комп'ютерні науки 2025", new DateOnly(2025, 9, 1), BuildComputerScienceCourses()),
        new PlanDefinition(3, "123", "Комп'ютерна інженерія 2025", new DateOnly(2025, 9, 1), BuildComputerEngineeringCourses()),
        new PlanDefinition(4, "122", "Кібернетика та ШІ 2025", new DateOnly(2025, 9, 1), BuildCyberneticsCourses()),
        new PlanDefinition(5, "051", "Міжнародна економіка 2025", new DateOnly(2025, 9, 1), BuildEconomicsCourses()),
        new PlanDefinition(6, "035", "Прикладна лінгвістика 2025", new DateOnly(2025, 9, 1), BuildLinguisticsCourses())
    ];

    private static GroupDefinition[] BuildGroups() =>
    [
        new GroupDefinition(1, "КС-24", 1, 1, new DateOnly(2024, 9, 1)),
        new GroupDefinition(2, "КС-25", 1, 1, new DateOnly(2025, 9, 1)),
        new GroupDefinition(3, "КН-24", 2, 2, new DateOnly(2024, 9, 1)),
        new GroupDefinition(4, "КН-25", 2, 2, new DateOnly(2025, 9, 1)),
        new GroupDefinition(5, "КТ-25", 4, 3, new DateOnly(2025, 9, 1)),
        new GroupDefinition(6, "КІ-25", 3, 3, new DateOnly(2025, 9, 1)),
        new GroupDefinition(7, "ШІ-25", 5, 4, new DateOnly(2025, 9, 1)),
        new GroupDefinition(8, "МЕ-25", 6, 5, new DateOnly(2025, 9, 1)),
        new GroupDefinition(9, "ПЛ-25", 8, 6, new DateOnly(2025, 9, 1))
    ];

    private static Subgroup[] BuildSubgroups(IEnumerable<GroupDefinition> groups) =>
        groups
            .SelectMany(group => new[]
            {
                new Subgroup { SubgroupId = Id(group.Id * 10 + 1), GroupId = Id(group.Id), SubgroupName = "Підгрупа 1" },
                new Subgroup { SubgroupId = Id(group.Id * 10 + 2), GroupId = Id(group.Id), SubgroupName = "Підгрупа 2" }
            })
            .ToArray();

    private static Student[] BuildStudents()
    {
        var definitions = new[]
        {
            new StudentDefinition(1, "Артем", "Коваль", "Сергійович", new DateOnly(2005, 1, 14)),
            new StudentDefinition(2, "Марія", "Ткачук", "Олегівна", new DateOnly(2005, 3, 22)),
            new StudentDefinition(3, "Владислав", "Мельник", "Ігорович", new DateOnly(2005, 5, 8)),
            new StudentDefinition(4, "Софія", "Бондар", "Андріївна", new DateOnly(2005, 7, 3)),
            new StudentDefinition(5, "Дмитро", "Савчук", "Віталійович", new DateOnly(2005, 10, 27)),
            new StudentDefinition(6, "Анастасія", "Романюк", "Петрівна", new DateOnly(2005, 12, 11)),
            new StudentDefinition(7, "Богдан", "Кравчук", "Юрійович", new DateOnly(2006, 2, 4)),
            new StudentDefinition(8, "Катерина", "Поліщук", "Олександрівна", new DateOnly(2006, 4, 12)),
            new StudentDefinition(9, "Роман", "Шевчук", "Миколайович", new DateOnly(2006, 6, 9)),
            new StudentDefinition(10, "Дарина", "Лисенко", "Іванівна", new DateOnly(2006, 8, 28)),
            new StudentDefinition(11, "Максим", "Гончаренко", "Сергійович", new DateOnly(2006, 11, 5)),
            new StudentDefinition(12, "Вікторія", "Олійник", "Василівна", new DateOnly(2005, 1, 17)),
            new StudentDefinition(13, "Назар", "Бойко", "Степанович", new DateOnly(2005, 2, 26)),
            new StudentDefinition(14, "Ірина", "Павлюк", "Андріївна", new DateOnly(2005, 4, 19), StudentStatus.OnLeave),
            new StudentDefinition(15, "Денис", "Федорук", "Олегович", new DateOnly(2005, 6, 30)),
            new StudentDefinition(16, "Ольга", "Сорока", "Михайлівна", new DateOnly(2005, 9, 2)),
            new StudentDefinition(17, "Ярослав", "Гуменюк", "Сергійович", new DateOnly(2005, 12, 14)),
            new StudentDefinition(18, "Аліна", "Кузьменко", "Володимирівна", new DateOnly(2006, 1, 23)),
            new StudentDefinition(19, "Андрій", "Дяченко", "Юрійович", new DateOnly(2006, 3, 5)),
            new StudentDefinition(20, "Вероніка", "Левченко", "Ігорівна", new DateOnly(2006, 5, 16)),
            new StudentDefinition(21, "Павло", "Мороз", "Романович", new DateOnly(2006, 7, 29)),
            new StudentDefinition(22, "Христина", "Мазур", "Богданівна", new DateOnly(2006, 10, 7)),
            new StudentDefinition(23, "Ілля", "Кушнір", "Сергійович", new DateOnly(2006, 1, 20)),
            new StudentDefinition(24, "Юлія", "Власюк", "Андріївна", new DateOnly(2006, 3, 31)),
            new StudentDefinition(25, "Тарас", "Олійник", "Миколайович", new DateOnly(2006, 6, 21)),
            new StudentDefinition(26, "Діана", "Руденко", "Олексіївна", new DateOnly(2006, 8, 10)),
            new StudentDefinition(27, "Олександр", "Гнатюк", "Віталійович", new DateOnly(2006, 11, 18)),
            new StudentDefinition(28, "Марта", "Черненко", "Ігорівна", new DateOnly(2006, 2, 13)),
            new StudentDefinition(29, "Кирило", "Ковтун", "Олександрович", new DateOnly(2006, 4, 24)),
            new StudentDefinition(30, "Поліна", "Нагорна", "Сергіївна", new DateOnly(2006, 7, 7)),
            new StudentDefinition(31, "Арсен", "Паламарчук", "Юрійович", new DateOnly(2006, 9, 19)),
            new StudentDefinition(32, "Соломія", "Петренко", "Василівна", new DateOnly(2006, 12, 2)),
            // Students for ШІ-25 (Group 7)
            new StudentDefinition(33, "Олег", "Сидоренко", "Іванович", new DateOnly(2006, 1, 15)),
            new StudentDefinition(34, "Марина", "Коваленко", "Олександрівна", new DateOnly(2006, 3, 20)),
            new StudentDefinition(35, "Іван", "Мороз", "Віталійович", new DateOnly(2006, 5, 25)),
            // Students for МЕ-25 (Group 8)
            new StudentDefinition(36, "Наталія", "Кравченко", "Сергіївна", new DateOnly(2006, 7, 10)),
            new StudentDefinition(37, "Антон", "Вовк", "Миколайович", new DateOnly(2006, 9, 5)),
            new StudentDefinition(38, "Дарія", "Григоренко", "Андріївна", new DateOnly(2006, 11, 12)),
            // Students for ПЛ-25 (Group 9)
            new StudentDefinition(39, "Євген", "Ткаченко", "Володимирович", new DateOnly(2006, 2, 8)),
            new StudentDefinition(40, "Валерія", "Онищенко", "Ігорівна", new DateOnly(2006, 4, 18)),
            new StudentDefinition(41, "Станіслав", "Лисенко", "Петрович", new DateOnly(2006, 6, 22))
        };

        return definitions
            .Select(item => new Student
            {
                StudentId = Id(item.Id),
                FirstName = item.FirstName,
                LastName = item.LastName,
                Patronymic = item.Patronymic,
                BirthDate = item.BirthDate,
                Email = $"student{item.Id:D2}@campus.ua",
                Phone = $"+38067010{item.Id:D3}",
                Status = item.Status
            })
            .ToArray();
    }

    private static EnrollmentDefinition[] BuildEnrollments() =>
    [
        new EnrollmentDefinition(1, 1, 1, new DateOnly(2024, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(2, 2, 1, new DateOnly(2024, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(3, 3, 1, new DateOnly(2024, 9, 1), new DateOnly(2026, 2, 10), EntryReason, TransferReason, 1),
        new EnrollmentDefinition(4, 4, 1, new DateOnly(2024, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(5, 5, 1, new DateOnly(2024, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(6, 6, 1, new DateOnly(2024, 9, 1), null, EntryReason, null, null),

        new EnrollmentDefinition(7, 7, 2, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(8, 8, 2, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(9, 9, 2, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(10, 10, 2, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(11, 11, 2, new DateOnly(2025, 9, 1), null, EntryReason, null, null),

        new EnrollmentDefinition(12, 12, 3, new DateOnly(2024, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(13, 13, 3, new DateOnly(2024, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(14, 14, 3, new DateOnly(2024, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(15, 15, 3, new DateOnly(2024, 9, 1), new DateOnly(2026, 2, 1), EntryReason, TransferReason, 2),
        new EnrollmentDefinition(16, 16, 3, new DateOnly(2024, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(17, 17, 3, new DateOnly(2024, 9, 1), null, EntryReason, null, null),

        new EnrollmentDefinition(18, 18, 4, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(19, 19, 4, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(20, 20, 4, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(21, 21, 4, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(22, 22, 4, new DateOnly(2025, 9, 1), null, EntryReason, null, null),

        new EnrollmentDefinition(23, 23, 5, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(24, 24, 5, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(25, 25, 5, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(26, 26, 5, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(27, 27, 5, new DateOnly(2025, 9, 1), null, EntryReason, null, null),

        new EnrollmentDefinition(28, 28, 6, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(29, 29, 6, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(30, 30, 6, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(31, 31, 6, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(32, 32, 6, new DateOnly(2025, 9, 1), null, EntryReason, null, null),

        // Group 7 (ШІ-25)
        new EnrollmentDefinition(33, 33, 7, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(34, 34, 7, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(35, 35, 7, new DateOnly(2025, 9, 1), null, EntryReason, null, null),

        // Group 8 (МЕ-25)
        new EnrollmentDefinition(36, 36, 8, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(37, 37, 8, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(38, 38, 8, new DateOnly(2025, 9, 1), null, EntryReason, null, null),

        // Group 9 (ПЛ-25)
        new EnrollmentDefinition(39, 39, 9, new DateOnly(2025, 9, 1), null, EntryReason, null, 1),
        new EnrollmentDefinition(40, 40, 9, new DateOnly(2025, 9, 1), null, EntryReason, null, 2),
        new EnrollmentDefinition(41, 41, 9, new DateOnly(2025, 9, 1), null, EntryReason, null, null),

        // Transfers
        new EnrollmentDefinition(101, 3, 4, new DateOnly(2026, 2, 11), null, TransferReason, null, null, false),
        new EnrollmentDefinition(102, 15, 6, new DateOnly(2026, 2, 2), null, TransferReason, null, null, false)
    ];

    private static StudentSubgroupEnrollment[] BuildSubgroupEnrollments(IEnumerable<EnrollmentDefinition> enrollments, IEnumerable<Subgroup> subgroups)
    {
        var subgroupIdByGroupAndNumber = subgroups.ToDictionary(
            item => (item.GroupId, item.SubgroupName.EndsWith("2", StringComparison.Ordinal) ? 2 : 1),
            item => item.SubgroupId);

        var nextId = 1001;
        var result = new List<StudentSubgroupEnrollment>();

        foreach (var enrollment in enrollments.Where(item => item.SubgroupNumber is not null))
        {
            result.Add(new StudentSubgroupEnrollment
            {
                SubgroupEnrollmentId = Id(nextId++),
                EnrollmentId = Id(enrollment.Id),
                SubgroupId = subgroupIdByGroupAndNumber[(Id(enrollment.GroupId), enrollment.SubgroupNumber!.Value)],
                DateFrom = enrollment.DateFrom,
                DateTo = enrollment.DateTo,
                Reason = enrollment.ReasonStart
            });
        }

        return result.ToArray();
    }

    private static AcademicLeave[] BuildAcademicLeaves() =>
    [
        new AcademicLeave
        {
            LeaveId = Id(1),
            EnrollmentId = Id(14),
            StartDate = new DateOnly(2026, 2, 3),
            Reason = "Стан здоров'я"
        },
        new AcademicLeave
        {
            LeaveId = Id(2),
            EnrollmentId = Id(9),
            StartDate = new DateOnly(2026, 1, 20),
            EndDate = new DateOnly(2026, 3, 10),
            Reason = "Сімейні обставини",
            ReturnReason = "Поновлено до навчання"
        }
    ];

    private static ExternalTransfer[] BuildExternalTransfers() =>
    [
        new ExternalTransfer
        {
            TransferId = Id(1),
            StudentId = Id(28),
            InstitutionId = Id(3),
            TransferType = TransferType.In,
            TransferDate = new DateOnly(2025, 8, 25),
            Notes = "Переведення після завершення першого курсу в іншому закладі."
        },
        new ExternalTransfer
        {
            TransferId = Id(2),
            StudentId = Id(12),
            InstitutionId = Id(2),
            TransferType = TransferType.In,
            TransferDate = new DateOnly(2024, 8, 28),
            Notes = "Поновлення навчання після зміни місця проживання."
        }
    ];

    private static (StudentCourseEnrollment[] Courses, GradeRecord[] Grades) BuildAutomaticCourseData(
        IEnumerable<EnrollmentDefinition> enrollments,
        IEnumerable<GroupPlanAssignment> assignments,
        IEnumerable<PlanDiscipline> planDisciplines)
    {
        var assignmentByGroupId = assignments.ToDictionary(item => item.GroupId, item => item);
        var planDisciplinesByPlanId = planDisciplines
            .GroupBy(item => item.PlanId)
            .ToDictionary(
                group => group.Key,
                group => group
                    .OrderBy(item => item.SemesterNo)
                    .ThenBy(item => item.DisciplineId)
                    .ToArray());

        var courses = new List<StudentCourseEnrollment>();
        var grades = new List<GradeRecord>();
        var nextCourseId = 2001;
        var nextGradeId = 3001;

        foreach (var enrollment in enrollments.Where(item => item.AutoCourses))
        {
            var assignment = assignmentByGroupId[Id(enrollment.GroupId)];
            var disciplines = planDisciplinesByPlanId[assignment.PlanId];
            var effectiveDate = enrollment.DateTo ?? ReferenceDate;
            var activeSemester = GetSemesterIndex(enrollment.DateFrom, effectiveDate);

            foreach (var discipline in disciplines)
            {
                CourseStatus? status = null;

                if (discipline.SemesterNo < activeSemester)
                {
                    status = CourseStatus.Completed;
                }
                else if (discipline.SemesterNo == activeSemester)
                {
                    status = CourseStatus.InProgress;
                }
                else if (enrollment.DateTo is null && discipline.SemesterNo == activeSemester + 1)
                {
                    status = CourseStatus.Planned;
                }

                if (status is null)
                {
                    continue;
                }

                var academicYearStart = enrollment.DateFrom.Year + (discipline.SemesterNo - 1) / 2;
                var courseId = Id(nextCourseId++);

                courses.Add(new StudentCourseEnrollment
                {
                    CourseEnrollmentId = courseId,
                    EnrollmentId = Id(enrollment.Id),
                    GroupPlanAssignmentId = assignment.GroupPlanAssignmentId,
                    PlanDisciplineId = discipline.PlanDisciplineId,
                    AcademicYearStart = academicYearStart,
                    Status = status.Value
                });

                if (status == CourseStatus.Completed)
                {
                    grades.Add(new GradeRecord
                    {
                        GradeId = Id(nextGradeId++),
                        CourseEnrollmentId = courseId,
                        GradeValue = BuildGradeValue(enrollment.StudentId, discipline.SemesterNo, discipline.DisciplineId),
                        AssessmentDate = BuildAssessmentDate(academicYearStart, discipline.SemesterNo)
                    });
                }
            }
        }

        return (courses.ToArray(), grades.ToArray());
    }

    private static (StudentCourseEnrollment[] Courses, GradeRecord[] Grades) BuildManualTransferCourses(
        IReadOnlyDictionary<(Guid PlanId, string DisciplineName), Guid> planDisciplineIdByPlanAndName,
        IEnumerable<GroupPlanAssignment> assignments)
    {
        var assignmentByGroupId = assignments.ToDictionary(item => item.GroupId, item => item);
        var courses = new List<StudentCourseEnrollment>();
        var grades = new List<GradeRecord>();
        var nextCourseId = 4001;
        var nextGradeId = 5001;

        AddManualCourse(101, 4, "Англійська мова (фахового спрямування)", CourseStatus.InProgress, 2025);
        AddManualCourse(101, 4, "Вступ до спеціальності", CourseStatus.InProgress, 2025);
        AddManualCourse(101, 4, "Теорія інформації та кодування", CourseStatus.Planned, 2026);

        AddManualCourse(102, 6, "Вступ до комп'ютерної інженерії", CourseStatus.InProgress, 2025);
        AddManualCourse(102, 6, "Комп'ютерна електроніка", CourseStatus.Completed, 2025, 90);
        AddManualCourse(102, 6, "Комп'ютерна схемотехніка", CourseStatus.Planned, 2026);

        return (courses.ToArray(), grades.ToArray());

        void AddManualCourse(int enrollmentId, int groupId, string disciplineName, CourseStatus status, int academicYearStart, int? gradeValue = null)
        {
            var assignment = assignmentByGroupId[Id(groupId)];
            var courseId = Id(nextCourseId++);

            courses.Add(new StudentCourseEnrollment
            {
                CourseEnrollmentId = courseId,
                EnrollmentId = Id(enrollmentId),
                GroupPlanAssignmentId = assignment.GroupPlanAssignmentId,
                PlanDisciplineId = planDisciplineIdByPlanAndName[(assignment.PlanId, disciplineName)],
                AcademicYearStart = academicYearStart,
                Status = status
            });

            if (gradeValue.HasValue)
            {
                grades.Add(new GradeRecord
                {
                    GradeId = Id(nextGradeId++),
                    CourseEnrollmentId = courseId,
                    GradeValue = gradeValue.Value,
                    AssessmentDate = new DateOnly(2026, 4, 15)
                });
            }
        }
    }

    private static StudentGroupTransfer[] BuildGroupTransfers() =>
    [
        new StudentGroupTransfer
        {
            TransferId = Id(1),
            OldEnrollmentId = Id(3),
            NewEnrollmentId = Id(101),
            TransferDate = new DateOnly(2026, 2, 11),
            Reason = "Переведення на суміжну освітню програму."
        },
        new StudentGroupTransfer
        {
            TransferId = Id(2),
            OldEnrollmentId = Id(15),
            NewEnrollmentId = Id(102),
            TransferDate = new DateOnly(2026, 2, 2),
            Reason = "Перехід до групи з поглибленою інженерною підготовкою."
        }
    ];

    private static AcademicDifferenceItem[] BuildAcademicDifferenceItems(
        IReadOnlyDictionary<(Guid PlanId, string DisciplineName), Guid> planDisciplineIdByPlanAndName) =>
    [
        new AcademicDifferenceItem
        {
            DifferenceItemId = Id(1),
            TransferId = Id(1),
            PlanDisciplineId = planDisciplineIdByPlanAndName[(Id(2), "Англійська мова (фахового спрямування)")],
            Status = DifferenceItemStatus.Pending,
            Notes = "Потрібно дозакрити дисципліну після внутрішнього переведення."
        },
        new AcademicDifferenceItem
        {
            DifferenceItemId = Id(2),
            TransferId = Id(1),
            PlanDisciplineId = planDisciplineIdByPlanAndName[(Id(2), "Вступ до спеціальності")],
            Status = DifferenceItemStatus.Pending
        },
        new AcademicDifferenceItem
        {
            DifferenceItemId = Id(3),
            TransferId = Id(2),
            PlanDisciplineId = planDisciplineIdByPlanAndName[(Id(3), "Вступ до комп'ютерної інженерії")],
            Status = DifferenceItemStatus.Pending
        },
        new AcademicDifferenceItem
        {
            DifferenceItemId = Id(4),
            TransferId = Id(2),
            PlanDisciplineId = planDisciplineIdByPlanAndName[(Id(3), "Комп'ютерна електроніка")],
            Status = DifferenceItemStatus.Completed,
            Notes = "Зараховано після проходження адаптаційного модуля."
        },
        new AcademicDifferenceItem
        {
            DifferenceItemId = Id(5),
            TransferId = Id(2),
            PlanDisciplineId = planDisciplineIdByPlanAndName[(Id(3), "Комп'ютерна схемотехніка")],
            Status = DifferenceItemStatus.Waived,
            Notes = "Перезараховано за попередні результати навчання."
        }
    ];

    private static Discipline[] BuildDisciplines(IEnumerable<PlanDefinition> plans)
    {
        var names = plans
            .SelectMany(plan => plan.Courses)
            .Select(course => course.Name)
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        return names
            .Select((name, index) => new Discipline
            {
                DisciplineId = Id(index + 1),
                DisciplineName = name,
                Description = BuildDescription(name)
            })
            .ToArray();
    }

    private static PlanDiscipline[] BuildPlanDisciplines(
        IEnumerable<PlanDefinition> plans,
        IReadOnlyDictionary<string, Guid> disciplineIdByName)
    {
        var planDisciplines = new List<PlanDiscipline>();
        var nextId = 1001;

        foreach (var plan in plans)
        {
            var semester = 1;
            decimal semesterCredits = 0;

            foreach (var course in plan.Courses)
            {
                if (semesterCredits > 0 && semesterCredits + course.Credits > 30m)
                {
                    semester++;
                    semesterCredits = 0;
                }

                planDisciplines.Add(new PlanDiscipline
                {
                    PlanDisciplineId = Id(nextId++),
                    PlanId = Id(plan.Id),
                    DisciplineId = disciplineIdByName[course.Name],
                    SemesterNo = semester,
                    ControlType = course.ControlType,
                    Credits = course.Credits
                });

                semesterCredits += course.Credits;
            }
        }

        return planDisciplines.ToArray();
    }

    private static string BuildDescription(string name) => name switch
    {
        "Вища математика" => "Математична база для аналізу, моделювання та розв’язування інженерних задач.",
        "Програмування та алгоритмічні мови" => "Базова дисципліна з алгоритмізації, практики програмування та побудови обчислювальних рішень.",
        "Об'єктно-орієнтоване програмування" => "Проєктування програмних систем із використанням класів, об’єктів, інкапсуляції та наслідування.",
        "Організація баз даних і знань" or "Організація баз даних та знань" => "Проєктування схем даних, робота з SQL та побудова прикладних рішень на основі баз даних.",
        "Комп'ютерні мережі" => "Мережеві моделі, протоколи передавання даних та практичні основи адміністрування мереж.",
        "DevOps-практики" => "Автоматизація збирання, розгортання та супроводу програмних систем у командній розробці.",
        "Якість програмного забезпечення та тестування" => "Підходи до забезпечення якості, верифікації, тестування та аналізу дефектів програмних продуктів.",
        _ => $"Навчальна дисципліна «{name}» із професійної підготовки здобувачів ІТ-спеціальностей."
    };

    private static int BuildGradeValue(int studentId, int semesterNo, Guid disciplineId)
    {
        var value = 72 + Math.Abs(HashCode.Combine(studentId, semesterNo, disciplineId)) % 24;
        return value;
    }

    private static DateOnly BuildAssessmentDate(int academicYearStart, int semesterNo) =>
        semesterNo % 2 == 1
            ? new DateOnly(academicYearStart + 1, 1, 20)
            : new DateOnly(academicYearStart + 1, 6, 20);

    private static int GetSemesterIndex(DateOnly startDate, DateOnly onDate)
    {
        var semester = 1;
        var cursor = startDate;

        while (NextSemesterStart(cursor) <= onDate)
        {
            cursor = NextSemesterStart(cursor);
            semester++;
        }

        return semester;
    }

    private static DateOnly NextSemesterStart(DateOnly currentSemesterStart) =>
        currentSemesterStart.Month >= 8
            ? new DateOnly(currentSemesterStart.Year + 1, 2, 1)
            : new DateOnly(currentSemesterStart.Year, 9, 1);

    private static Guid Id(int value) => new(value, 0, 0, new byte[8]);
}
