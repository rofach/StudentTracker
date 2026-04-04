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
                name: "Discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    institution_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    student_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                name: "Study_Group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    faculty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    date_created = table.Column<DateOnly>(type: "date", nullable: false),
                    date_closed = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study_Group", x => x.group_id);
                });

            migrationBuilder.CreateTable(
                name: "Study_Plan",
                columns: table => new
                {
                    plan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specialty_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    plan_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study_Plan", x => x.plan_id);
                });

            migrationBuilder.CreateTable(
                name: "External_Transfers",
                columns: table => new
                {
                    transfer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    institution_id = table.Column<int>(type: "int", nullable: false),
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
                name: "Student_Group_Enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false),
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
                    subgroup_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<int>(type: "int", nullable: false),
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
                name: "Plan_Disciplines",
                columns: table => new
                {
                    plan_id = table.Column<int>(type: "int", nullable: false),
                    discipline_id = table.Column<int>(type: "int", nullable: false),
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
                name: "Student_Plan_Assignment",
                columns: table => new
                {
                    assignment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    plan_id = table.Column<int>(type: "int", nullable: false),
                    date_from = table.Column<DateOnly>(type: "date", nullable: false),
                    date_to = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Plan_Assignment", x => x.assignment_id);
                    table.ForeignKey(
                        name: "FK_Student_Plan_Assignment_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Plan_Assignment_Study_Plan_plan_id",
                        column: x => x.plan_id,
                        principalTable: "Study_Plan",
                        principalColumn: "plan_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Academic_Leave",
                columns: table => new
                {
                    leave_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enrollment_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
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
                name: "Student_Subgroup_Assignment",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "int", nullable: false),
                    subgroup_id = table.Column<int>(type: "int", nullable: false)
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
                name: "Student_Course_Enrollment",
                columns: table => new
                {
                    course_enrollment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assignment_id = table.Column<int>(type: "int", nullable: false),
                    discipline_id = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Student_Course_Enrollment_Student_Plan_Assignment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "Student_Plan_Assignment",
                        principalColumn: "assignment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade_Record",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_enrollment_id = table.Column<int>(type: "int", nullable: false),
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
                table: "Discipline",
                columns: new[] { "discipline_id", "discipline_name" },
                values: new object[,]
                {
                    { 1, "Вища математика" },
                    { 2, "Основи програмування" },
                    { 3, "Дискретна математика" },
                    { 4, "Алгоритми та структури даних" },
                    { 5, "Лінійна алгебра" },
                    { 6, "Об'єктно-орієнтоване програмування" },
                    { 7, "Бази даних" },
                    { 8, "Операційні системи" },
                    { 9, "Комп'ютерні мережі" },
                    { 10, "Вебтехнології" },
                    { 11, "Архітектура комп'ютерів" },
                    { 12, "Теорія ймовірностей" }
                });

            migrationBuilder.InsertData(
                table: "Institution",
                columns: new[] { "institution_id", "city", "country", "institution_name" },
                values: new object[,]
                {
                    { 1, "Київ", "Україна", "Національний технічний університет України \"Київський політехнічний інститут імені Ігоря Сікорського\"" },
                    { 2, "Львів", "Україна", "Національний університет \"Львівська політехніка\"" },
                    { 3, "Київ", "Україна", "Київський національний університет імені Тараса Шевченка" },
                    { 4, "Харків", "Україна", "Харківський національний університет радіоелектроніки" },
                    { 5, "Одеса", "Україна", "Національний університет \"Одеська політехніка\"" },
                    { 6, "Чернігів", "Україна", "Національний університет \"Чернігівська політехніка\"" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[] { 1, new DateOnly(2003, 4, 15), "andrii.melnyk@campus.ua", "Андрій", "Мельник", "+380671000001" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[] { 2, new DateOnly(2002, 9, 3), "olena.koval@campus.ua", "Олена", "Коваль", "+380671000002", "Graduated" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[,]
                {
                    { 3, new DateOnly(2004, 1, 27), "maksym.shevchenko@campus.ua", "Максим", "Шевченко", "+380671000003" },
                    { 4, new DateOnly(2004, 6, 11), "iryna.boiko@campus.ua", "Ірина", "Бойко", "+380671000004" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[] { 5, new DateOnly(2004, 12, 1), "bohdan.tkachenko@campus.ua", "Богдан", "Ткаченко", "+380671000005", "OnLeave" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[,]
                {
                    { 6, new DateOnly(2004, 2, 18), "mariia.kravets@campus.ua", "Марія", "Кравець", "+380671000006" },
                    { 7, new DateOnly(2004, 8, 5), "dmytro.polishchuk@campus.ua", "Дмитро", "Поліщук", "+380671000007" },
                    { 8, new DateOnly(2004, 11, 22), "nataliia.savchuk@campus.ua", "Наталія", "Савчук", "+380671000008" },
                    { 9, new DateOnly(2003, 7, 14), "vladyslav.romaniuk@campus.ua", "Владислав", "Романюк", "+380671000009" },
                    { 10, new DateOnly(2005, 3, 8), "sofiia.kozak@campus.ua", "Софія", "Козак", "+380671000010" },
                    { 11, new DateOnly(2005, 9, 21), "artem.lytvyn@campus.ua", "Артем", "Литвин", "+380671000011" },
                    { 12, new DateOnly(2005, 5, 12), "kateryna.pavlenko@campus.ua", "Катерина", "Павленко", "+380671000012" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[] { 13, new DateOnly(2002, 2, 17), "yurii.moroz@campus.ua", "Юрій", "Мороз", "+380671000013", "Graduated" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[] { 14, new DateOnly(2005, 1, 30), "anastasiia.ivanchuk@campus.ua", "Анастасія", "Іванчук", "+380671000014" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[] { 15, new DateOnly(2004, 10, 6), "denys.oliinyk@campus.ua", "Денис", "Олійник", "+380671000015", "Expelled" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[,]
                {
                    { 16, new DateOnly(2005, 7, 19), "veronika.hnatiuk@campus.ua", "Вероніка", "Гнатюк", "+380671000016" },
                    { 17, new DateOnly(2004, 4, 2), "taras.bondar@campus.ua", "Тарас", "Бондар", "+380671000017" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[] { 18, new DateOnly(2005, 8, 9), "khrystyna.fedoruk@campus.ua", "Христина", "Федорук", "+380671000018", "OnLeave" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[,]
                {
                    { 19, new DateOnly(2004, 3, 25), "roman.soroka@campus.ua", "Роман", "Сорока", "+380671000019" },
                    { 20, new DateOnly(2005, 11, 13), "daryna.mazur@campus.ua", "Дарина", "Мазур", "+380671000020" },
                    { 21, new DateOnly(2006, 2, 16), "pavlo.diachenko@campus.ua", "Павло", "Дяченко", "+380671000021" },
                    { 22, new DateOnly(2006, 4, 1), "yuliia.vlasenko@campus.ua", "Юлія", "Власенко", "+380671000022" },
                    { 23, new DateOnly(2006, 6, 6), "illia.kovtun@campus.ua", "Ілля", "Ковтун", "+380671000023" },
                    { 24, new DateOnly(2005, 9, 28), "oksana.chumak@campus.ua", "Оксана", "Чумак", "+380671000024" }
                });

            migrationBuilder.InsertData(
                table: "Study_Group",
                columns: new[] { "group_id", "date_closed", "date_created", "faculty", "group_code" },
                values: new object[,]
                {
                    { 1, null, new DateOnly(2021, 9, 1), "Факультет інформатики та обчислювальної техніки", "КН-21" },
                    { 2, null, new DateOnly(2022, 9, 1), "Факультет інформатики та обчислювальної техніки", "КН-22" },
                    { 3, null, new DateOnly(2023, 9, 1), "Факультет прикладної математики", "ПЗ-23" },
                    { 4, null, new DateOnly(2024, 9, 1), "Факультет комп'ютерної інженерії", "КІ-24" },
                    { 5, null, new DateOnly(2024, 9, 1), "Факультет прикладної математики", "ПЗ-24" },
                    { 6, null, new DateOnly(2025, 9, 1), "Факультет комп'ютерної інженерії", "КІ-25" }
                });

            migrationBuilder.InsertData(
                table: "Study_Plan",
                columns: new[] { "plan_id", "plan_name", "specialty_code", "valid_from" },
                values: new object[,]
                {
                    { 1, "Комп'ютерні науки 2021", "122", new DateOnly(2021, 9, 1) },
                    { 2, "Інженерія програмного забезпечення 2023", "121", new DateOnly(2023, 9, 1) },
                    { 3, "Комп'ютерна інженерія 2024", "123", new DateOnly(2024, 9, 1) }
                });

            migrationBuilder.InsertData(
                table: "External_Transfers",
                columns: new[] { "transfer_id", "institution_id", "notes", "student_id", "transfer_date", "transfer_type" },
                values: new object[,]
                {
                    { 1, 4, "Переведення після другого семестру", 4, new DateOnly(2023, 8, 25), "In" },
                    { 2, 3, "Продовження навчання після переїзду", 11, new DateOnly(2024, 8, 28), "In" },
                    { 3, 2, "Подальше навчання в магістратурі", 13, new DateOnly(2025, 7, 2), "Out" },
                    { 4, 6, "Переведення з іншого закладу", 24, new DateOnly(2024, 8, 26), "In" }
                });

            migrationBuilder.InsertData(
                table: "Plan_Disciplines",
                columns: new[] { "discipline_id", "plan_id", "control_type", "credits", "hours", "semester_no" },
                values: new object[,]
                {
                    { 1, 1, "Exam", 5.0m, 150, 1 },
                    { 2, 1, "Exam", 6.0m, 180, 1 },
                    { 3, 1, "Credit", 4.0m, 120, 1 },
                    { 4, 1, "Exam", 6.0m, 180, 2 },
                    { 5, 1, "Exam", 4.0m, 120, 2 },
                    { 6, 1, "Coursework", 5.0m, 150, 3 },
                    { 7, 1, "Exam", 5.0m, 150, 3 },
                    { 8, 1, "Exam", 5.0m, 150, 4 },
                    { 9, 1, "Exam", 4.0m, 120, 5 },
                    { 10, 1, "Coursework", 5.0m, 150, 5 },
                    { 12, 1, "Exam", 4.0m, 120, 4 },
                    { 1, 2, "Exam", 5.0m, 150, 1 },
                    { 2, 2, "Exam", 6.0m, 180, 1 },
                    { 3, 2, "Credit", 4.0m, 120, 1 },
                    { 5, 2, "Exam", 4.0m, 120, 2 },
                    { 6, 2, "Coursework", 5.0m, 150, 2 },
                    { 7, 2, "Exam", 5.0m, 150, 3 },
                    { 8, 2, "Exam", 5.0m, 150, 4 },
                    { 9, 2, "Exam", 4.0m, 120, 4 },
                    { 10, 2, "Coursework", 5.0m, 150, 3 },
                    { 1, 3, "Exam", 5.0m, 150, 1 },
                    { 2, 3, "Exam", 6.0m, 180, 1 },
                    { 4, 3, "Exam", 6.0m, 180, 2 },
                    { 5, 3, "Credit", 4.0m, 120, 1 },
                    { 7, 3, "Exam", 5.0m, 150, 4 },
                    { 8, 3, "Exam", 5.0m, 150, 3 },
                    { 9, 3, "Exam", 4.0m, 120, 3 },
                    { 11, 3, "Exam", 5.0m, 150, 2 }
                });

            migrationBuilder.InsertData(
                table: "Student_Group_Enrollment",
                columns: new[] { "enrollment_id", "date_from", "date_to", "group_id", "reason_end", "reason_start", "student_id" },
                values: new object[,]
                {
                    { 1, new DateOnly(2021, 9, 1), new DateOnly(2022, 8, 31), 1, "Переведення", "Вступ", 1 },
                    { 2, new DateOnly(2022, 9, 1), null, 2, null, "Переведення", 1 },
                    { 3, new DateOnly(2021, 9, 1), new DateOnly(2025, 6, 30), 1, "Випуск", "Вступ", 2 },
                    { 4, new DateOnly(2022, 9, 1), null, 2, null, "Вступ", 3 },
                    { 5, new DateOnly(2023, 9, 1), null, 3, null, "Вступ", 4 },
                    { 6, new DateOnly(2023, 9, 1), new DateOnly(2024, 1, 31), 3, "Академвідпустка", "Вступ", 5 },
                    { 7, new DateOnly(2023, 9, 1), null, 3, null, "Вступ", 6 },
                    { 8, new DateOnly(2023, 9, 1), null, 3, null, "Вступ", 7 },
                    { 9, new DateOnly(2023, 9, 1), null, 3, null, "Вступ", 8 },
                    { 10, new DateOnly(2023, 9, 1), null, 3, null, "Вступ", 9 },
                    { 11, new DateOnly(2024, 9, 1), null, 4, null, "Вступ", 10 },
                    { 12, new DateOnly(2024, 9, 1), null, 4, null, "Вступ", 11 },
                    { 13, new DateOnly(2024, 9, 1), null, 5, null, "Вступ", 12 },
                    { 14, new DateOnly(2021, 9, 1), new DateOnly(2025, 6, 30), 1, "Випуск", "Вступ", 13 },
                    { 15, new DateOnly(2024, 9, 1), null, 5, null, "Вступ", 14 },
                    { 16, new DateOnly(2023, 9, 1), new DateOnly(2025, 2, 14), 3, "Відрахування", "Вступ", 15 },
                    { 17, new DateOnly(2024, 9, 1), null, 4, null, "Вступ", 16 },
                    { 18, new DateOnly(2022, 9, 1), new DateOnly(2024, 8, 31), 2, "Переведення", "Вступ", 17 },
                    { 19, new DateOnly(2024, 9, 1), null, 3, null, "Переведення", 17 },
                    { 20, new DateOnly(2024, 9, 1), new DateOnly(2025, 2, 1), 5, "Академвідпустка", "Вступ", 18 },
                    { 21, new DateOnly(2023, 9, 1), null, 3, null, "Вступ", 19 },
                    { 22, new DateOnly(2024, 9, 1), null, 4, null, "Вступ", 20 },
                    { 23, new DateOnly(2025, 9, 1), null, 6, null, "Вступ", 21 },
                    { 24, new DateOnly(2025, 9, 1), null, 6, null, "Вступ", 22 },
                    { 25, new DateOnly(2025, 9, 1), null, 6, null, "Вступ", 23 },
                    { 26, new DateOnly(2024, 9, 1), null, 4, null, "Вступ", 24 }
                });

            migrationBuilder.InsertData(
                table: "Student_Plan_Assignment",
                columns: new[] { "assignment_id", "date_from", "date_to", "plan_id", "student_id" },
                values: new object[,]
                {
                    { 1, new DateOnly(2021, 9, 1), null, 1, 1 },
                    { 2, new DateOnly(2021, 9, 1), new DateOnly(2025, 6, 30), 1, 2 },
                    { 3, new DateOnly(2022, 9, 1), null, 1, 3 },
                    { 4, new DateOnly(2023, 9, 1), null, 2, 4 },
                    { 5, new DateOnly(2023, 9, 1), null, 2, 5 },
                    { 6, new DateOnly(2023, 9, 1), null, 2, 6 },
                    { 7, new DateOnly(2023, 9, 1), null, 2, 7 },
                    { 8, new DateOnly(2023, 9, 1), null, 2, 8 },
                    { 9, new DateOnly(2023, 9, 1), null, 2, 9 },
                    { 10, new DateOnly(2024, 9, 1), null, 3, 10 },
                    { 11, new DateOnly(2024, 9, 1), null, 3, 11 },
                    { 12, new DateOnly(2024, 9, 1), null, 3, 12 },
                    { 13, new DateOnly(2021, 9, 1), new DateOnly(2025, 6, 30), 1, 13 },
                    { 14, new DateOnly(2024, 9, 1), null, 2, 14 },
                    { 15, new DateOnly(2023, 9, 1), new DateOnly(2025, 2, 14), 2, 15 },
                    { 16, new DateOnly(2024, 9, 1), null, 3, 16 },
                    { 17, new DateOnly(2022, 9, 1), null, 1, 17 },
                    { 18, new DateOnly(2024, 9, 1), null, 2, 18 },
                    { 19, new DateOnly(2023, 9, 1), null, 2, 19 },
                    { 20, new DateOnly(2024, 9, 1), null, 3, 20 },
                    { 21, new DateOnly(2025, 9, 1), null, 3, 21 },
                    { 22, new DateOnly(2025, 9, 1), null, 3, 22 },
                    { 23, new DateOnly(2025, 9, 1), null, 3, 23 },
                    { 24, new DateOnly(2024, 9, 1), null, 3, 24 }
                });

            migrationBuilder.InsertData(
                table: "Subgroup",
                columns: new[] { "subgroup_id", "group_id", "subgroup_name" },
                values: new object[,]
                {
                    { 1, 1, "Підгрупа 1" },
                    { 2, 1, "Підгрупа 2" },
                    { 3, 2, "Підгрупа 1" },
                    { 4, 2, "Підгрупа 2" },
                    { 5, 3, "Підгрупа 1" },
                    { 6, 3, "Підгрупа 2" },
                    { 7, 4, "Підгрупа 1" },
                    { 8, 4, "Підгрупа 2" },
                    { 9, 5, "Підгрупа 1" },
                    { 10, 5, "Підгрупа 2" },
                    { 11, 6, "Підгрупа 1" },
                    { 12, 6, "Підгрупа 2" }
                });

            migrationBuilder.InsertData(
                table: "Academic_Leave",
                columns: new[] { "leave_id", "end_date", "enrollment_id", "reason", "start_date" },
                values: new object[,]
                {
                    { 1, null, 6, "Стан здоров'я", new DateOnly(2024, 2, 1) },
                    { 2, null, 20, "Сімейні обставини", new DateOnly(2025, 2, 2) },
                    { 3, new DateOnly(2025, 1, 20), 9, "Програма академічної мобільності", new DateOnly(2024, 11, 4) }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 1, 2021, 1, 1, "Completed" },
                    { 2, 2021, 1, 2, "Completed" },
                    { 3, 2021, 1, 3, "Completed" },
                    { 4, 2022, 1, 5, "Completed" },
                    { 5, 2022, 1, 4, "Completed" },
                    { 6, 2023, 1, 6, "Completed" },
                    { 7, 2024, 1, 7, "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id" },
                values: new object[] { 8, 2024, 1, 12 });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 9, 2021, 2, 1, "Completed" },
                    { 10, 2021, 2, 2, "Completed" },
                    { 11, 2021, 2, 3, "Completed" },
                    { 12, 2022, 2, 5, "Completed" },
                    { 13, 2022, 2, 4, "Completed" },
                    { 14, 2023, 2, 6, "Completed" },
                    { 15, 2023, 2, 7, "Completed" },
                    { 16, 2024, 2, 12, "Completed" },
                    { 17, 2024, 2, 8, "Completed" },
                    { 18, 2025, 2, 10, "Completed" },
                    { 19, 2025, 2, 9, "Completed" },
                    { 20, 2022, 3, 1, "Completed" },
                    { 21, 2022, 3, 2, "Completed" },
                    { 22, 2022, 3, 3, "Completed" },
                    { 23, 2023, 3, 5, "Completed" },
                    { 24, 2023, 3, 4, "Completed" },
                    { 25, 2024, 3, 6, "InProgress" },
                    { 26, 2023, 4, 1, "Completed" },
                    { 27, 2023, 4, 2, "Completed" },
                    { 28, 2023, 4, 3, "Completed" },
                    { 29, 2024, 4, 5, "Completed" },
                    { 30, 2024, 4, 6, "InProgress" },
                    { 31, 2023, 5, 1, "Completed" },
                    { 32, 2023, 5, 2, "Completed" },
                    { 33, 2023, 5, 3, "Completed" },
                    { 34, 2024, 5, 5, "Completed" },
                    { 35, 2023, 6, 1, "Completed" },
                    { 36, 2023, 6, 2, "Completed" },
                    { 37, 2023, 6, 3, "Completed" },
                    { 38, 2024, 6, 5, "Completed" },
                    { 39, 2024, 6, 6, "InProgress" },
                    { 40, 2023, 7, 1, "Completed" },
                    { 41, 2023, 7, 2, "Completed" },
                    { 42, 2023, 7, 3, "Completed" },
                    { 43, 2024, 7, 5, "Completed" },
                    { 44, 2024, 7, 6, "Completed" },
                    { 45, 2025, 7, 7, "InProgress" },
                    { 46, 2023, 8, 1, "Completed" },
                    { 47, 2023, 8, 2, "Completed" },
                    { 48, 2023, 8, 3, "Completed" },
                    { 49, 2024, 8, 5, "Completed" },
                    { 50, 2024, 8, 10, "Retake" },
                    { 51, 2023, 9, 1, "Completed" },
                    { 52, 2023, 9, 2, "Completed" },
                    { 53, 2023, 9, 3, "Completed" },
                    { 54, 2024, 9, 5, "Completed" },
                    { 55, 2024, 9, 6, "InProgress" },
                    { 56, 2024, 10, 1, "Completed" },
                    { 57, 2024, 10, 2, "Completed" },
                    { 58, 2024, 10, 5, "Completed" },
                    { 59, 2025, 10, 11, "InProgress" },
                    { 60, 2024, 11, 1, "Completed" },
                    { 61, 2024, 11, 2, "Completed" },
                    { 62, 2024, 11, 5, "Completed" },
                    { 63, 2025, 11, 11, "InProgress" },
                    { 64, 2024, 12, 1, "Completed" },
                    { 65, 2024, 12, 2, "Completed" },
                    { 66, 2024, 12, 5, "Completed" },
                    { 67, 2025, 12, 11, "InProgress" },
                    { 68, 2021, 13, 1, "Completed" },
                    { 69, 2021, 13, 2, "Completed" },
                    { 70, 2021, 13, 3, "Completed" },
                    { 71, 2022, 13, 5, "Completed" },
                    { 72, 2022, 13, 4, "Completed" },
                    { 73, 2023, 13, 6, "Completed" },
                    { 74, 2023, 13, 7, "Completed" },
                    { 75, 2024, 13, 12, "Completed" },
                    { 76, 2024, 13, 8, "Completed" },
                    { 77, 2024, 14, 1, "Completed" },
                    { 78, 2024, 14, 2, "Completed" },
                    { 79, 2024, 14, 3, "InProgress" },
                    { 80, 2023, 15, 1, "Completed" },
                    { 81, 2023, 15, 2, "Completed" },
                    { 82, 2023, 15, 3, "Completed" },
                    { 83, 2024, 15, 5, "Completed" },
                    { 84, 2024, 16, 1, "Completed" },
                    { 85, 2024, 16, 2, "Completed" },
                    { 86, 2024, 16, 5, "Completed" },
                    { 87, 2025, 16, 11, "InProgress" },
                    { 88, 2022, 17, 1, "Completed" },
                    { 89, 2022, 17, 2, "Completed" },
                    { 90, 2022, 17, 3, "Completed" },
                    { 91, 2023, 17, 5, "Completed" },
                    { 92, 2023, 17, 4, "Completed" },
                    { 93, 2024, 17, 6, "Completed" },
                    { 94, 2025, 17, 7, "InProgress" },
                    { 95, 2024, 18, 1, "Completed" },
                    { 96, 2024, 18, 2, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id" },
                values: new object[] { 97, 2025, 18, 3 });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 98, 2023, 19, 1, "Completed" },
                    { 99, 2023, 19, 2, "Completed" },
                    { 100, 2024, 19, 3, "Completed" },
                    { 101, 2024, 19, 5, "Completed" },
                    { 102, 2025, 19, 6, "InProgress" },
                    { 103, 2024, 20, 1, "Completed" },
                    { 104, 2024, 20, 2, "Completed" },
                    { 105, 2024, 20, 5, "Completed" },
                    { 106, 2025, 21, 1, "InProgress" },
                    { 107, 2025, 21, 2, "InProgress" },
                    { 108, 2025, 22, 1, "InProgress" },
                    { 109, 2025, 22, 2, "InProgress" },
                    { 110, 2025, 23, 1, "InProgress" },
                    { 111, 2025, 23, 2, "InProgress" },
                    { 112, 2024, 24, 1, "Completed" },
                    { 113, 2024, 24, 2, "Completed" },
                    { 114, 2024, 24, 5, "Completed" },
                    { 115, 2025, 24, 11, "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Subgroup_Assignment",
                columns: new[] { "enrollment_id", "subgroup_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 3, 2 },
                    { 4, 4 },
                    { 5, 5 },
                    { 7, 6 },
                    { 8, 5 },
                    { 9, 6 },
                    { 10, 5 },
                    { 11, 7 },
                    { 12, 8 },
                    { 13, 9 },
                    { 14, 1 },
                    { 15, 10 },
                    { 17, 8 },
                    { 18, 3 },
                    { 19, 6 },
                    { 20, 9 },
                    { 21, 6 },
                    { 22, 7 },
                    { 23, 11 },
                    { 24, 12 },
                    { 25, 11 },
                    { 26, 8 }
                });

            migrationBuilder.InsertData(
                table: "Grade_Record",
                columns: new[] { "grade_id", "assessment_date", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { 1, new DateOnly(2022, 2, 11), 1, "96" },
                    { 2, new DateOnly(2022, 3, 12), 2, "94" },
                    { 3, new DateOnly(2022, 4, 13), 3, "90" },
                    { 4, new DateOnly(2023, 6, 15), 4, "91" },
                    { 5, new DateOnly(2023, 5, 14), 5, "93" },
                    { 6, new DateOnly(2024, 7, 16), 6, "95" },
                    { 7, new DateOnly(2022, 2, 11), 9, "98" },
                    { 8, new DateOnly(2022, 3, 12), 10, "95" },
                    { 9, new DateOnly(2022, 4, 13), 11, "92" },
                    { 10, new DateOnly(2023, 6, 15), 12, "94" },
                    { 11, new DateOnly(2023, 5, 14), 13, "96" },
                    { 12, new DateOnly(2024, 7, 16), 14, "93" },
                    { 13, new DateOnly(2024, 8, 17), 15, "91" },
                    { 14, new DateOnly(2025, 1, 22), 16, "90" },
                    { 15, new DateOnly(2025, 9, 18), 17, "89" },
                    { 16, new DateOnly(2026, 11, 20), 18, "92" },
                    { 17, new DateOnly(2026, 10, 19), 19, "88" },
                    { 18, new DateOnly(2023, 2, 11), 20, "87" },
                    { 19, new DateOnly(2023, 3, 12), 21, "89" },
                    { 20, new DateOnly(2023, 4, 13), 22, "84" },
                    { 21, new DateOnly(2024, 6, 15), 23, "86" },
                    { 22, new DateOnly(2024, 5, 14), 24, "88" },
                    { 23, new DateOnly(2024, 2, 11), 26, "90" },
                    { 24, new DateOnly(2024, 3, 12), 27, "92" },
                    { 25, new DateOnly(2024, 4, 13), 28, "88" },
                    { 26, new DateOnly(2025, 6, 15), 29, "91" },
                    { 27, new DateOnly(2024, 2, 11), 31, "83" },
                    { 28, new DateOnly(2024, 3, 12), 32, "85" },
                    { 29, new DateOnly(2024, 4, 13), 33, "81" },
                    { 30, new DateOnly(2025, 6, 15), 34, "84" },
                    { 31, new DateOnly(2024, 2, 11), 35, "88" },
                    { 32, new DateOnly(2024, 3, 12), 36, "87" },
                    { 33, new DateOnly(2024, 4, 13), 37, "86" },
                    { 34, new DateOnly(2025, 6, 15), 38, "89" },
                    { 35, new DateOnly(2024, 2, 11), 40, "82" },
                    { 36, new DateOnly(2024, 3, 12), 41, "84" },
                    { 37, new DateOnly(2024, 4, 13), 42, "80" },
                    { 38, new DateOnly(2025, 6, 15), 43, "83" },
                    { 39, new DateOnly(2025, 7, 16), 44, "85" },
                    { 40, new DateOnly(2024, 2, 11), 46, "86" },
                    { 41, new DateOnly(2024, 3, 12), 47, "88" },
                    { 42, new DateOnly(2024, 4, 13), 48, "85" },
                    { 43, new DateOnly(2025, 6, 15), 49, "87" },
                    { 44, new DateOnly(2024, 2, 11), 51, "79" },
                    { 45, new DateOnly(2024, 3, 12), 52, "82" },
                    { 46, new DateOnly(2024, 4, 13), 53, "78" },
                    { 47, new DateOnly(2025, 6, 15), 54, "81" },
                    { 48, new DateOnly(2025, 2, 11), 56, "93" },
                    { 49, new DateOnly(2025, 3, 12), 57, "95" },
                    { 50, new DateOnly(2025, 6, 15), 58, "90" },
                    { 51, new DateOnly(2025, 2, 11), 60, "91" },
                    { 52, new DateOnly(2025, 3, 12), 61, "92" },
                    { 53, new DateOnly(2025, 6, 15), 62, "89" },
                    { 54, new DateOnly(2025, 2, 11), 64, "94" },
                    { 55, new DateOnly(2025, 3, 12), 65, "90" },
                    { 56, new DateOnly(2025, 6, 15), 66, "88" },
                    { 57, new DateOnly(2022, 2, 11), 68, "97" },
                    { 58, new DateOnly(2022, 3, 12), 69, "96" },
                    { 59, new DateOnly(2022, 4, 13), 70, "93" },
                    { 60, new DateOnly(2023, 6, 15), 71, "95" },
                    { 61, new DateOnly(2023, 5, 14), 72, "94" },
                    { 62, new DateOnly(2024, 7, 16), 73, "92" },
                    { 63, new DateOnly(2024, 8, 17), 74, "91" },
                    { 64, new DateOnly(2025, 1, 22), 75, "90" },
                    { 65, new DateOnly(2025, 9, 18), 76, "89" },
                    { 66, new DateOnly(2025, 2, 11), 77, "90" },
                    { 67, new DateOnly(2025, 3, 12), 78, "91" },
                    { 68, new DateOnly(2024, 2, 11), 80, "73" },
                    { 69, new DateOnly(2024, 3, 12), 81, "76" },
                    { 70, new DateOnly(2024, 4, 13), 82, "71" },
                    { 71, new DateOnly(2025, 6, 15), 83, "74" },
                    { 72, new DateOnly(2025, 2, 11), 84, "89" },
                    { 73, new DateOnly(2025, 3, 12), 85, "91" },
                    { 74, new DateOnly(2025, 6, 15), 86, "87" },
                    { 75, new DateOnly(2023, 2, 11), 88, "88" },
                    { 76, new DateOnly(2023, 3, 12), 89, "86" },
                    { 77, new DateOnly(2023, 4, 13), 90, "84" },
                    { 78, new DateOnly(2024, 6, 15), 91, "87" },
                    { 79, new DateOnly(2024, 5, 14), 92, "85" },
                    { 80, new DateOnly(2025, 7, 16), 93, "89" },
                    { 81, new DateOnly(2025, 2, 11), 95, "85" },
                    { 82, new DateOnly(2025, 3, 12), 96, "84" },
                    { 83, new DateOnly(2024, 2, 11), 98, "84" },
                    { 84, new DateOnly(2024, 3, 12), 99, "86" },
                    { 85, new DateOnly(2025, 4, 13), 100, "82" },
                    { 86, new DateOnly(2025, 6, 15), 101, "85" },
                    { 87, new DateOnly(2025, 2, 11), 103, "92" },
                    { 88, new DateOnly(2025, 3, 12), 104, "90" },
                    { 89, new DateOnly(2025, 6, 15), 105, "88" },
                    { 90, new DateOnly(2025, 2, 11), 112, "90" },
                    { 91, new DateOnly(2025, 3, 12), 113, "89" },
                    { 92, new DateOnly(2025, 6, 15), 114, "87" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Leave_enrollment_id",
                table: "Academic_Leave",
                column: "enrollment_id");

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
                name: "IX_Plan_Disciplines_discipline_id",
                table: "Plan_Disciplines",
                column: "discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_assignment_id",
                table: "Student_Course_Enrollment",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_discipline_id",
                table: "Student_Course_Enrollment",
                column: "discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_GroupId_DateFrom",
                table: "Student_Group_Enrollment",
                columns: new[] { "group_id", "date_from" });

            migrationBuilder.CreateIndex(
                name: "IX_Student_Group_Enrollment_student_id",
                table: "Student_Group_Enrollment",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Plan_Assignment_plan_id",
                table: "Student_Plan_Assignment",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Plan_Assignment_student_id",
                table: "Student_Plan_Assignment",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Subgroup_Assignment_subgroup_id",
                table: "Student_Subgroup_Assignment",
                column: "subgroup_id");

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
                name: "Student_Group_Enrollment");

            migrationBuilder.DropTable(
                name: "Subgroup");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "Student_Plan_Assignment");

            migrationBuilder.DropTable(
                name: "Study_Group");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Study_Plan");
        }
    }
}
