using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academic_Unit",
                columns: table => new
                {
                    academic_unit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic_Unit", x => x.academic_unit_id);
                    table.CheckConstraint("chk_academic_unit_type", "type IN ('Faculty','Institute')");
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    discipline_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    institution_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    institution_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.institution_id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'Active'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.student_id);
                    table.CheckConstraint("chk_student_status", "status IN ('Active','OnLeave','Expelled','Graduated')");
                });

            migrationBuilder.CreateTable(
                name: "Study_Plan",
                columns: table => new
                {
                    plan_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    specialty_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    plan_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study_Plan", x => x.plan_id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    department_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    academic_unit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.department_id);
                    table.ForeignKey(
                        name: "FK_Department_Academic_Unit_academic_unit_id",
                        column: x => x.academic_unit_id,
                        principalTable: "Academic_Unit",
                        principalColumn: "academic_unit_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "External_Transfers",
                columns: table => new
                {
                    transfer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    institution_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transfer_type = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    transfer_date = table.Column<DateOnly>(type: "date", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_External_Transfers", x => x.transfer_id);
                    table.CheckConstraint("chk_transfer_type", "transfer_type IN ('In','Out')");
                    table.ForeignKey(
                        name: "FK_External_Transfers_Institution_institution_id",
                        column: x => x.institution_id,
                        principalTable: "Institution",
                        principalColumn: "institution_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_External_Transfers_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "student_id");
                });

            migrationBuilder.CreateTable(
                name: "Plan_Disciplines",
                columns: table => new
                {
                    plan_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    semester_no = table.Column<int>(type: "int", nullable: false),
                    control_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    credits = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan_Disciplines", x => new { x.plan_id, x.discipline_id });
                    table.CheckConstraint("chk_control_type", "control_type IN ('Exam','Credit','Coursework')");
                    table.ForeignKey(
                        name: "FK_Plan_Disciplines_Discipline_discipline_id",
                        column: x => x.discipline_id,
                        principalTable: "Discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plan_Disciplines_Study_Plan_plan_id",
                        column: x => x.plan_id,
                        principalTable: "Study_Plan",
                        principalColumn: "plan_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Study_Group",
                columns: table => new
                {
                    group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    department_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date_created = table.Column<DateOnly>(type: "date", nullable: false),
                    date_closed = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study_Group", x => x.group_id);
                    table.ForeignKey(
                        name: "FK_Study_Group_Department_department_id",
                        column: x => x.department_id,
                        principalTable: "Department",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Group_Plan_Assignment",
                columns: table => new
                {
                    group_plan_assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    plan_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date_from = table.Column<DateOnly>(type: "date", nullable: false),
                    date_to = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group_Plan_Assignment", x => x.group_plan_assignment_id);
                    table.ForeignKey(
                        name: "FK_Group_Plan_Assignment_Study_Group_group_id",
                        column: x => x.group_id,
                        principalTable: "Study_Group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Group_Plan_Assignment_Study_Plan_plan_id",
                        column: x => x.plan_id,
                        principalTable: "Study_Plan",
                        principalColumn: "plan_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student_Group_Enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date_from = table.Column<DateOnly>(type: "date", nullable: false),
                    date_to = table.Column<DateOnly>(type: "date", nullable: true),
                    reason_start = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    reason_end = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Group_Enrollment", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK_Student_Group_Enrollment_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Group_Enrollment_Study_Group_group_id",
                        column: x => x.group_id,
                        principalTable: "Study_Group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subgroup",
                columns: table => new
                {
                    subgroup_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subgroup_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subgroup", x => x.subgroup_id);
                    table.ForeignKey(
                        name: "FK_Subgroup_Study_Group_group_id",
                        column: x => x.group_id,
                        principalTable: "Study_Group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Academic_Leave",
                columns: table => new
                {
                    leave_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    return_reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic_Leave", x => x.leave_id);
                    table.ForeignKey(
                        name: "FK_Academic_Leave_Student_Group_Enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student_Course_Enrollment",
                columns: table => new
                {
                    course_enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_plan_assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    academic_year_start = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'Planned'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Course_Enrollment", x => x.course_enrollment_id);
                    table.CheckConstraint("chk_course_status", "status IN ('Planned','InProgress','Completed','Retake')");
                    table.ForeignKey(
                        name: "FK_Student_Course_Enrollment_Discipline_discipline_id",
                        column: x => x.discipline_id,
                        principalTable: "Discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Course_Enrollment_Group_Plan_Assignment_group_plan_assignment_id",
                        column: x => x.group_plan_assignment_id,
                        principalTable: "Group_Plan_Assignment",
                        principalColumn: "group_plan_assignment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Course_Enrollment_Student_Group_Enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student_Subgroup_Assignment",
                columns: table => new
                {
                    enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subgroup_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Subgroup_Assignment", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK_Student_Subgroup_Assignment_Student_Group_Enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Subgroup_Assignment_Subgroup_subgroup_id",
                        column: x => x.subgroup_id,
                        principalTable: "Subgroup",
                        principalColumn: "subgroup_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grade_Record",
                columns: table => new
                {
                    grade_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    grade_value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    assessment_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade_Record", x => x.grade_id);
                    table.ForeignKey(
                        name: "FK_Grade_Record_Student_Course_Enrollment_course_enrollment_id",
                        column: x => x.course_enrollment_id,
                        principalTable: "Student_Course_Enrollment",
                        principalColumn: "course_enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Academic_Unit",
                columns: new[] { "academic_unit_id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Факультет інформатики та обчислювальної техніки", "Faculty" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Факультет прикладної математики", "Faculty" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Факультет комп'ютерної інженерії", "Faculty" }
                });

            migrationBuilder.InsertData(
                table: "Discipline",
                columns: new[] { "discipline_id", "discipline_name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Вища математика" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Основи програмування" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Дискретна математика" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Алгоритми та структури даних" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "Лінійна алгебра" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), "Об'єктно-орієнтоване програмування" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), "Бази даних" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), "Операційні системи" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), "Комп'ютерні мережі" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), "Вебтехнології" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), "Архітектура комп'ютерів" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), "Теорія ймовірностей" }
                });

            migrationBuilder.InsertData(
                table: "Institution",
                columns: new[] { "institution_id", "city", "country", "institution_name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Київ", "Україна", "Національний технічний університет України \"Київський політехнічний інститут імені Ігоря Сікорського\"" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Львів", "Україна", "Національний університет \"Львівська політехніка\"" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Київ", "Україна", "Київський національний університет імені Тараса Шевченка" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Харків", "Україна", "Харківський національний університет радіоелектроніки" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "Одеса", "Україна", "Національний університет \"Одеська політехніка\"" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), "Чернігів", "Україна", "Національний університет \"Чернігівська політехніка\"" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[] { new Guid("00000001-0000-0000-0000-000000000000"), new DateOnly(2003, 4, 15), "andrii.melnyk@campus.ua", "Андрій", "Мельник", "Олександрович", "+380671000001" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone", "status" },
                values: new object[] { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2002, 9, 3), "olena.koval@campus.ua", "Олена", "Коваль", "Ігорівна", "+380671000002", "Graduated" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2004, 1, 27), "maksym.shevchenko@campus.ua", "Максим", "Шевченко", "Сергійович", "+380671000003" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2004, 6, 11), "iryna.boiko@campus.ua", "Ірина", "Бойко", "Василівна", "+380671000004" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone", "status" },
                values: new object[] { new Guid("00000005-0000-0000-0000-000000000000"), new DateOnly(2004, 12, 1), "bohdan.tkachenko@campus.ua", "Богдан", "Ткаченко", "Романович", "+380671000005", "OnLeave" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[,]
                {
                    { new Guid("00000006-0000-0000-0000-000000000000"), new DateOnly(2004, 2, 18), "mariia.kravets@campus.ua", "Марія", "Кравець", "Андріївна", "+380671000006" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new DateOnly(2004, 8, 5), "dmytro.polishchuk@campus.ua", "Дмитро", "Поліщук", "Олегович", "+380671000007" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new DateOnly(2004, 11, 22), "nataliia.savchuk@campus.ua", "Наталія", "Савчук", "Вікторівна", "+380671000008" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new DateOnly(2003, 7, 14), "vladyslav.romaniuk@campus.ua", "Владислав", "Романюк", "Миколайович", "+380671000009" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new DateOnly(2005, 3, 8), "sofiia.kozak@campus.ua", "Софія", "Козак", "Петрівна", "+380671000010" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new DateOnly(2005, 9, 21), "artem.lytvyn@campus.ua", "Артем", "Литвин", "Володимирович", "+380671000011" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new DateOnly(2005, 5, 12), "kateryna.pavlenko@campus.ua", "Катерина", "Павленко", "Юріївна", "+380671000012" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone", "status" },
                values: new object[] { new Guid("0000000d-0000-0000-0000-000000000000"), new DateOnly(2002, 2, 17), "yurii.moroz@campus.ua", "Юрій", "Мороз", "Степанович", "+380671000013", "Graduated" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[] { new Guid("0000000e-0000-0000-0000-000000000000"), new DateOnly(2005, 1, 30), "anastasiia.ivanchuk@campus.ua", "Анастасія", "Іванчук", "Павлівна", "+380671000014" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone", "status" },
                values: new object[] { new Guid("0000000f-0000-0000-0000-000000000000"), new DateOnly(2004, 10, 6), "denys.oliinyk@campus.ua", "Денис", "Олійник", "Олексійович", "+380671000015", "Expelled" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[,]
                {
                    { new Guid("00000010-0000-0000-0000-000000000000"), new DateOnly(2005, 7, 19), "veronika.hnatiuk@campus.ua", "Вероніка", "Гнатюк", "Іванівна", "+380671000016" },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new DateOnly(2004, 4, 2), "taras.bondar@campus.ua", "Тарас", "Бондар", "Михайлович", "+380671000017" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone", "status" },
                values: new object[] { new Guid("00000012-0000-0000-0000-000000000000"), new DateOnly(2005, 8, 9), "khrystyna.fedoruk@campus.ua", "Христина", "Федорук", "Богданівна", "+380671000018", "OnLeave" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[,]
                {
                    { new Guid("00000013-0000-0000-0000-000000000000"), new DateOnly(2004, 3, 25), "roman.soroka@campus.ua", "Роман", "Сорока", "Дмитрович", "+380671000019" },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new DateOnly(2005, 11, 13), "daryna.mazur@campus.ua", "Дарина", "Мазур", "Сергіївна", "+380671000020" },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new DateOnly(2006, 2, 16), "pavlo.diachenko@campus.ua", "Павло", "Дяченко", "Віталійович", "+380671000021" },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new DateOnly(2006, 4, 1), "yuliia.vlasenko@campus.ua", "Юлія", "Власенко", "Анатоліївна", "+380671000022" },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new DateOnly(2006, 6, 6), "illia.kovtun@campus.ua", "Ілля", "Ковтун", "Романович", "+380671000023" },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new DateOnly(2005, 9, 28), "oksana.chumak@campus.ua", "Оксана", "Чумак", "Миколаївна", "+380671000024" }
                });

            migrationBuilder.InsertData(
                table: "Study_Plan",
                columns: new[] { "plan_id", "plan_name", "specialty_code", "valid_from" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Комп'ютерні науки 2021", "122", new DateOnly(2021, 9, 1) },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Інженерія програмного забезпечення 2023", "121", new DateOnly(2023, 9, 1) },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Комп'ютерна інженерія 2024", "123", new DateOnly(2024, 9, 1) }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "department_id", "academic_unit_id", "name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Кафедра програмування" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Кафедра комп'ютерних наук" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Кафедра прикладної математики" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Кафедра програмного забезпечення" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Кафедра комп'ютерної інженерії" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Кафедра вбудованих систем" }
                });

            migrationBuilder.InsertData(
                table: "External_Transfers",
                columns: new[] { "transfer_id", "institution_id", "notes", "student_id", "transfer_date", "transfer_type" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Переведення після другого семестру", new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2023, 8, 25), "In" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Продовження навчання після переїзду", new Guid("0000000b-0000-0000-0000-000000000000"), new DateOnly(2024, 8, 28), "In" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Подальше навчання в магістратурі", new Guid("0000000d-0000-0000-0000-000000000000"), new DateOnly(2025, 7, 2), "Out" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "Переведення з іншого закладу", new Guid("00000018-0000-0000-0000-000000000000"), new DateOnly(2024, 8, 26), "In" }
                });

            migrationBuilder.InsertData(
                table: "Plan_Disciplines",
                columns: new[] { "discipline_id", "plan_id", "control_type", "credits", "hours", "semester_no" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 1 },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 6.0m, 180, 1 },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Credit", 4.0m, 120, 1 },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 6.0m, 180, 2 },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 4.0m, 120, 2 },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Coursework", 5.0m, 150, 3 },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 3 },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 4 },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 4.0m, 120, 5 },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Coursework", 5.0m, 150, 5 },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Exam", 4.0m, 120, 4 },
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 1 },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Exam", 6.0m, 180, 1 },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Credit", 4.0m, 120, 1 },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Exam", 4.0m, 120, 2 },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Coursework", 5.0m, 150, 2 },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 3 },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 4 },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Exam", 4.0m, 120, 4 },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Coursework", 5.0m, 150, 3 },
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 1 },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Exam", 6.0m, 180, 1 },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Exam", 6.0m, 180, 2 },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Credit", 4.0m, 120, 1 },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 4 },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 3 },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Exam", 4.0m, 120, 3 },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Exam", 5.0m, 150, 2 }
                });

            migrationBuilder.InsertData(
                table: "Study_Group",
                columns: new[] { "group_id", "date_closed", "date_created", "department_id", "group_code" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, new DateOnly(2021, 9, 1), new Guid("00000001-0000-0000-0000-000000000000"), "КН-21" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, new DateOnly(2022, 9, 1), new Guid("00000002-0000-0000-0000-000000000000"), "КН-22" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), null, new DateOnly(2023, 9, 1), new Guid("00000004-0000-0000-0000-000000000000"), "ПЗ-23" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), null, new DateOnly(2024, 9, 1), new Guid("00000005-0000-0000-0000-000000000000"), "КІ-24" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), null, new DateOnly(2024, 9, 1), new Guid("00000004-0000-0000-0000-000000000000"), "ПЗ-24" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000005-0000-0000-0000-000000000000"), "КІ-25" }
                });

            migrationBuilder.InsertData(
                table: "Group_Plan_Assignment",
                columns: new[] { "group_plan_assignment_id", "date_from", "date_to", "group_id", "plan_id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new DateOnly(2021, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2022, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Group_Enrollment",
                columns: new[] { "enrollment_id", "date_from", "date_to", "group_id", "reason_end", "reason_start", "student_id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new DateOnly(2021, 9, 1), new DateOnly(2022, 8, 31), new Guid("00000001-0000-0000-0000-000000000000"), "Переведення", "Вступ", new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2022, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), null, "Переведення", new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2021, 9, 1), new DateOnly(2025, 6, 30), new Guid("00000001-0000-0000-0000-000000000000"), "Випуск", "Вступ", new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2022, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), new DateOnly(2024, 1, 31), new Guid("00000003-0000-0000-0000-000000000000"), "Академвідпустка", "Вступ", new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000006-0000-0000-0000-000000000000") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000007-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000008-0000-0000-0000-000000000000") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000009-0000-0000-0000-000000000000") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000a-0000-0000-0000-000000000000") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000b-0000-0000-0000-000000000000") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000c-0000-0000-0000-000000000000") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new DateOnly(2021, 9, 1), new DateOnly(2025, 6, 30), new Guid("00000001-0000-0000-0000-000000000000"), "Випуск", "Вступ", new Guid("0000000d-0000-0000-0000-000000000000") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000e-0000-0000-0000-000000000000") },
                    { new Guid("00000010-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), new DateOnly(2025, 2, 14), new Guid("00000003-0000-0000-0000-000000000000"), "Відрахування", "Вступ", new Guid("0000000f-0000-0000-0000-000000000000") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000010-0000-0000-0000-000000000000") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new DateOnly(2022, 9, 1), new DateOnly(2024, 8, 31), new Guid("00000002-0000-0000-0000-000000000000"), "Переведення", "Вступ", new Guid("00000011-0000-0000-0000-000000000000") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Переведення", new Guid("00000011-0000-0000-0000-000000000000") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), new DateOnly(2025, 2, 1), new Guid("00000005-0000-0000-0000-000000000000"), "Академвідпустка", "Вступ", new Guid("00000012-0000-0000-0000-000000000000") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new DateOnly(2023, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000013-0000-0000-0000-000000000000") },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000014-0000-0000-0000-000000000000") },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000015-0000-0000-0000-000000000000") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000016-0000-0000-0000-000000000000") },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000017-0000-0000-0000-000000000000") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000018-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Subgroup",
                columns: new[] { "subgroup_id", "group_id", "subgroup_name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "Підгрупа 2" }
                });

            migrationBuilder.InsertData(
                table: "Academic_Leave",
                columns: new[] { "leave_id", "end_date", "enrollment_id", "reason", "return_reason", "start_date" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, new Guid("00000006-0000-0000-0000-000000000000"), "Стан здоров'я", null, new DateOnly(2024, 2, 1) },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, new Guid("00000014-0000-0000-0000-000000000000"), "Сімейні обставини", null, new DateOnly(2025, 2, 2) },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000009-0000-0000-0000-000000000000"), "Програма академічної мобільності", null, new DateOnly(2024, 11, 4) }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "discipline_id", "enrollment_id", "group_plan_assignment_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), 2021, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), 2021, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), 2021, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), 2022, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), 2022, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), 2023, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), 2024, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "discipline_id", "enrollment_id", "group_plan_assignment_id" },
                values: new object[] { new Guid("00000008-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "discipline_id", "enrollment_id", "group_plan_assignment_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000009-0000-0000-0000-000000000000"), 2021, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), 2021, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), 2021, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), 2022, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), 2022, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), 2023, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), 2023, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000010-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000011-0000-0000-0000-000000000000"), 2024, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000012-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000013-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000014-0000-0000-0000-000000000000"), 2022, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000015-0000-0000-0000-000000000000"), 2022, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000016-0000-0000-0000-000000000000"), 2022, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000017-0000-0000-0000-000000000000"), 2023, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000018-0000-0000-0000-000000000000"), 2023, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000019-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), 2023, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000020-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000021-0000-0000-0000-000000000000"), 2023, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000022-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000023-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000024-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000025-0000-0000-0000-000000000000"), 2023, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000026-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000027-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000028-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000029-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000002a-0000-0000-0000-000000000000"), 2023, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000002b-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000002c-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000002d-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000002e-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000002f-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000030-0000-0000-0000-000000000000"), 2023, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000031-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000032-0000-0000-0000-000000000000"), 2024, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Retake" },
                    { new Guid("00000033-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000034-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000035-0000-0000-0000-000000000000"), 2023, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000036-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000037-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000038-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000039-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000003a-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000003b-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000003c-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000003d-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000003e-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000003f-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000040-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000041-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000042-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000043-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000044-0000-0000-0000-000000000000"), 2021, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000045-0000-0000-0000-000000000000"), 2021, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000046-0000-0000-0000-000000000000"), 2021, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000047-0000-0000-0000-000000000000"), 2022, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000048-0000-0000-0000-000000000000"), 2022, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000049-0000-0000-0000-000000000000"), 2023, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000004a-0000-0000-0000-000000000000"), 2023, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000004b-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000004c-0000-0000-0000-000000000000"), 2024, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000004d-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000004e-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000004f-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000050-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000051-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000052-0000-0000-0000-000000000000"), 2023, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000053-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000054-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000055-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000056-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000057-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000058-0000-0000-0000-000000000000"), 2022, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000059-0000-0000-0000-000000000000"), 2022, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000005a-0000-0000-0000-000000000000"), 2022, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000005b-0000-0000-0000-000000000000"), 2023, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000005c-0000-0000-0000-000000000000"), 2023, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000005d-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000005e-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000005f-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000060-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "discipline_id", "enrollment_id", "group_plan_assignment_id" },
                values: new object[] { new Guid("00000061-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "discipline_id", "enrollment_id", "group_plan_assignment_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000062-0000-0000-0000-000000000000"), 2023, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000063-0000-0000-0000-000000000000"), 2023, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000064-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000065-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000066-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000067-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000068-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000069-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000006a-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000006b-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000006c-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000006d-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000006e-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000006f-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000070-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000071-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000072-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000073-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Subgroup_Assignment",
                columns: new[] { "enrollment_id", "subgroup_id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("0000000a-0000-0000-0000-000000000000") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000") },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000") },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new Guid("0000000b-0000-0000-0000-000000000000") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new Guid("0000000c-0000-0000-0000-000000000000") },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new Guid("0000000b-0000-0000-0000-000000000000") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Grade_Record",
                columns: new[] { "grade_id", "assessment_date", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new DateOnly(2022, 2, 11), new Guid("00000001-0000-0000-0000-000000000000"), "96" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2022, 3, 12), new Guid("00000002-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2022, 4, 13), new Guid("00000003-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2023, 6, 15), new Guid("00000004-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new DateOnly(2023, 5, 14), new Guid("00000005-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new DateOnly(2024, 7, 16), new Guid("00000006-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new DateOnly(2022, 2, 11), new Guid("00000009-0000-0000-0000-000000000000"), "98" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new DateOnly(2022, 3, 12), new Guid("0000000a-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new DateOnly(2022, 4, 13), new Guid("0000000b-0000-0000-0000-000000000000"), "92" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new DateOnly(2023, 6, 15), new Guid("0000000c-0000-0000-0000-000000000000"), "94" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new DateOnly(2023, 5, 14), new Guid("0000000d-0000-0000-0000-000000000000"), "96" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new DateOnly(2024, 7, 16), new Guid("0000000e-0000-0000-0000-000000000000"), "93" },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new DateOnly(2024, 8, 17), new Guid("0000000f-0000-0000-0000-000000000000"), "91" },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 22), new Guid("00000010-0000-0000-0000-000000000000"), "90" },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 18), new Guid("00000011-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000010-0000-0000-0000-000000000000"), new DateOnly(2026, 11, 20), new Guid("00000012-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new DateOnly(2026, 10, 19), new Guid("00000013-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new DateOnly(2023, 2, 11), new Guid("00000014-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new DateOnly(2023, 3, 12), new Guid("00000015-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new DateOnly(2023, 4, 13), new Guid("00000016-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new DateOnly(2024, 6, 15), new Guid("00000017-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new DateOnly(2024, 5, 14), new Guid("00000018-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("0000001a-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("0000001b-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new DateOnly(2024, 4, 13), new Guid("0000001c-0000-0000-0000-000000000000"), "88" },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("0000001d-0000-0000-0000-000000000000"), "91" },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("0000001f-0000-0000-0000-000000000000"), "83" },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("00000020-0000-0000-0000-000000000000"), "85" },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new DateOnly(2024, 4, 13), new Guid("00000021-0000-0000-0000-000000000000"), "81" },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000022-0000-0000-0000-000000000000"), "84" },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("00000023-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("00000024-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000021-0000-0000-0000-000000000000"), new DateOnly(2024, 4, 13), new Guid("00000025-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000022-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000026-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000023-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("00000028-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000024-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("00000029-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000025-0000-0000-0000-000000000000"), new DateOnly(2024, 4, 13), new Guid("0000002a-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000026-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("0000002b-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000027-0000-0000-0000-000000000000"), new DateOnly(2025, 7, 16), new Guid("0000002c-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000028-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("0000002e-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000029-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("0000002f-0000-0000-0000-000000000000"), "88" },
                    { new Guid("0000002a-0000-0000-0000-000000000000"), new DateOnly(2024, 4, 13), new Guid("00000030-0000-0000-0000-000000000000"), "85" },
                    { new Guid("0000002b-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000031-0000-0000-0000-000000000000"), "87" },
                    { new Guid("0000002c-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("00000033-0000-0000-0000-000000000000"), "79" },
                    { new Guid("0000002d-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("00000034-0000-0000-0000-000000000000"), "82" },
                    { new Guid("0000002e-0000-0000-0000-000000000000"), new DateOnly(2024, 4, 13), new Guid("00000035-0000-0000-0000-000000000000"), "78" },
                    { new Guid("0000002f-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000036-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000030-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("00000038-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000031-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("00000039-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000032-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("0000003a-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000033-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("0000003c-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000034-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("0000003d-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000035-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("0000003e-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000036-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("00000040-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000037-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("00000041-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000038-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000042-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000039-0000-0000-0000-000000000000"), new DateOnly(2022, 2, 11), new Guid("00000044-0000-0000-0000-000000000000"), "97" },
                    { new Guid("0000003a-0000-0000-0000-000000000000"), new DateOnly(2022, 3, 12), new Guid("00000045-0000-0000-0000-000000000000"), "96" },
                    { new Guid("0000003b-0000-0000-0000-000000000000"), new DateOnly(2022, 4, 13), new Guid("00000046-0000-0000-0000-000000000000"), "93" },
                    { new Guid("0000003c-0000-0000-0000-000000000000"), new DateOnly(2023, 6, 15), new Guid("00000047-0000-0000-0000-000000000000"), "95" },
                    { new Guid("0000003d-0000-0000-0000-000000000000"), new DateOnly(2023, 5, 14), new Guid("00000048-0000-0000-0000-000000000000"), "94" },
                    { new Guid("0000003e-0000-0000-0000-000000000000"), new DateOnly(2024, 7, 16), new Guid("00000049-0000-0000-0000-000000000000"), "92" },
                    { new Guid("0000003f-0000-0000-0000-000000000000"), new DateOnly(2024, 8, 17), new Guid("0000004a-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000040-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 22), new Guid("0000004b-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000041-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 18), new Guid("0000004c-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000042-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("0000004d-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000043-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("0000004e-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000044-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("00000050-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000045-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("00000051-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000046-0000-0000-0000-000000000000"), new DateOnly(2024, 4, 13), new Guid("00000052-0000-0000-0000-000000000000"), "71" },
                    { new Guid("00000047-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000053-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000048-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("00000054-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000049-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("00000055-0000-0000-0000-000000000000"), "91" },
                    { new Guid("0000004a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000056-0000-0000-0000-000000000000"), "87" },
                    { new Guid("0000004b-0000-0000-0000-000000000000"), new DateOnly(2023, 2, 11), new Guid("00000058-0000-0000-0000-000000000000"), "88" },
                    { new Guid("0000004c-0000-0000-0000-000000000000"), new DateOnly(2023, 3, 12), new Guid("00000059-0000-0000-0000-000000000000"), "86" },
                    { new Guid("0000004d-0000-0000-0000-000000000000"), new DateOnly(2023, 4, 13), new Guid("0000005a-0000-0000-0000-000000000000"), "84" },
                    { new Guid("0000004e-0000-0000-0000-000000000000"), new DateOnly(2024, 6, 15), new Guid("0000005b-0000-0000-0000-000000000000"), "87" },
                    { new Guid("0000004f-0000-0000-0000-000000000000"), new DateOnly(2024, 5, 14), new Guid("0000005c-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000050-0000-0000-0000-000000000000"), new DateOnly(2025, 7, 16), new Guid("0000005d-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000051-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("0000005f-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000052-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("00000060-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000053-0000-0000-0000-000000000000"), new DateOnly(2024, 2, 11), new Guid("00000062-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000054-0000-0000-0000-000000000000"), new DateOnly(2024, 3, 12), new Guid("00000063-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000055-0000-0000-0000-000000000000"), new DateOnly(2025, 4, 13), new Guid("00000064-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000056-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000065-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000057-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("00000067-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000058-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("00000068-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000059-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000069-0000-0000-0000-000000000000"), "88" },
                    { new Guid("0000005a-0000-0000-0000-000000000000"), new DateOnly(2025, 2, 11), new Guid("00000070-0000-0000-0000-000000000000"), "90" },
                    { new Guid("0000005b-0000-0000-0000-000000000000"), new DateOnly(2025, 3, 12), new Guid("00000071-0000-0000-0000-000000000000"), "89" },
                    { new Guid("0000005c-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 15), new Guid("00000072-0000-0000-0000-000000000000"), "87" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Leave_enrollment_id",
                table: "Academic_Leave",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Unit_name",
                table: "Academic_Unit",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_academic_unit_id_name",
                table: "Department",
                columns: new[] { "academic_unit_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_External_Transfers_institution_id",
                table: "External_Transfers",
                column: "institution_id");

            migrationBuilder.CreateIndex(
                name: "IX_External_Transfers_student_id",
                table: "External_Transfers",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Record_course_enrollment_id",
                table: "Grade_Record",
                column: "course_enrollment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_Plan_Assignment_plan_id",
                table: "Group_Plan_Assignment",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPlanAssignment_GroupId_DateFrom",
                table: "Group_Plan_Assignment",
                columns: new[] { "group_id", "date_from" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Disciplines_discipline_id",
                table: "Plan_Disciplines",
                column: "discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_discipline_id",
                table: "Student_Course_Enrollment",
                column: "discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_enrollment_id",
                table: "Student_Course_Enrollment",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_group_plan_assignment_id",
                table: "Student_Course_Enrollment",
                column: "group_plan_assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_GroupId_DateFrom",
                table: "Student_Group_Enrollment",
                columns: new[] { "group_id", "date_from" });

            migrationBuilder.CreateIndex(
                name: "IX_Student_Group_Enrollment_student_id",
                table: "Student_Group_Enrollment",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Subgroup_Assignment_subgroup_id",
                table: "Student_Subgroup_Assignment",
                column: "subgroup_id");

            migrationBuilder.CreateIndex(
                name: "IX_Study_Group_department_id",
                table: "Study_Group",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Study_Group_group_code",
                table: "Study_Group",
                column: "group_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subgroup_group_id",
                table: "Subgroup",
                column: "group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academic_Leave");

            migrationBuilder.DropTable(
                name: "External_Transfers");

            migrationBuilder.DropTable(
                name: "Grade_Record");

            migrationBuilder.DropTable(
                name: "Plan_Disciplines");

            migrationBuilder.DropTable(
                name: "Student_Subgroup_Assignment");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "Student_Course_Enrollment");

            migrationBuilder.DropTable(
                name: "Subgroup");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "Group_Plan_Assignment");

            migrationBuilder.DropTable(
                name: "Student_Group_Enrollment");

            migrationBuilder.DropTable(
                name: "Study_Plan");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Study_Group");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Academic_Unit");
        }
    }
}
