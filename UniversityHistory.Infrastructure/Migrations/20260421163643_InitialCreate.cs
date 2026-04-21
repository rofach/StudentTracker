using System;
using Microsoft.EntityFrameworkCore.Migrations;
using UniversityHistory.Infrastructure.Migrations.Sql;

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
                    discipline_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
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
                    plan_discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    plan_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    semester_no = table.Column<int>(type: "int", nullable: false),
                    control_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    credits = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan_Disciplines", x => x.plan_discipline_id);
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
                    plan_discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    academic_year_start = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'Planned'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Course_Enrollment", x => x.course_enrollment_id);
                    table.CheckConstraint("chk_course_status", "status IN ('Planned','InProgress','Completed','Retake')");
                    table.ForeignKey(
                        name: "FK_Student_Course_Enrollment_Group_Plan_Assignment_group_plan_assignment_id",
                        column: x => x.group_plan_assignment_id,
                        principalTable: "Group_Plan_Assignment",
                        principalColumn: "group_plan_assignment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Course_Enrollment_Plan_Disciplines_plan_discipline_id",
                        column: x => x.plan_discipline_id,
                        principalTable: "Plan_Disciplines",
                        principalColumn: "plan_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Course_Enrollment_Student_Group_Enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student_Group_Transfer",
                columns: table => new
                {
                    transfer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    old_enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    new_enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transfer_date = table.Column<DateOnly>(type: "date", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Group_Transfer", x => x.transfer_id);
                    table.ForeignKey(
                        name: "FK_Student_Group_Transfer_Student_Group_Enrollment_new_enrollment_id",
                        column: x => x.new_enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Group_Transfer_Student_Group_Enrollment_old_enrollment_id",
                        column: x => x.old_enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student_Subgroup_Enrollment",
                columns: table => new
                {
                    subgroup_enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subgroup_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date_from = table.Column<DateOnly>(type: "date", nullable: false),
                    date_to = table.Column<DateOnly>(type: "date", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Subgroup_Enrollment", x => x.subgroup_enrollment_id);
                    table.ForeignKey(
                        name: "FK_Student_Subgroup_Enrollment_Student_Group_Enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Subgroup_Enrollment_Subgroup_subgroup_id",
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

            migrationBuilder.CreateTable(
                name: "Academic_Difference_Item",
                columns: table => new
                {
                    difference_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transfer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    plan_discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'Pending'"),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic_Difference_Item", x => x.difference_item_id);
                    table.CheckConstraint("chk_diff_item_status", "status IN ('Pending','Completed','Waived')");
                    table.ForeignKey(
                        name: "FK_Academic_Difference_Item_Plan_Disciplines_plan_discipline_id",
                        column: x => x.plan_discipline_id,
                        principalTable: "Plan_Disciplines",
                        principalColumn: "plan_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Academic_Difference_Item_Student_Group_Transfer_transfer_id",
                        column: x => x.transfer_id,
                        principalTable: "Student_Group_Transfer",
                        principalColumn: "transfer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Academic_Unit",
                columns: new[] { "academic_unit_id", "name", "type" },
                values: new object[] { new Guid("00000001-0000-0000-0000-000000000000"), "Факультет комп'ютерних наук та інженерії", "Faculty" });

            migrationBuilder.InsertData(
                table: "Discipline",
                columns: new[] { "discipline_id", "description", "discipline_name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Навчальна дисципліна «Українська мова за професійним спрямуванням» із професійної підготовки здобувачів ІТ-спеціальностей.", "Українська мова за професійним спрямуванням" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Математична база для аналізу, моделювання та розв’язування інженерних задач.", "Вища математика" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Навчальна дисципліна «Фізика (вибрані розділи)» із професійної підготовки здобувачів ІТ-спеціальностей.", "Фізика (вибрані розділи)" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Навчальна дисципліна «Теорія ймовірностей та математична статистика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Теорія ймовірностей та математична статистика" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "Навчальна дисципліна «Підготовка до тестів з іноземної мови» із професійної підготовки здобувачів ІТ-спеціальностей.", "Підготовка до тестів з іноземної мови" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), "Навчальна дисципліна «Іноземна мова за професійним спрямуванням» із професійної підготовки здобувачів ІТ-спеціальностей.", "Іноземна мова за професійним спрямуванням" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), "Базова дисципліна з алгоритмізації, практики програмування та побудови обчислювальних рішень.", "Програмування та алгоритмічні мови" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), "Навчальна дисципліна «Дискретна математика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Дискретна математика" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), "Навчальна дисципліна «Методи та засоби комп'ютерних інформаційних технологій» із професійної підготовки здобувачів ІТ-спеціальностей.", "Методи та засоби комп'ютерних інформаційних технологій" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), "Навчальна дисципліна «Вступ до інженерії програмного забезпечення» із професійної підготовки здобувачів ІТ-спеціальностей.", "Вступ до інженерії програмного забезпечення" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), "Проєктування програмних систем із використанням класів, об’єктів, інкапсуляції та наслідування.", "Об'єктно-орієнтоване програмування" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), "Навчальна дисципліна «Алгоритми та структури даних» із професійної підготовки здобувачів ІТ-спеціальностей.", "Алгоритми та структури даних" },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), "Навчальна дисципліна «Комп'ютерна схемотехніка та архітектура комп'ютерів» із професійної підготовки здобувачів ІТ-спеціальностей.", "Комп'ютерна схемотехніка та архітектура комп'ютерів" },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), "Навчальна дисципліна «Чисельні методи» із професійної підготовки здобувачів ІТ-спеціальностей.", "Чисельні методи" },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), "Навчальна дисципліна «Людино-машинний інтерфейс» із професійної підготовки здобувачів ІТ-спеціальностей.", "Людино-машинний інтерфейс" },
                    { new Guid("00000010-0000-0000-0000-000000000000"), "Навчальна дисципліна «Курсова робота з об'єктно-орієнтованого програмування» із професійної підготовки здобувачів ІТ-спеціальностей.", "Курсова робота з об'єктно-орієнтованого програмування" },
                    { new Guid("00000011-0000-0000-0000-000000000000"), "Проєктування схем даних, робота з SQL та побудова прикладних рішень на основі баз даних.", "Організація баз даних та знань" },
                    { new Guid("00000012-0000-0000-0000-000000000000"), "Навчальна дисципліна «Операційні системи» із професійної підготовки здобувачів ІТ-спеціальностей.", "Операційні системи" },
                    { new Guid("00000013-0000-0000-0000-000000000000"), "Навчальна дисципліна «Вебпрограмування» із професійної підготовки здобувачів ІТ-спеціальностей.", "Вебпрограмування" },
                    { new Guid("00000014-0000-0000-0000-000000000000"), "Навчальна дисципліна «Аналіз вимог до програмного забезпечення» із професійної підготовки здобувачів ІТ-спеціальностей.", "Аналіз вимог до програмного забезпечення" },
                    { new Guid("00000015-0000-0000-0000-000000000000"), "Навчальна дисципліна «Навчальна практика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Навчальна практика" },
                    { new Guid("00000016-0000-0000-0000-000000000000"), "Навчальна дисципліна «Паралельні обчислювальні процеси» із професійної підготовки здобувачів ІТ-спеціальностей.", "Паралельні обчислювальні процеси" },
                    { new Guid("00000017-0000-0000-0000-000000000000"), "Навчальна дисципліна «Проектування та архітектура програмного забезпечення» із професійної підготовки здобувачів ІТ-спеціальностей.", "Проектування та архітектура програмного забезпечення" },
                    { new Guid("00000018-0000-0000-0000-000000000000"), "Навчальна дисципліна «Курсова робота з баз даних» із професійної підготовки здобувачів ІТ-спеціальностей.", "Курсова робота з баз даних" },
                    { new Guid("00000019-0000-0000-0000-000000000000"), "Навчальна дисципліна «Виробнича проєктно-технологічна практика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Виробнича проєктно-технологічна практика" },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), "Навчальна дисципліна «Емпіричні методи програмної інженерії» із професійної підготовки здобувачів ІТ-спеціальностей.", "Емпіричні методи програмної інженерії" },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), "Навчальна дисципліна «Моделювання та аналіз програмного забезпечення» із професійної підготовки здобувачів ІТ-спеціальностей.", "Моделювання та аналіз програмного забезпечення" },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), "Навчальна дисципліна «Професійна практика програмної інженерії» із професійної підготовки здобувачів ІТ-спеціальностей.", "Професійна практика програмної інженерії" },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), "Навчальна дисципліна «Захист інформації в комп'ютерних системах» із професійної підготовки здобувачів ІТ-спеціальностей.", "Захист інформації в комп'ютерних системах" },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), "Підходи до забезпечення якості, верифікації, тестування та аналізу дефектів програмних продуктів.", "Якість програмного забезпечення та тестування" },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), "Навчальна дисципліна «Економіка програмного забезпечення» із професійної підготовки здобувачів ІТ-спеціальностей.", "Економіка програмного забезпечення" },
                    { new Guid("00000020-0000-0000-0000-000000000000"), "Навчальна дисципліна «Адміністрування операційних систем» із професійної підготовки здобувачів ІТ-спеціальностей.", "Адміністрування операційних систем" },
                    { new Guid("00000021-0000-0000-0000-000000000000"), "Навчальна дисципліна «Проєктно-дослідницька практика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Проєктно-дослідницька практика" },
                    { new Guid("00000022-0000-0000-0000-000000000000"), "Навчальна дисципліна «Кваліфікаційна робота» із професійної підготовки здобувачів ІТ-спеціальностей.", "Кваліфікаційна робота" },
                    { new Guid("00000023-0000-0000-0000-000000000000"), "Навчальна дисципліна «Іноземна мова» із професійної підготовки здобувачів ІТ-спеціальностей.", "Іноземна мова" },
                    { new Guid("00000024-0000-0000-0000-000000000000"), "Навчальна дисципліна «Англійська мова (фахового спрямування)» із професійної підготовки здобувачів ІТ-спеціальностей.", "Англійська мова (фахового спрямування)" },
                    { new Guid("00000025-0000-0000-0000-000000000000"), "Навчальна дисципліна «Вступ до спеціальності» із професійної підготовки здобувачів ІТ-спеціальностей.", "Вступ до спеціальності" },
                    { new Guid("00000026-0000-0000-0000-000000000000"), "Навчальна дисципліна «Теорія інформації та кодування» із професійної підготовки здобувачів ІТ-спеціальностей.", "Теорія інформації та кодування" },
                    { new Guid("00000027-0000-0000-0000-000000000000"), "Навчальна дисципліна «Методи оптимізації та дослідження операцій» із професійної підготовки здобувачів ІТ-спеціальностей.", "Методи оптимізації та дослідження операцій" },
                    { new Guid("00000028-0000-0000-0000-000000000000"), "Навчальна дисципліна «Курсова робота з ООП» із професійної підготовки здобувачів ІТ-спеціальностей.", "Курсова робота з ООП" },
                    { new Guid("00000029-0000-0000-0000-000000000000"), "Проєктування схем даних, робота з SQL та побудова прикладних рішень на основі баз даних.", "Організація баз даних і знань" },
                    { new Guid("0000002a-0000-0000-0000-000000000000"), "Навчальна дисципліна «Технологія створення програмних продуктів» із професійної підготовки здобувачів ІТ-спеціальностей.", "Технологія створення програмних продуктів" },
                    { new Guid("0000002b-0000-0000-0000-000000000000"), "Мережеві моделі, протоколи передавання даних та практичні основи адміністрування мереж.", "Комп'ютерні мережі" },
                    { new Guid("0000002c-0000-0000-0000-000000000000"), "Навчальна дисципліна «Паралельні обчислення та розподілені системи» із професійної підготовки здобувачів ІТ-спеціальностей.", "Паралельні обчислення та розподілені системи" },
                    { new Guid("0000002d-0000-0000-0000-000000000000"), "Навчальна дисципліна «Виробнича проектно-технологічна практика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Виробнича проектно-технологічна практика" },
                    { new Guid("0000002e-0000-0000-0000-000000000000"), "Навчальна дисципліна «Технології захисту інформації» із професійної підготовки здобувачів ІТ-спеціальностей.", "Технології захисту інформації" },
                    { new Guid("0000002f-0000-0000-0000-000000000000"), "Навчальна дисципліна «Моделювання процесів та SMART-систем» із професійної підготовки здобувачів ІТ-спеціальностей.", "Моделювання процесів та SMART-систем" },
                    { new Guid("00000030-0000-0000-0000-000000000000"), "Навчальна дисципліна «Інтелектуальний аналіз даних» із професійної підготовки здобувачів ІТ-спеціальностей.", "Інтелектуальний аналіз даних" },
                    { new Guid("00000031-0000-0000-0000-000000000000"), "Навчальна дисципліна «Проектування інформаційних систем» із професійної підготовки здобувачів ІТ-спеціальностей.", "Проектування інформаційних систем" },
                    { new Guid("00000032-0000-0000-0000-000000000000"), "Навчальна дисципліна «Управління ІТ-проектами» із професійної підготовки здобувачів ІТ-спеціальностей.", "Управління ІТ-проектами" },
                    { new Guid("00000033-0000-0000-0000-000000000000"), "Навчальна дисципліна «CASE-технології» із професійної підготовки здобувачів ІТ-спеціальностей.", "CASE-технології" },
                    { new Guid("00000034-0000-0000-0000-000000000000"), "Навчальна дисципліна «Проектно-дослідницька практика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Проектно-дослідницька практика" },
                    { new Guid("00000035-0000-0000-0000-000000000000"), "Навчальна дисципліна «Вступ до комп'ютерної інженерії» із професійної підготовки здобувачів ІТ-спеціальностей.", "Вступ до комп'ютерної інженерії" },
                    { new Guid("00000036-0000-0000-0000-000000000000"), "Навчальна дисципліна «Комп'ютерна електроніка» із професійної підготовки здобувачів ІТ-спеціальностей.", "Комп'ютерна електроніка" },
                    { new Guid("00000037-0000-0000-0000-000000000000"), "Навчальна дисципліна «Комп'ютерна схемотехніка» із професійної підготовки здобувачів ІТ-спеціальностей.", "Комп'ютерна схемотехніка" },
                    { new Guid("00000038-0000-0000-0000-000000000000"), "Навчальна дисципліна «Прикладна теорія цифрових автоматів» із професійної підготовки здобувачів ІТ-спеціальностей.", "Прикладна теорія цифрових автоматів" },
                    { new Guid("00000039-0000-0000-0000-000000000000"), "Навчальна дисципліна «Курсова робота з комп'ютерної схемотехніки» із професійної підготовки здобувачів ІТ-спеціальностей.", "Курсова робота з комп'ютерної схемотехніки" },
                    { new Guid("0000003a-0000-0000-0000-000000000000"), "Навчальна дисципліна «Архітектура комп'ютерів» із професійної підготовки здобувачів ІТ-спеціальностей.", "Архітектура комп'ютерів" },
                    { new Guid("0000003b-0000-0000-0000-000000000000"), "Навчальна дисципліна «Системне програмування» із професійної підготовки здобувачів ІТ-спеціальностей.", "Системне програмування" },
                    { new Guid("0000003c-0000-0000-0000-000000000000"), "Навчальна дисципліна «Інструментальні засоби проектування та розробки сучасних електронних пристроїв» із професійної підготовки здобувачів ІТ-спеціальностей.", "Інструментальні засоби проектування та розробки сучасних електронних пристроїв" },
                    { new Guid("0000003d-0000-0000-0000-000000000000"), "Навчальна дисципліна «Сучасні технології FPGA розробки» із професійної підготовки здобувачів ІТ-спеціальностей.", "Сучасні технології FPGA розробки" },
                    { new Guid("0000003e-0000-0000-0000-000000000000"), "Навчальна дисципліна «Курсова робота з комп'ютерних мереж» із професійної підготовки здобувачів ІТ-спеціальностей.", "Курсова робота з комп'ютерних мереж" },
                    { new Guid("0000003f-0000-0000-0000-000000000000"), "Навчальна дисципліна «Хмарні технології» із професійної підготовки здобувачів ІТ-спеціальностей.", "Хмарні технології" },
                    { new Guid("00000040-0000-0000-0000-000000000000"), "Навчальна дисципліна «Проектування та розробка інтернет речей» із професійної підготовки здобувачів ІТ-спеціальностей.", "Проектування та розробка інтернет речей" },
                    { new Guid("00000041-0000-0000-0000-000000000000"), "Автоматизація збирання, розгортання та супроводу програмних систем у командній розробці.", "DevOps-практики" },
                    { new Guid("00000042-0000-0000-0000-000000000000"), "Навчальна дисципліна «Розробка мобільних застосунків» із професійної підготовки здобувачів ІТ-спеціальностей.", "Розробка мобільних застосунків" },
                    { new Guid("00000043-0000-0000-0000-000000000000"), "Навчальна дисципліна «Економіка та управління проектами» із професійної підготовки здобувачів ІТ-спеціальностей.", "Економіка та управління проектами" },
                    { new Guid("00000044-0000-0000-0000-000000000000"), "Навчальна дисципліна «Інженерія програмного забезпечення» із професійної підготовки здобувачів ІТ-спеціальностей.", "Інженерія програмного забезпечення" },
                    { new Guid("00000045-0000-0000-0000-000000000000"), "Навчальна дисципліна «Конструювання та програмування роботів» із професійної підготовки здобувачів ІТ-спеціальностей.", "Конструювання та програмування роботів" }
                });

            migrationBuilder.InsertData(
                table: "Institution",
                columns: new[] { "institution_id", "city", "country", "institution_name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Київ", "Україна", "Національний технічний університет України «КПІ імені Ігоря Сікорського»" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Львів", "Україна", "Львівська політехніка" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Харків", "Україна", "Харківський національний університет радіоелектроніки" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new DateOnly(2005, 1, 14), "student01@campus.ua", "Артем", "Коваль", "Сергійович", "+38067010001" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2005, 3, 22), "student02@campus.ua", "Марія", "Ткачук", "Олегівна", "+38067010002" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2005, 5, 8), "student03@campus.ua", "Владислав", "Мельник", "Ігорович", "+38067010003" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2005, 7, 3), "student04@campus.ua", "Софія", "Бондар", "Андріївна", "+38067010004" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new DateOnly(2005, 10, 27), "student05@campus.ua", "Дмитро", "Савчук", "Віталійович", "+38067010005" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new DateOnly(2005, 12, 11), "student06@campus.ua", "Анастасія", "Романюк", "Петрівна", "+38067010006" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new DateOnly(2006, 2, 4), "student07@campus.ua", "Богдан", "Кравчук", "Юрійович", "+38067010007" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new DateOnly(2006, 4, 12), "student08@campus.ua", "Катерина", "Поліщук", "Олександрівна", "+38067010008" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new DateOnly(2006, 6, 9), "student09@campus.ua", "Роман", "Шевчук", "Миколайович", "+38067010009" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new DateOnly(2006, 8, 28), "student10@campus.ua", "Дарина", "Лисенко", "Іванівна", "+38067010010" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new DateOnly(2006, 11, 5), "student11@campus.ua", "Максим", "Гончаренко", "Сергійович", "+38067010011" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new DateOnly(2005, 1, 17), "student12@campus.ua", "Вікторія", "Олійник", "Василівна", "+38067010012" },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new DateOnly(2005, 2, 26), "student13@campus.ua", "Назар", "Бойко", "Степанович", "+38067010013" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone", "status" },
                values: new object[] { new Guid("0000000e-0000-0000-0000-000000000000"), new DateOnly(2005, 4, 19), "student14@campus.ua", "Ірина", "Павлюк", "Андріївна", "+38067010014", "OnLeave" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[,]
                {
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new DateOnly(2005, 6, 30), "student15@campus.ua", "Денис", "Федорук", "Олегович", "+38067010015" },
                    { new Guid("00000010-0000-0000-0000-000000000000"), new DateOnly(2005, 9, 2), "student16@campus.ua", "Ольга", "Сорока", "Михайлівна", "+38067010016" },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new DateOnly(2005, 12, 14), "student17@campus.ua", "Ярослав", "Гуменюк", "Сергійович", "+38067010017" },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new DateOnly(2006, 1, 23), "student18@campus.ua", "Аліна", "Кузьменко", "Володимирівна", "+38067010018" },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new DateOnly(2006, 3, 5), "student19@campus.ua", "Андрій", "Дяченко", "Юрійович", "+38067010019" },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new DateOnly(2006, 5, 16), "student20@campus.ua", "Вероніка", "Левченко", "Ігорівна", "+38067010020" },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new DateOnly(2006, 7, 29), "student21@campus.ua", "Павло", "Мороз", "Романович", "+38067010021" },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new DateOnly(2006, 10, 7), "student22@campus.ua", "Христина", "Мазур", "Богданівна", "+38067010022" },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new DateOnly(2006, 1, 20), "student23@campus.ua", "Ілля", "Кушнір", "Сергійович", "+38067010023" },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new DateOnly(2006, 3, 31), "student24@campus.ua", "Юлія", "Власюк", "Андріївна", "+38067010024" },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new DateOnly(2006, 6, 21), "student25@campus.ua", "Тарас", "Олійник", "Миколайович", "+38067010025" },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new DateOnly(2006, 8, 10), "student26@campus.ua", "Діана", "Руденко", "Олексіївна", "+38067010026" },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), new DateOnly(2006, 11, 18), "student27@campus.ua", "Олександр", "Гнатюк", "Віталійович", "+38067010027" },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new DateOnly(2006, 2, 13), "student28@campus.ua", "Марта", "Черненко", "Ігорівна", "+38067010028" },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new DateOnly(2006, 4, 24), "student29@campus.ua", "Кирило", "Ковтун", "Олександрович", "+38067010029" },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), new DateOnly(2006, 7, 7), "student30@campus.ua", "Поліна", "Нагорна", "Сергіївна", "+38067010030" },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new DateOnly(2006, 9, 19), "student31@campus.ua", "Арсен", "Паламарчук", "Юрійович", "+38067010031" },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new DateOnly(2006, 12, 2), "student32@campus.ua", "Соломія", "Петренко", "Василівна", "+38067010032" }
                });

            migrationBuilder.InsertData(
                table: "Study_Plan",
                columns: new[] { "plan_id", "plan_name", "specialty_code", "valid_from" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Програмна інженерія 2025", "121", new DateOnly(2025, 9, 1) },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Комп'ютерні науки 2025", "122", new DateOnly(2025, 9, 1) },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Комп'ютерна інженерія 2025", "123", new DateOnly(2025, 9, 1) }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "department_id", "academic_unit_id", "name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Кафедра програмної інженерії" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Кафедра комп'ютерних наук" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Кафедра комп'ютерної інженерії" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Кафедра комп'ютерних технологій" }
                });

            migrationBuilder.InsertData(
                table: "External_Transfers",
                columns: new[] { "transfer_id", "institution_id", "notes", "student_id", "transfer_date", "transfer_type" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Переведення після завершення першого курсу в іншому закладі.", new Guid("0000001c-0000-0000-0000-000000000000"), new DateOnly(2025, 8, 25), "In" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Поновлення навчання після зміни місця проживання.", new Guid("0000000c-0000-0000-0000-000000000000"), new DateOnly(2024, 8, 28), "In" }
                });

            migrationBuilder.InsertData(
                table: "Plan_Disciplines",
                columns: new[] { "plan_discipline_id", "control_type", "credits", "discipline_id", "hours", "plan_id", "semester_no" },
                values: new object[,]
                {
                    { new Guid("000003e9-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000001-0000-0000-0000-000000000000"), 90, new Guid("00000001-0000-0000-0000-000000000000"), 1 },
                    { new Guid("000003ea-0000-0000-0000-000000000000"), "Exam", 11m, new Guid("00000002-0000-0000-0000-000000000000"), 330, new Guid("00000001-0000-0000-0000-000000000000"), 1 },
                    { new Guid("000003eb-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("00000003-0000-0000-0000-000000000000"), 180, new Guid("00000001-0000-0000-0000-000000000000"), 1 },
                    { new Guid("000003ec-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000004-0000-0000-0000-000000000000"), 150, new Guid("00000001-0000-0000-0000-000000000000"), 1 },
                    { new Guid("000003ed-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000005-0000-0000-0000-000000000000"), 90, new Guid("00000001-0000-0000-0000-000000000000"), 1 },
                    { new Guid("000003ee-0000-0000-0000-000000000000"), "Credit", 9m, new Guid("00000006-0000-0000-0000-000000000000"), 270, new Guid("00000001-0000-0000-0000-000000000000"), 2 },
                    { new Guid("000003ef-0000-0000-0000-000000000000"), "Exam", 9m, new Guid("00000007-0000-0000-0000-000000000000"), 270, new Guid("00000001-0000-0000-0000-000000000000"), 2 },
                    { new Guid("000003f0-0000-0000-0000-000000000000"), "Exam", 7m, new Guid("00000008-0000-0000-0000-000000000000"), 210, new Guid("00000001-0000-0000-0000-000000000000"), 2 },
                    { new Guid("000003f1-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000009-0000-0000-0000-000000000000"), 90, new Guid("00000001-0000-0000-0000-000000000000"), 2 },
                    { new Guid("000003f2-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("0000000a-0000-0000-0000-000000000000"), 90, new Guid("00000001-0000-0000-0000-000000000000"), 3 },
                    { new Guid("000003f3-0000-0000-0000-000000000000"), "Exam", 9m, new Guid("0000000b-0000-0000-0000-000000000000"), 270, new Guid("00000001-0000-0000-0000-000000000000"), 3 },
                    { new Guid("000003f4-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000000c-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 3 },
                    { new Guid("000003f5-0000-0000-0000-000000000000"), "Exam", 8m, new Guid("0000000d-0000-0000-0000-000000000000"), 240, new Guid("00000001-0000-0000-0000-000000000000"), 3 },
                    { new Guid("000003f6-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000000e-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 3 },
                    { new Guid("000003f7-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000000f-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 4 },
                    { new Guid("000003f8-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000010-0000-0000-0000-000000000000"), 90, new Guid("00000001-0000-0000-0000-000000000000"), 4 },
                    { new Guid("000003f9-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000011-0000-0000-0000-000000000000"), 180, new Guid("00000001-0000-0000-0000-000000000000"), 4 },
                    { new Guid("000003fa-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000012-0000-0000-0000-000000000000"), 150, new Guid("00000001-0000-0000-0000-000000000000"), 4 },
                    { new Guid("000003fb-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000013-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 4 },
                    { new Guid("000003fc-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000014-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 4 },
                    { new Guid("000003fd-0000-0000-0000-000000000000"), "Credit", 1m, new Guid("00000015-0000-0000-0000-000000000000"), 30, new Guid("00000001-0000-0000-0000-000000000000"), 4 },
                    { new Guid("000003fe-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000016-0000-0000-0000-000000000000"), 150, new Guid("00000001-0000-0000-0000-000000000000"), 5 },
                    { new Guid("000003ff-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000017-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000400-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000018-0000-0000-0000-000000000000"), 90, new Guid("00000001-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000401-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("00000019-0000-0000-0000-000000000000"), 180, new Guid("00000001-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000402-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000001a-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000403-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000001b-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000404-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("0000001c-0000-0000-0000-000000000000"), 90, new Guid("00000001-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000405-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000001d-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000406-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000001e-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000407-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000001f-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000408-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000020-0000-0000-0000-000000000000"), 120, new Guid("00000001-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000409-0000-0000-0000-000000000000"), "Credit", 12m, new Guid("00000021-0000-0000-0000-000000000000"), 360, new Guid("00000001-0000-0000-0000-000000000000"), 6 },
                    { new Guid("0000040a-0000-0000-0000-000000000000"), "Coursework", 6m, new Guid("00000022-0000-0000-0000-000000000000"), 180, new Guid("00000001-0000-0000-0000-000000000000"), 7 },
                    { new Guid("0000040b-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000001-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000040c-0000-0000-0000-000000000000"), "Exam", 11m, new Guid("00000002-0000-0000-0000-000000000000"), 330, new Guid("00000002-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000040d-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000023-0000-0000-0000-000000000000"), 180, new Guid("00000002-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000040e-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("00000003-0000-0000-0000-000000000000"), 180, new Guid("00000002-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000040f-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000004-0000-0000-0000-000000000000"), 150, new Guid("00000002-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000410-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000024-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000411-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000005-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000412-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000009-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000413-0000-0000-0000-000000000000"), "Exam", 9m, new Guid("00000007-0000-0000-0000-000000000000"), 270, new Guid("00000002-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000414-0000-0000-0000-000000000000"), "Exam", 7m, new Guid("00000008-0000-0000-0000-000000000000"), 210, new Guid("00000002-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000415-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000025-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000416-0000-0000-0000-000000000000"), "Exam", 9m, new Guid("0000000b-0000-0000-0000-000000000000"), 270, new Guid("00000002-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000417-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000000c-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000418-0000-0000-0000-000000000000"), "Exam", 8m, new Guid("0000000d-0000-0000-0000-000000000000"), 240, new Guid("00000002-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000419-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000000e-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000041a-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000026-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("0000041b-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000027-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("0000041c-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000028-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("0000041d-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000012-0000-0000-0000-000000000000"), 150, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("0000041e-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000029-0000-0000-0000-000000000000"), 180, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("0000041f-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000002a-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000420-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000013-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000421-0000-0000-0000-000000000000"), "Credit", 1m, new Guid("00000015-0000-0000-0000-000000000000"), 30, new Guid("00000002-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000422-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000002b-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000423-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("0000002c-0000-0000-0000-000000000000"), 150, new Guid("00000002-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000424-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000018-0000-0000-0000-000000000000"), 90, new Guid("00000002-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000425-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("0000002d-0000-0000-0000-000000000000"), 180, new Guid("00000002-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000426-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000002e-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000427-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000002f-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000428-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000030-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000429-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000031-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 6 },
                    { new Guid("0000042a-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000032-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 6 },
                    { new Guid("0000042b-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000033-0000-0000-0000-000000000000"), 120, new Guid("00000002-0000-0000-0000-000000000000"), 6 },
                    { new Guid("0000042c-0000-0000-0000-000000000000"), "Credit", 12m, new Guid("00000034-0000-0000-0000-000000000000"), 360, new Guid("00000002-0000-0000-0000-000000000000"), 6 },
                    { new Guid("0000042d-0000-0000-0000-000000000000"), "Coursework", 6m, new Guid("00000022-0000-0000-0000-000000000000"), 180, new Guid("00000002-0000-0000-0000-000000000000"), 6 },
                    { new Guid("0000042e-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000001-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000042f-0000-0000-0000-000000000000"), "Exam", 11m, new Guid("00000002-0000-0000-0000-000000000000"), 330, new Guid("00000003-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000430-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("00000006-0000-0000-0000-000000000000"), 180, new Guid("00000003-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000431-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("00000003-0000-0000-0000-000000000000"), 180, new Guid("00000003-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000432-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000004-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000433-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000005-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000434-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000009-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000435-0000-0000-0000-000000000000"), "Exam", 9m, new Guid("00000007-0000-0000-0000-000000000000"), 270, new Guid("00000003-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000436-0000-0000-0000-000000000000"), "Exam", 7m, new Guid("00000008-0000-0000-0000-000000000000"), 210, new Guid("00000003-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000437-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000035-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000438-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000036-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000439-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000037-0000-0000-0000-000000000000"), 150, new Guid("00000003-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000043a-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000000b-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000043b-0000-0000-0000-000000000000"), "Exam", 3m, new Guid("00000038-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000043c-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000039-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000043d-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("0000003a-0000-0000-0000-000000000000"), 180, new Guid("00000003-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000043e-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000003b-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000043f-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000000e-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000440-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000026-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000441-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000003c-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000442-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000029-0000-0000-0000-000000000000"), 180, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000443-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000003d-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000444-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000013-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000445-0000-0000-0000-000000000000"), "Credit", 1m, new Guid("00000015-0000-0000-0000-000000000000"), 30, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000446-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("0000002b-0000-0000-0000-000000000000"), 150, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000447-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("0000003e-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000448-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000003f-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000449-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("0000002d-0000-0000-0000-000000000000"), 180, new Guid("00000003-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000044a-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000002e-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000044b-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000040-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000044c-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000041-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000044d-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000042-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000044e-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000043-0000-0000-0000-000000000000"), 90, new Guid("00000003-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000044f-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000044-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000450-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000045-0000-0000-0000-000000000000"), 120, new Guid("00000003-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000451-0000-0000-0000-000000000000"), "Credit", 12m, new Guid("00000034-0000-0000-0000-000000000000"), 360, new Guid("00000003-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000452-0000-0000-0000-000000000000"), "Coursework", 6m, new Guid("00000022-0000-0000-0000-000000000000"), 180, new Guid("00000003-0000-0000-0000-000000000000"), 6 }
                });

            migrationBuilder.InsertData(
                table: "Study_Group",
                columns: new[] { "group_id", "date_closed", "date_created", "department_id", "group_code" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, new DateOnly(2024, 9, 1), new Guid("00000001-0000-0000-0000-000000000000"), "КС-24" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000001-0000-0000-0000-000000000000"), "КС-25" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), null, new DateOnly(2024, 9, 1), new Guid("00000002-0000-0000-0000-000000000000"), "КН-24" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000002-0000-0000-0000-000000000000"), "КН-25" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000004-0000-0000-0000-000000000000"), "КТ-25" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000003-0000-0000-0000-000000000000"), "КІ-25" }
                });

            migrationBuilder.InsertData(
                table: "Group_Plan_Assignment",
                columns: new[] { "group_plan_assignment_id", "date_from", "date_to", "group_id", "plan_id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Group_Enrollment",
                columns: new[] { "enrollment_id", "date_from", "date_to", "group_id", "reason_end", "reason_start", "student_id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), new DateOnly(2026, 2, 10), new Guid("00000001-0000-0000-0000-000000000000"), "Внутрішнє переведення", "Вступ", new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000006-0000-0000-0000-000000000000") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000007-0000-0000-0000-000000000000") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000008-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000009-0000-0000-0000-000000000000") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000a-0000-0000-0000-000000000000") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000b-0000-0000-0000-000000000000") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000c-0000-0000-0000-000000000000") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000d-0000-0000-0000-000000000000") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000000e-0000-0000-0000-000000000000") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), new DateOnly(2026, 2, 1), new Guid("00000003-0000-0000-0000-000000000000"), "Внутрішнє переведення", "Вступ", new Guid("0000000f-0000-0000-0000-000000000000") },
                    { new Guid("00000010-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000010-0000-0000-0000-000000000000") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000003-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000011-0000-0000-0000-000000000000") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000012-0000-0000-0000-000000000000") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000013-0000-0000-0000-000000000000") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000014-0000-0000-0000-000000000000") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000015-0000-0000-0000-000000000000") },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000016-0000-0000-0000-000000000000") },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000017-0000-0000-0000-000000000000") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000018-0000-0000-0000-000000000000") },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000019-0000-0000-0000-000000000000") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000001a-0000-0000-0000-000000000000") },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000001b-0000-0000-0000-000000000000") },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000001c-0000-0000-0000-000000000000") },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000001d-0000-0000-0000-000000000000") },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000001e-0000-0000-0000-000000000000") },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("0000001f-0000-0000-0000-000000000000") },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000020-0000-0000-0000-000000000000") },
                    { new Guid("00000065-0000-0000-0000-000000000000"), new DateOnly(2026, 2, 11), null, new Guid("00000004-0000-0000-0000-000000000000"), null, "Внутрішнє переведення", new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000066-0000-0000-0000-000000000000"), new DateOnly(2026, 2, 2), null, new Guid("00000006-0000-0000-0000-000000000000"), null, "Внутрішнє переведення", new Guid("0000000f-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Subgroup",
                columns: new[] { "subgroup_id", "group_id", "subgroup_name" },
                values: new object[,]
                {
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("0000002a-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000033-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000034-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("0000003d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("0000003e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), "Підгрупа 2" }
                });

            migrationBuilder.InsertData(
                table: "Academic_Leave",
                columns: new[] { "leave_id", "end_date", "enrollment_id", "reason", "return_reason", "start_date" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, new Guid("0000000e-0000-0000-0000-000000000000"), "Стан здоров'я", null, new DateOnly(2026, 2, 3) },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new DateOnly(2026, 3, 10), new Guid("00000009-0000-0000-0000-000000000000"), "Сімейні обставини", "Поновлено до навчання", new DateOnly(2026, 1, 20) }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000007d1-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d2-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d3-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d4-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d5-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d6-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d7-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d8-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007d9-0000-0000-0000-000000000000"), 2024, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007da-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007db-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007dc-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007dd-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007de-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007df-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f7-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007e0-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f8-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007e1-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f9-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007e2-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fa-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007e3-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fb-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007e4-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fc-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007e5-0000-0000-0000-000000000000"), 2025, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fd-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000007e6-0000-0000-0000-000000000000"), 2026, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fe-0000-0000-0000-000000000000") },
                    { new Guid("000007e7-0000-0000-0000-000000000000"), 2026, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ff-0000-0000-0000-000000000000") },
                    { new Guid("000007e8-0000-0000-0000-000000000000"), 2026, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000400-0000-0000-0000-000000000000") },
                    { new Guid("000007e9-0000-0000-0000-000000000000"), 2026, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000401-0000-0000-0000-000000000000") },
                    { new Guid("000007ea-0000-0000-0000-000000000000"), 2026, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000402-0000-0000-0000-000000000000") },
                    { new Guid("000007eb-0000-0000-0000-000000000000"), 2026, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000403-0000-0000-0000-000000000000") },
                    { new Guid("000007ec-0000-0000-0000-000000000000"), 2026, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000404-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000007ed-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007ee-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007ef-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f0-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f1-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f2-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f3-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f4-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f5-0000-0000-0000-000000000000"), 2024, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f6-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f7-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f8-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007f9-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007fa-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000007fb-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f7-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007fc-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f8-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007fd-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f9-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007fe-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fa-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000007ff-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fb-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000800-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fc-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000801-0000-0000-0000-000000000000"), 2025, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fd-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000802-0000-0000-0000-000000000000"), 2026, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fe-0000-0000-0000-000000000000") },
                    { new Guid("00000803-0000-0000-0000-000000000000"), 2026, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ff-0000-0000-0000-000000000000") },
                    { new Guid("00000804-0000-0000-0000-000000000000"), 2026, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000400-0000-0000-0000-000000000000") },
                    { new Guid("00000805-0000-0000-0000-000000000000"), 2026, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000401-0000-0000-0000-000000000000") },
                    { new Guid("00000806-0000-0000-0000-000000000000"), 2026, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000402-0000-0000-0000-000000000000") },
                    { new Guid("00000807-0000-0000-0000-000000000000"), 2026, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000403-0000-0000-0000-000000000000") },
                    { new Guid("00000808-0000-0000-0000-000000000000"), 2026, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000404-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000809-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000080a-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000080b-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000080c-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000080d-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000080e-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000080f-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000810-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000811-0000-0000-0000-000000000000"), 2024, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000812-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000813-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000814-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000815-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000816-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000817-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f7-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000818-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f8-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000819-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f9-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000081a-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fa-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000081b-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fb-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000081c-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fc-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000081d-0000-0000-0000-000000000000"), 2025, new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fd-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000081e-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000081f-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000820-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000821-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000822-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000823-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000824-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000825-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000826-0000-0000-0000-000000000000"), 2024, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000827-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000828-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000829-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000082a-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000082b-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000082c-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f7-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000082d-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f8-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000082e-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f9-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000082f-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fa-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000830-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fb-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000831-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fc-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000832-0000-0000-0000-000000000000"), 2025, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fd-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000833-0000-0000-0000-000000000000"), 2026, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fe-0000-0000-0000-000000000000") },
                    { new Guid("00000834-0000-0000-0000-000000000000"), 2026, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ff-0000-0000-0000-000000000000") },
                    { new Guid("00000835-0000-0000-0000-000000000000"), 2026, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000400-0000-0000-0000-000000000000") },
                    { new Guid("00000836-0000-0000-0000-000000000000"), 2026, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000401-0000-0000-0000-000000000000") },
                    { new Guid("00000837-0000-0000-0000-000000000000"), 2026, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000402-0000-0000-0000-000000000000") },
                    { new Guid("00000838-0000-0000-0000-000000000000"), 2026, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000403-0000-0000-0000-000000000000") },
                    { new Guid("00000839-0000-0000-0000-000000000000"), 2026, new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000404-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("0000083a-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000083b-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000083c-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000083d-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000083e-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000083f-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000840-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000841-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000842-0000-0000-0000-000000000000"), 2024, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000843-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000844-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000845-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000846-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000847-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000848-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f7-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000849-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f8-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000084a-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f9-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000084b-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fa-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000084c-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fb-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000084d-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fc-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000084e-0000-0000-0000-000000000000"), 2025, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fd-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000084f-0000-0000-0000-000000000000"), 2026, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fe-0000-0000-0000-000000000000") },
                    { new Guid("00000850-0000-0000-0000-000000000000"), 2026, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ff-0000-0000-0000-000000000000") },
                    { new Guid("00000851-0000-0000-0000-000000000000"), 2026, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000400-0000-0000-0000-000000000000") },
                    { new Guid("00000852-0000-0000-0000-000000000000"), 2026, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000401-0000-0000-0000-000000000000") },
                    { new Guid("00000853-0000-0000-0000-000000000000"), 2026, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000402-0000-0000-0000-000000000000") },
                    { new Guid("00000854-0000-0000-0000-000000000000"), 2026, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000403-0000-0000-0000-000000000000") },
                    { new Guid("00000855-0000-0000-0000-000000000000"), 2026, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000404-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000856-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000857-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000858-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000859-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000085a-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000085b-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000085c-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000085d-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000085e-0000-0000-0000-000000000000"), 2024, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000085f-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000860-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000861-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000862-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000863-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000864-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f7-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000865-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f8-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000866-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003f9-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000867-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fa-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000868-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fb-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000869-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fc-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000086a-0000-0000-0000-000000000000"), 2025, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fd-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000086b-0000-0000-0000-000000000000"), 2026, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003fe-0000-0000-0000-000000000000") },
                    { new Guid("0000086c-0000-0000-0000-000000000000"), 2026, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("000003ff-0000-0000-0000-000000000000") },
                    { new Guid("0000086d-0000-0000-0000-000000000000"), 2026, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000400-0000-0000-0000-000000000000") },
                    { new Guid("0000086e-0000-0000-0000-000000000000"), 2026, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000401-0000-0000-0000-000000000000") },
                    { new Guid("0000086f-0000-0000-0000-000000000000"), 2026, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000402-0000-0000-0000-000000000000") },
                    { new Guid("00000870-0000-0000-0000-000000000000"), 2026, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000403-0000-0000-0000-000000000000") },
                    { new Guid("00000871-0000-0000-0000-000000000000"), 2026, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000404-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000872-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000873-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000874-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000875-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000876-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000877-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000878-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000879-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000087a-0000-0000-0000-000000000000"), 2025, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000087b-0000-0000-0000-000000000000"), 2026, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000") },
                    { new Guid("0000087c-0000-0000-0000-000000000000"), 2026, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000") },
                    { new Guid("0000087d-0000-0000-0000-000000000000"), 2026, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000") },
                    { new Guid("0000087e-0000-0000-0000-000000000000"), 2026, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000") },
                    { new Guid("0000087f-0000-0000-0000-000000000000"), 2026, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000880-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000881-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000882-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000883-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000884-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000885-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000886-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000887-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000888-0000-0000-0000-000000000000"), 2025, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000889-0000-0000-0000-000000000000"), 2026, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000") },
                    { new Guid("0000088a-0000-0000-0000-000000000000"), 2026, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000") },
                    { new Guid("0000088b-0000-0000-0000-000000000000"), 2026, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000") },
                    { new Guid("0000088c-0000-0000-0000-000000000000"), 2026, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000") },
                    { new Guid("0000088d-0000-0000-0000-000000000000"), 2026, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("0000088e-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000088f-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000890-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000891-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000892-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000893-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000894-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000895-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000896-0000-0000-0000-000000000000"), 2025, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000897-0000-0000-0000-000000000000"), 2026, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000") },
                    { new Guid("00000898-0000-0000-0000-000000000000"), 2026, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000") },
                    { new Guid("00000899-0000-0000-0000-000000000000"), 2026, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000") },
                    { new Guid("0000089a-0000-0000-0000-000000000000"), 2026, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000") },
                    { new Guid("0000089b-0000-0000-0000-000000000000"), 2026, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("0000089c-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000089d-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000089e-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000089f-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008a0-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008a1-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008a2-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008a3-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008a4-0000-0000-0000-000000000000"), 2025, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000008a5-0000-0000-0000-000000000000"), 2026, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000") },
                    { new Guid("000008a6-0000-0000-0000-000000000000"), 2026, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000") },
                    { new Guid("000008a7-0000-0000-0000-000000000000"), 2026, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000") },
                    { new Guid("000008a8-0000-0000-0000-000000000000"), 2026, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000") },
                    { new Guid("000008a9-0000-0000-0000-000000000000"), 2026, new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000008aa-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003e9-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008ab-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ea-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008ac-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003eb-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008ad-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ec-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008ae-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ed-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008af-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ee-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008b0-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003ef-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008b1-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f0-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008b2-0000-0000-0000-000000000000"), 2025, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f1-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000008b3-0000-0000-0000-000000000000"), 2026, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f2-0000-0000-0000-000000000000") },
                    { new Guid("000008b4-0000-0000-0000-000000000000"), 2026, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f3-0000-0000-0000-000000000000") },
                    { new Guid("000008b5-0000-0000-0000-000000000000"), 2026, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f4-0000-0000-0000-000000000000") },
                    { new Guid("000008b6-0000-0000-0000-000000000000"), 2026, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f5-0000-0000-0000-000000000000") },
                    { new Guid("000008b7-0000-0000-0000-000000000000"), 2026, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), new Guid("000003f6-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000008b8-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008b9-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008ba-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008bb-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008bc-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008bd-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008be-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008bf-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c0-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c1-0000-0000-0000-000000000000"), 2024, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c2-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c3-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c4-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c5-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c6-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008c7-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008c8-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000420-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008c9-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000421-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008ca-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008cb-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008cc-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008cd-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008ce-0000-0000-0000-000000000000"), 2025, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000008cf-0000-0000-0000-000000000000"), 2026, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000424-0000-0000-0000-000000000000") },
                    { new Guid("000008d0-0000-0000-0000-000000000000"), 2026, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000422-0000-0000-0000-000000000000") },
                    { new Guid("000008d1-0000-0000-0000-000000000000"), 2026, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000423-0000-0000-0000-000000000000") },
                    { new Guid("000008d2-0000-0000-0000-000000000000"), 2026, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000425-0000-0000-0000-000000000000") },
                    { new Guid("000008d3-0000-0000-0000-000000000000"), 2026, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000426-0000-0000-0000-000000000000") },
                    { new Guid("000008d4-0000-0000-0000-000000000000"), 2026, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000427-0000-0000-0000-000000000000") },
                    { new Guid("000008d5-0000-0000-0000-000000000000"), 2026, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000428-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000008d6-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008d7-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008d8-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008d9-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008da-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008db-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008dc-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008dd-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008de-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008df-0000-0000-0000-000000000000"), 2024, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008e0-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008e1-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008e2-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008e3-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008e4-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008e5-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008e6-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000420-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008e7-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000421-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008e8-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008e9-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008ea-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008eb-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000008ec-0000-0000-0000-000000000000"), 2025, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000008ed-0000-0000-0000-000000000000"), 2026, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000424-0000-0000-0000-000000000000") },
                    { new Guid("000008ee-0000-0000-0000-000000000000"), 2026, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000422-0000-0000-0000-000000000000") },
                    { new Guid("000008ef-0000-0000-0000-000000000000"), 2026, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000423-0000-0000-0000-000000000000") },
                    { new Guid("000008f0-0000-0000-0000-000000000000"), 2026, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000425-0000-0000-0000-000000000000") },
                    { new Guid("000008f1-0000-0000-0000-000000000000"), 2026, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000426-0000-0000-0000-000000000000") },
                    { new Guid("000008f2-0000-0000-0000-000000000000"), 2026, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000427-0000-0000-0000-000000000000") },
                    { new Guid("000008f3-0000-0000-0000-000000000000"), 2026, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000428-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000008f4-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008f5-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008f6-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008f7-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008f8-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008f9-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008fa-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008fb-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008fc-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008fd-0000-0000-0000-000000000000"), 2024, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008fe-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000008ff-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000900-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000901-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000902-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000903-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000904-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000420-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000905-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000421-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000906-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000907-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000908-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000909-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000090a-0000-0000-0000-000000000000"), 2025, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000090b-0000-0000-0000-000000000000"), 2026, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000424-0000-0000-0000-000000000000") },
                    { new Guid("0000090c-0000-0000-0000-000000000000"), 2026, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000422-0000-0000-0000-000000000000") },
                    { new Guid("0000090d-0000-0000-0000-000000000000"), 2026, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000423-0000-0000-0000-000000000000") },
                    { new Guid("0000090e-0000-0000-0000-000000000000"), 2026, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000425-0000-0000-0000-000000000000") },
                    { new Guid("0000090f-0000-0000-0000-000000000000"), 2026, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000426-0000-0000-0000-000000000000") },
                    { new Guid("00000910-0000-0000-0000-000000000000"), 2026, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000427-0000-0000-0000-000000000000") },
                    { new Guid("00000911-0000-0000-0000-000000000000"), 2026, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000428-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000912-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000913-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000914-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000915-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000916-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000917-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000918-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000919-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000091a-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000091b-0000-0000-0000-000000000000"), 2024, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000091c-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000091d-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000091e-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000091f-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000920-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000921-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000922-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000420-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000923-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000421-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000924-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000925-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000926-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000927-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000928-0000-0000-0000-000000000000"), 2025, new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000929-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000092a-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000092b-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000092c-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000092d-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000092e-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000092f-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000930-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000931-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000932-0000-0000-0000-000000000000"), 2024, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000933-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000934-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000935-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000936-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000937-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000938-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000939-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000420-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000093a-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000421-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000093b-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000093c-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000093d-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000093e-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000093f-0000-0000-0000-000000000000"), 2025, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000940-0000-0000-0000-000000000000"), 2026, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000424-0000-0000-0000-000000000000") },
                    { new Guid("00000941-0000-0000-0000-000000000000"), 2026, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000422-0000-0000-0000-000000000000") },
                    { new Guid("00000942-0000-0000-0000-000000000000"), 2026, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000423-0000-0000-0000-000000000000") },
                    { new Guid("00000943-0000-0000-0000-000000000000"), 2026, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000425-0000-0000-0000-000000000000") },
                    { new Guid("00000944-0000-0000-0000-000000000000"), 2026, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000426-0000-0000-0000-000000000000") },
                    { new Guid("00000945-0000-0000-0000-000000000000"), 2026, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000427-0000-0000-0000-000000000000") },
                    { new Guid("00000946-0000-0000-0000-000000000000"), 2026, new Guid("00000010-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000428-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000947-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000948-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000949-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000094a-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000094b-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000094c-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000094d-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000094e-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("0000094f-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000950-0000-0000-0000-000000000000"), 2024, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000951-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000952-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000953-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000954-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000955-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000956-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000957-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000420-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000958-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000421-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000959-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000095a-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000095b-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000095c-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000095d-0000-0000-0000-000000000000"), 2025, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("0000041f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000095e-0000-0000-0000-000000000000"), 2026, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000424-0000-0000-0000-000000000000") },
                    { new Guid("0000095f-0000-0000-0000-000000000000"), 2026, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000422-0000-0000-0000-000000000000") },
                    { new Guid("00000960-0000-0000-0000-000000000000"), 2026, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000423-0000-0000-0000-000000000000") },
                    { new Guid("00000961-0000-0000-0000-000000000000"), 2026, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000425-0000-0000-0000-000000000000") },
                    { new Guid("00000962-0000-0000-0000-000000000000"), 2026, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000426-0000-0000-0000-000000000000") },
                    { new Guid("00000963-0000-0000-0000-000000000000"), 2026, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000427-0000-0000-0000-000000000000") },
                    { new Guid("00000964-0000-0000-0000-000000000000"), 2026, new Guid("00000011-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000428-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000965-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000966-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000967-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000968-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000969-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000096a-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000096b-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000096c-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000096d-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000096e-0000-0000-0000-000000000000"), 2025, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000096f-0000-0000-0000-000000000000"), 2026, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000") },
                    { new Guid("00000970-0000-0000-0000-000000000000"), 2026, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000") },
                    { new Guid("00000971-0000-0000-0000-000000000000"), 2026, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000") },
                    { new Guid("00000972-0000-0000-0000-000000000000"), 2026, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000") },
                    { new Guid("00000973-0000-0000-0000-000000000000"), 2026, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000974-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000975-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000976-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000977-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000978-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000979-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000097a-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000097b-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000097c-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000097d-0000-0000-0000-000000000000"), 2025, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000097e-0000-0000-0000-000000000000"), 2026, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000") },
                    { new Guid("0000097f-0000-0000-0000-000000000000"), 2026, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000") },
                    { new Guid("00000980-0000-0000-0000-000000000000"), 2026, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000") },
                    { new Guid("00000981-0000-0000-0000-000000000000"), 2026, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000") },
                    { new Guid("00000982-0000-0000-0000-000000000000"), 2026, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000983-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000984-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000985-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000986-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000987-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000988-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000989-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000098a-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000098b-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000098c-0000-0000-0000-000000000000"), 2025, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000098d-0000-0000-0000-000000000000"), 2026, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000") },
                    { new Guid("0000098e-0000-0000-0000-000000000000"), 2026, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000") },
                    { new Guid("0000098f-0000-0000-0000-000000000000"), 2026, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000") },
                    { new Guid("00000990-0000-0000-0000-000000000000"), 2026, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000") },
                    { new Guid("00000991-0000-0000-0000-000000000000"), 2026, new Guid("00000014-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000992-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000993-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000994-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000995-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000996-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000997-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000998-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000999-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000099a-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("0000099b-0000-0000-0000-000000000000"), 2025, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("0000099c-0000-0000-0000-000000000000"), 2026, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000") },
                    { new Guid("0000099d-0000-0000-0000-000000000000"), 2026, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000") },
                    { new Guid("0000099e-0000-0000-0000-000000000000"), 2026, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000") },
                    { new Guid("0000099f-0000-0000-0000-000000000000"), 2026, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000") },
                    { new Guid("000009a0-0000-0000-0000-000000000000"), 2026, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000009a1-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009a2-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009a3-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009a4-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040d-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009a5-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000040f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009a6-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000411-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009a7-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000413-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009a8-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000414-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009a9-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000412-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009aa-0000-0000-0000-000000000000"), 2025, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000009ab-0000-0000-0000-000000000000"), 2026, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000416-0000-0000-0000-000000000000") },
                    { new Guid("000009ac-0000-0000-0000-000000000000"), 2026, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000417-0000-0000-0000-000000000000") },
                    { new Guid("000009ad-0000-0000-0000-000000000000"), 2026, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000418-0000-0000-0000-000000000000") },
                    { new Guid("000009ae-0000-0000-0000-000000000000"), 2026, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000419-0000-0000-0000-000000000000") },
                    { new Guid("000009af-0000-0000-0000-000000000000"), 2026, new Guid("00000016-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000009b0-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009b1-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009b2-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009b3-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009b4-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009b5-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009b6-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009b7-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009b8-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009b9-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009ba-0000-0000-0000-000000000000"), 2025, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000009bb-0000-0000-0000-000000000000"), 2026, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("000009bc-0000-0000-0000-000000000000"), 2026, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("000009bd-0000-0000-0000-000000000000"), 2026, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("000009be-0000-0000-0000-000000000000"), 2026, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("000009bf-0000-0000-0000-000000000000"), 2026, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("000009c0-0000-0000-0000-000000000000"), 2026, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("000009c1-0000-0000-0000-000000000000"), 2026, new Guid("00000017-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000009c2-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009c3-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009c4-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009c5-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009c6-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009c7-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009c8-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009c9-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009ca-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009cb-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009cc-0000-0000-0000-000000000000"), 2025, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000009cd-0000-0000-0000-000000000000"), 2026, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("000009ce-0000-0000-0000-000000000000"), 2026, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("000009cf-0000-0000-0000-000000000000"), 2026, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("000009d0-0000-0000-0000-000000000000"), 2026, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("000009d1-0000-0000-0000-000000000000"), 2026, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("000009d2-0000-0000-0000-000000000000"), 2026, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("000009d3-0000-0000-0000-000000000000"), 2026, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000009d4-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009d5-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009d6-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009d7-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009d8-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009d9-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009da-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009db-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009dc-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009dd-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009de-0000-0000-0000-000000000000"), 2025, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000009df-0000-0000-0000-000000000000"), 2026, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("000009e0-0000-0000-0000-000000000000"), 2026, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("000009e1-0000-0000-0000-000000000000"), 2026, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("000009e2-0000-0000-0000-000000000000"), 2026, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("000009e3-0000-0000-0000-000000000000"), 2026, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("000009e4-0000-0000-0000-000000000000"), 2026, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("000009e5-0000-0000-0000-000000000000"), 2026, new Guid("00000019-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000009e6-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009e7-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009e8-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009e9-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009ea-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009eb-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009ec-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009ed-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009ee-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009ef-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009f0-0000-0000-0000-000000000000"), 2025, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("000009f1-0000-0000-0000-000000000000"), 2026, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("000009f2-0000-0000-0000-000000000000"), 2026, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("000009f3-0000-0000-0000-000000000000"), 2026, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("000009f4-0000-0000-0000-000000000000"), 2026, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("000009f5-0000-0000-0000-000000000000"), 2026, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("000009f6-0000-0000-0000-000000000000"), 2026, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("000009f7-0000-0000-0000-000000000000"), 2026, new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("000009f8-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009f9-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009fa-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009fb-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009fc-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("000009fd-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009fe-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("000009ff-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a00-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a01-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a02-0000-0000-0000-000000000000"), 2025, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a03-0000-0000-0000-000000000000"), 2026, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("00000a04-0000-0000-0000-000000000000"), 2026, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("00000a05-0000-0000-0000-000000000000"), 2026, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("00000a06-0000-0000-0000-000000000000"), 2026, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("00000a07-0000-0000-0000-000000000000"), 2026, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("00000a08-0000-0000-0000-000000000000"), 2026, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("00000a09-0000-0000-0000-000000000000"), 2026, new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a0a-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a0b-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a0c-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a0d-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a0e-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a0f-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a10-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a11-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a12-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a13-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a14-0000-0000-0000-000000000000"), 2025, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a15-0000-0000-0000-000000000000"), 2026, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("00000a16-0000-0000-0000-000000000000"), 2026, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("00000a17-0000-0000-0000-000000000000"), 2026, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("00000a18-0000-0000-0000-000000000000"), 2026, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("00000a19-0000-0000-0000-000000000000"), 2026, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("00000a1a-0000-0000-0000-000000000000"), 2026, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("00000a1b-0000-0000-0000-000000000000"), 2026, new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a1c-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a1d-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a1e-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a1f-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a20-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a21-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a22-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a23-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a24-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a25-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a26-0000-0000-0000-000000000000"), 2025, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a27-0000-0000-0000-000000000000"), 2026, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("00000a28-0000-0000-0000-000000000000"), 2026, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("00000a29-0000-0000-0000-000000000000"), 2026, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("00000a2a-0000-0000-0000-000000000000"), 2026, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("00000a2b-0000-0000-0000-000000000000"), 2026, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("00000a2c-0000-0000-0000-000000000000"), 2026, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("00000a2d-0000-0000-0000-000000000000"), 2026, new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a2e-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a2f-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a30-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a31-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a32-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a33-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a34-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a35-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a36-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a37-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a38-0000-0000-0000-000000000000"), 2025, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a39-0000-0000-0000-000000000000"), 2026, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("00000a3a-0000-0000-0000-000000000000"), 2026, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("00000a3b-0000-0000-0000-000000000000"), 2026, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("00000a3c-0000-0000-0000-000000000000"), 2026, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("00000a3d-0000-0000-0000-000000000000"), 2026, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("00000a3e-0000-0000-0000-000000000000"), 2026, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("00000a3f-0000-0000-0000-000000000000"), 2026, new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a40-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a41-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a42-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a43-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a44-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a45-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a46-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a47-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a48-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a49-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a4a-0000-0000-0000-000000000000"), 2025, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a4b-0000-0000-0000-000000000000"), 2026, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("00000a4c-0000-0000-0000-000000000000"), 2026, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("00000a4d-0000-0000-0000-000000000000"), 2026, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("00000a4e-0000-0000-0000-000000000000"), 2026, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("00000a4f-0000-0000-0000-000000000000"), 2026, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("00000a50-0000-0000-0000-000000000000"), 2026, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("00000a51-0000-0000-0000-000000000000"), 2026, new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a52-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042e-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a53-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000042f-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a54-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000431-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a55-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000432-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a56-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000430-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a57-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000433-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a58-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000435-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a59-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000436-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a5a-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000434-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a5b-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a5c-0000-0000-0000-000000000000"), 2025, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a5d-0000-0000-0000-000000000000"), 2026, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043a-0000-0000-0000-000000000000") },
                    { new Guid("00000a5e-0000-0000-0000-000000000000"), 2026, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043f-0000-0000-0000-000000000000") },
                    { new Guid("00000a5f-0000-0000-0000-000000000000"), 2026, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") },
                    { new Guid("00000a60-0000-0000-0000-000000000000"), 2026, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043b-0000-0000-0000-000000000000") },
                    { new Guid("00000a61-0000-0000-0000-000000000000"), 2026, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043c-0000-0000-0000-000000000000") },
                    { new Guid("00000a62-0000-0000-0000-000000000000"), 2026, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043d-0000-0000-0000-000000000000") },
                    { new Guid("00000a63-0000-0000-0000-000000000000"), 2026, new Guid("00000020-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("0000043e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000fa1-0000-0000-0000-000000000000"), 2025, new Guid("00000065-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000410-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000fa2-0000-0000-0000-000000000000"), 2025, new Guid("00000065-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("00000415-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[] { new Guid("00000fa3-0000-0000-0000-000000000000"), 2026, new Guid("00000065-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), new Guid("0000041a-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000fa4-0000-0000-0000-000000000000"), 2025, new Guid("00000066-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000437-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000fa5-0000-0000-0000-000000000000"), 2025, new Guid("00000066-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000438-0000-0000-0000-000000000000"), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[] { new Guid("00000fa6-0000-0000-0000-000000000000"), 2026, new Guid("00000066-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000439-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Student_Group_Transfer",
                columns: new[] { "transfer_id", "new_enrollment_id", "old_enrollment_id", "reason", "transfer_date" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000065-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Переведення на суміжну освітню програму.", new DateOnly(2026, 2, 11) },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000066-0000-0000-0000-000000000000"), new Guid("0000000f-0000-0000-0000-000000000000"), "Перехід до групи з поглибленою інженерною підготовкою.", new DateOnly(2026, 2, 2) }
                });

            migrationBuilder.InsertData(
                table: "Student_Subgroup_Enrollment",
                columns: new[] { "subgroup_enrollment_id", "date_from", "date_to", "enrollment_id", "reason", "subgroup_id" },
                values: new object[,]
                {
                    { new Guid("000003e9-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000001-0000-0000-0000-000000000000"), "Вступ", new Guid("0000000b-0000-0000-0000-000000000000") },
                    { new Guid("000003ea-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000002-0000-0000-0000-000000000000"), "Вступ", new Guid("0000000c-0000-0000-0000-000000000000") },
                    { new Guid("000003eb-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), new DateOnly(2026, 2, 10), new Guid("00000003-0000-0000-0000-000000000000"), "Вступ", new Guid("0000000b-0000-0000-0000-000000000000") },
                    { new Guid("000003ec-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000004-0000-0000-0000-000000000000"), "Вступ", new Guid("0000000c-0000-0000-0000-000000000000") },
                    { new Guid("000003ed-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000005-0000-0000-0000-000000000000"), "Вступ", new Guid("0000000b-0000-0000-0000-000000000000") },
                    { new Guid("000003ee-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000007-0000-0000-0000-000000000000"), "Вступ", new Guid("00000015-0000-0000-0000-000000000000") },
                    { new Guid("000003ef-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000008-0000-0000-0000-000000000000"), "Вступ", new Guid("00000016-0000-0000-0000-000000000000") },
                    { new Guid("000003f0-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000009-0000-0000-0000-000000000000"), "Вступ", new Guid("00000015-0000-0000-0000-000000000000") },
                    { new Guid("000003f1-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("0000000a-0000-0000-0000-000000000000"), "Вступ", new Guid("00000016-0000-0000-0000-000000000000") },
                    { new Guid("000003f2-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("0000000c-0000-0000-0000-000000000000"), "Вступ", new Guid("0000001f-0000-0000-0000-000000000000") },
                    { new Guid("000003f3-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("0000000d-0000-0000-0000-000000000000"), "Вступ", new Guid("00000020-0000-0000-0000-000000000000") },
                    { new Guid("000003f4-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("0000000e-0000-0000-0000-000000000000"), "Вступ", new Guid("0000001f-0000-0000-0000-000000000000") },
                    { new Guid("000003f5-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), new DateOnly(2026, 2, 1), new Guid("0000000f-0000-0000-0000-000000000000"), "Вступ", new Guid("00000020-0000-0000-0000-000000000000") },
                    { new Guid("000003f6-0000-0000-0000-000000000000"), new DateOnly(2024, 9, 1), null, new Guid("00000010-0000-0000-0000-000000000000"), "Вступ", new Guid("0000001f-0000-0000-0000-000000000000") },
                    { new Guid("000003f7-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000012-0000-0000-0000-000000000000"), "Вступ", new Guid("00000029-0000-0000-0000-000000000000") },
                    { new Guid("000003f8-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000013-0000-0000-0000-000000000000"), "Вступ", new Guid("0000002a-0000-0000-0000-000000000000") },
                    { new Guid("000003f9-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000014-0000-0000-0000-000000000000"), "Вступ", new Guid("00000029-0000-0000-0000-000000000000") },
                    { new Guid("000003fa-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000015-0000-0000-0000-000000000000"), "Вступ", new Guid("0000002a-0000-0000-0000-000000000000") },
                    { new Guid("000003fb-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000017-0000-0000-0000-000000000000"), "Вступ", new Guid("00000033-0000-0000-0000-000000000000") },
                    { new Guid("000003fc-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000018-0000-0000-0000-000000000000"), "Вступ", new Guid("00000034-0000-0000-0000-000000000000") },
                    { new Guid("000003fd-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000019-0000-0000-0000-000000000000"), "Вступ", new Guid("00000033-0000-0000-0000-000000000000") },
                    { new Guid("000003fe-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("0000001a-0000-0000-0000-000000000000"), "Вступ", new Guid("00000034-0000-0000-0000-000000000000") },
                    { new Guid("000003ff-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("0000001c-0000-0000-0000-000000000000"), "Вступ", new Guid("0000003d-0000-0000-0000-000000000000") },
                    { new Guid("00000400-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("0000001d-0000-0000-0000-000000000000"), "Вступ", new Guid("0000003e-0000-0000-0000-000000000000") },
                    { new Guid("00000401-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("0000001e-0000-0000-0000-000000000000"), "Вступ", new Guid("0000003d-0000-0000-0000-000000000000") },
                    { new Guid("00000402-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("0000001f-0000-0000-0000-000000000000"), "Вступ", new Guid("0000003e-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Academic_Difference_Item",
                columns: new[] { "difference_item_id", "notes", "plan_discipline_id", "transfer_id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Потрібно дозакрити дисципліну після внутрішнього переведення.", new Guid("00000410-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, new Guid("00000415-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), null, new Guid("00000437-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Academic_Difference_Item",
                columns: new[] { "difference_item_id", "notes", "plan_discipline_id", "status", "transfer_id" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Зараховано після проходження адаптаційного модуля.", new Guid("00000438-0000-0000-0000-000000000000"), "Completed", new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "Перезараховано за попередні результати навчання.", new Guid("00000439-0000-0000-0000-000000000000"), "Waived", new Guid("00000002-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Grade_Record",
                columns: new[] { "grade_id", "assessment_date", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { new Guid("00000bb9-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007d1-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000bba-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007d2-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000bbb-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007d3-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000bbc-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007d4-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000bbd-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007d5-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000bbe-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007d6-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000bbf-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007d7-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000bc0-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007d8-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000bc1-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007d9-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000bc2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007da-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000bc3-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007db-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000bc4-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007dc-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000bc5-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007dd-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000bc6-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007de-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000bc7-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007ed-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000bc8-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007ee-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000bc9-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007ef-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000bca-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007f0-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000bcb-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000007f1-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000bcc-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007f2-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000bcd-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007f3-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000bce-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007f4-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000bcf-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000007f5-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000bd0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007f6-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000bd1-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007f7-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000bd2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007f8-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000bd3-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007f9-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000bd4-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000007fa-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000bd5-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000809-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000bd6-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000080a-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000bd7-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000080b-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000bd8-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000080c-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000bd9-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000080d-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000bda-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000080e-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000bdb-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000080f-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000bdc-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000810-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000bdd-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000811-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000bde-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000812-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000bdf-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000813-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000be0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000814-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000be1-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000815-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000be2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000816-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000be3-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000081e-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000be4-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000081f-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000be5-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000820-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000be6-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000821-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000be7-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000822-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000be8-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000823-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000be9-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000824-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000bea-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000825-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000beb-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000826-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000bec-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000827-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000bed-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000828-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000bee-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000829-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000bef-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000082a-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000bf0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000082b-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000bf1-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000083a-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000bf2-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000083b-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000bf3-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000083c-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000bf4-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000083d-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000bf5-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000083e-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000bf6-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000083f-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000bf7-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000840-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000bf8-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000841-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000bf9-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000842-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000bfa-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000843-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000bfb-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000844-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000bfc-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000845-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000bfd-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000846-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000bfe-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000847-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000bff-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000856-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000c00-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000857-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000c01-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000858-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c02-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000859-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c03-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000085a-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c04-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000085b-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c05-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000085c-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000c06-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000085d-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000c07-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000085e-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c08-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000085f-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000c09-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000860-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c0a-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000861-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000c0b-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000862-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c0c-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000863-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c0d-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000872-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c0e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000873-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c0f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000874-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c10-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000875-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000c11-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000876-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000c12-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000880-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c13-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000881-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000c14-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000882-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c15-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000883-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000c16-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000884-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c17-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000088e-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000c18-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000088f-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c19-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000890-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000c1a-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000891-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000c1b-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000892-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c1c-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000089c-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000c1d-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000089d-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c1e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000089e-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000c1f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000089f-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000c20-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008a0-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c21-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008aa-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c22-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008ab-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000c23-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008ac-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c24-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008ad-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000c25-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008ae-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000c26-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008b8-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c27-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008b9-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000c28-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008ba-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000c29-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008bb-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c2a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008bc-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000c2b-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008bd-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000c2c-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008be-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c2d-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008bf-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c2e-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008c0-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c2f-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008c1-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c30-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008c2-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000c31-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008c3-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c32-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008c4-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c33-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008c5-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000c34-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008c6-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000c35-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008d6-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000c36-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008d7-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c37-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008d8-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c38-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008d9-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000c39-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008da-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c3a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008db-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000c3b-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008dc-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c3c-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008dd-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000c3d-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008de-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000c3e-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008df-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000c3f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008e0-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c40-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008e1-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000c41-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008e2-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c42-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008e3-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c43-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008e4-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000c44-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008f4-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c45-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008f5-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c46-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008f6-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c47-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("000008f7-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c48-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008f8-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c49-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008f9-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c4a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008fa-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000c4b-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008fb-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000c4c-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008fc-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000c4d-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("000008fd-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000c4e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008fe-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c4f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000008ff-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000c50-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000900-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000c51-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000901-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000c52-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000902-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000c53-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000912-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c54-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000913-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000c55-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000914-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000c56-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000915-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c57-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000916-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000c58-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000917-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000c59-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000918-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000c5a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000919-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000c5b-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000091a-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000c5c-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000091b-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c5d-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000091c-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c5e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000091d-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000c5f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000091e-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c60-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("0000091f-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000c61-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000920-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000c62-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000929-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000c63-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000092a-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c64-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000092b-0000-0000-0000-000000000000"), "80" },
                    { new Guid("00000c65-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000092c-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000c66-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000092d-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000c67-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000092e-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000c68-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000092f-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c69-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000930-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000c6a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000931-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c6b-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000932-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000c6c-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000933-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c6d-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000934-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000c6e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000935-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c6f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000936-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000c70-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000937-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000c71-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000947-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000c72-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000948-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c73-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("00000949-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000c74-0000-0000-0000-000000000000"), new DateOnly(2025, 1, 20), new Guid("0000094a-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c75-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000094b-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000c76-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000094c-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c77-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000094d-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c78-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000094e-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000c79-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("0000094f-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c7a-0000-0000-0000-000000000000"), new DateOnly(2025, 6, 20), new Guid("00000950-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000c7b-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000951-0000-0000-0000-000000000000"), "84" },
                    { new Guid("00000c7c-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000952-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000c7d-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000953-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000c7e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000954-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c7f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000955-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c80-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000965-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000c81-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000966-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000c82-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000967-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000c83-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000968-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c84-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000974-0000-0000-0000-000000000000"), "77" },
                    { new Guid("00000c85-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000975-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000c86-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000976-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c87-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000977-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000c88-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000983-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c89-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000984-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000c8a-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000985-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000c8b-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000986-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000c8c-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000992-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c8d-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000993-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000c8e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000994-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c8f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000995-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c90-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009a1-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c91-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009a2-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c92-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009a3-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c93-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009a4-0000-0000-0000-000000000000"), "86" },
                    { new Guid("00000c94-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009b0-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c95-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009b1-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000c96-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009b2-0000-0000-0000-000000000000"), "78" },
                    { new Guid("00000c97-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009b3-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000c98-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009b4-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000c99-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009c2-0000-0000-0000-000000000000"), "91" },
                    { new Guid("00000c9a-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009c3-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000c9b-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009c4-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000c9c-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009c5-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c9d-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009c6-0000-0000-0000-000000000000"), "92" },
                    { new Guid("00000c9e-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009d4-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000c9f-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009d5-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000ca0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009d6-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000ca1-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009d7-0000-0000-0000-000000000000"), "85" },
                    { new Guid("00000ca2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009d8-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000ca3-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009e6-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000ca4-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009e7-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000ca5-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009e8-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000ca6-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009e9-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000ca7-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009ea-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000ca8-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009f8-0000-0000-0000-000000000000"), "88" },
                    { new Guid("00000ca9-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009f9-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000caa-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009fa-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000cab-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009fb-0000-0000-0000-000000000000"), "74" },
                    { new Guid("00000cac-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("000009fc-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000cad-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a0a-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000cae-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a0b-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000caf-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a0c-0000-0000-0000-000000000000"), "89" },
                    { new Guid("00000cb0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a0d-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000cb1-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a0e-0000-0000-0000-000000000000"), "73" },
                    { new Guid("00000cb2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a1c-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000cb3-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a1d-0000-0000-0000-000000000000"), "95" },
                    { new Guid("00000cb4-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a1e-0000-0000-0000-000000000000"), "76" },
                    { new Guid("00000cb5-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a1f-0000-0000-0000-000000000000"), "94" },
                    { new Guid("00000cb6-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a20-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000cb7-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a2e-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000cb8-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a2f-0000-0000-0000-000000000000"), "93" },
                    { new Guid("00000cb9-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a30-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000cba-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a31-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000cbb-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a32-0000-0000-0000-000000000000"), "72" },
                    { new Guid("00000cbc-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a40-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000cbd-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a41-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000cbe-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a42-0000-0000-0000-000000000000"), "75" },
                    { new Guid("00000cbf-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a43-0000-0000-0000-000000000000"), "82" },
                    { new Guid("00000cc0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a44-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00000cc1-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a52-0000-0000-0000-000000000000"), "87" },
                    { new Guid("00000cc2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a53-0000-0000-0000-000000000000"), "79" },
                    { new Guid("00000cc3-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a54-0000-0000-0000-000000000000"), "83" },
                    { new Guid("00000cc4-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a55-0000-0000-0000-000000000000"), "81" },
                    { new Guid("00000cc5-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a56-0000-0000-0000-000000000000"), "90" },
                    { new Guid("00001389-0000-0000-0000-000000000000"), new DateOnly(2026, 4, 15), new Guid("00000fa5-0000-0000-0000-000000000000"), "90" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Difference_Item_plan_discipline_id",
                table: "Academic_Difference_Item",
                column: "plan_discipline_id");

            migrationBuilder.CreateIndex(
                name: "UX_AcademicDifferenceItem_TransferId_PlanDisciplineId",
                table: "Academic_Difference_Item",
                columns: new[] { "transfer_id", "plan_discipline_id" },
                unique: true);

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
                name: "IX_Plan_Disciplines_plan_id_discipline_id",
                table: "Plan_Disciplines",
                columns: new[] { "plan_id", "discipline_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_enrollment_id",
                table: "Student_Course_Enrollment",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_group_plan_assignment_id",
                table: "Student_Course_Enrollment",
                column: "group_plan_assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Enrollment_plan_discipline_id",
                table: "Student_Course_Enrollment",
                column: "plan_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_GroupId_DateFrom",
                table: "Student_Group_Enrollment",
                columns: new[] { "group_id", "date_from" });

            migrationBuilder.CreateIndex(
                name: "IX_Student_Group_Enrollment_student_id",
                table: "Student_Group_Enrollment",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupTransfer_NewEnrollmentId",
                table: "Student_Group_Transfer",
                column: "new_enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupTransfer_OldEnrollmentId",
                table: "Student_Group_Transfer",
                column: "old_enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubgroupEnrollment_EnrollmentId_DateFrom",
                table: "Student_Subgroup_Enrollment",
                columns: new[] { "enrollment_id", "date_from" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubgroupEnrollment_SubgroupId",
                table: "Student_Subgroup_Enrollment",
                column: "subgroup_id");

            migrationBuilder.CreateIndex(
                name: "UX_StudentSubgroupEnrollment_Open",
                table: "Student_Subgroup_Enrollment",
                column: "enrollment_id",
                unique: true,
                filter: "[date_to] IS NULL");

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

            migrationBuilder.Sql(StudentTimelineViewSql.Drop);
            migrationBuilder.Sql(StudentTimelineViewSql.Create);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(StudentTimelineViewSql.Drop);

            migrationBuilder.DropTable(
                name: "Academic_Difference_Item");

            migrationBuilder.DropTable(
                name: "Academic_Leave");

            migrationBuilder.DropTable(
                name: "External_Transfers");

            migrationBuilder.DropTable(
                name: "Grade_Record");

            migrationBuilder.DropTable(
                name: "Student_Subgroup_Enrollment");

            migrationBuilder.DropTable(
                name: "Student_Group_Transfer");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "Student_Course_Enrollment");

            migrationBuilder.DropTable(
                name: "Subgroup");

            migrationBuilder.DropTable(
                name: "Group_Plan_Assignment");

            migrationBuilder.DropTable(
                name: "Plan_Disciplines");

            migrationBuilder.DropTable(
                name: "Student_Group_Enrollment");

            migrationBuilder.DropTable(
                name: "Discipline");

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
