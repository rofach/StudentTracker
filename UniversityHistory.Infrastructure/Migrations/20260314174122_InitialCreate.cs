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
                    creation_year = table.Column<int>(type: "int", nullable: false),
                    faculty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                    total_credits = table.Column<int>(type: "int", nullable: false),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study_Plan", x => x.plan_id);
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
                name: "Student_Group_Enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    subgroup_id = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Student_Group_Enrollment_Subgroup_subgroup_id",
                        column: x => x.subgroup_id,
                        principalTable: "Subgroup",
                        principalColumn: "subgroup_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Student_Course_Enrollment",
                columns: table => new
                {
                    course_enrollment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assignment_id = table.Column<int>(type: "int", nullable: false),
                    discipline_id = table.Column<int>(type: "int", nullable: false),
                    semester_no = table.Column<int>(type: "int", nullable: false),
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
                name: "Academic_Leave",
                columns: table => new
                {
                    leave_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Academic_Leave_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "student_id");
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
                    enrollment_id = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_External_Transfers_Student_Group_Enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_External_Transfers_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "student_id");
                });

            migrationBuilder.CreateTable(
                name: "Grade_Record",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_enrollment_id = table.Column<int>(type: "int", nullable: false),
                    attempt_no = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
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
                    { 1, "Calculus I" },
                    { 2, "Introduction to Computer Science" },
                    { 3, "Data Structures" },
                    { 4, "Linear Algebra" }
                });

            migrationBuilder.InsertData(
                table: "Institution",
                columns: new[] { "institution_id", "city", "country", "institution_name" },
                values: new object[,]
                {
                    { 1, "Cambridge", "USA", "MIT" },
                    { 2, "Stanford", "USA", "Stanford University" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[] { 1, new DateOnly(2003, 5, 14), "alice@example.com", "Alice", "Smith", "555-0101" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[,]
                {
                    { 2, new DateOnly(2002, 11, 2), "bob@example.com", "Bob", "Johnson", "555-0102", "Graduated" },
                    { 3, new DateOnly(2004, 8, 22), "charlie@example.com", "Charlie", "Williams", "555-0103", "OnLeave" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[] { 4, new DateOnly(2003, 1, 30), "diana@example.com", "Diana", "Brown", "555-0104" });

            migrationBuilder.InsertData(
                table: "Study_Group",
                columns: new[] { "group_id", "creation_year", "faculty", "group_code" },
                values: new object[,]
                {
                    { 1, 2021, "Computer Science", "CS-21" },
                    { 2, 2022, "Computer Science", "CS-22" },
                    { 3, 2023, "Computer Science", "CS-23" }
                });

            migrationBuilder.InsertData(
                table: "Study_Plan",
                columns: new[] { "plan_id", "plan_name", "specialty_code", "total_credits", "valid_from" },
                values: new object[,]
                {
                    { 1, "BSc Computer Science v1", "CS-101", 240, new DateOnly(2021, 9, 1) },
                    { 2, "BSc Computer Science v2", "CS-102", 240, new DateOnly(2023, 9, 1) }
                });

            migrationBuilder.InsertData(
                table: "Plan_Disciplines",
                columns: new[] { "discipline_id", "plan_id", "control_type", "credits", "hours", "semester_no" },
                values: new object[,]
                {
                    { 1, 1, "Exam", 4.0m, 120, 1 },
                    { 2, 1, "Exam", 5.0m, 150, 1 },
                    { 3, 1, "Coursework", 6.0m, 180, 2 },
                    { 1, 2, "Exam", 4.0m, 120, 1 },
                    { 2, 2, "Exam", 5.0m, 150, 1 },
                    { 4, 2, "Exam", 4.0m, 120, 2 }
                });

            migrationBuilder.InsertData(
                table: "Student_Group_Enrollment",
                columns: new[] { "enrollment_id", "date_from", "date_to", "group_id", "reason_end", "reason_start", "student_id", "subgroup_id" },
                values: new object[,]
                {
                    { 4, new DateOnly(2022, 9, 1), new DateOnly(2023, 1, 15), 2, "Academic Leave", "Admission", 3, null },
                    { 5, new DateOnly(2023, 9, 1), null, 3, null, "Admission", 4, null }
                });

            migrationBuilder.InsertData(
                table: "Student_Plan_Assignment",
                columns: new[] { "assignment_id", "date_from", "date_to", "plan_id", "student_id" },
                values: new object[,]
                {
                    { 1, new DateOnly(2021, 9, 1), null, 1, 1 },
                    { 2, new DateOnly(2021, 9, 1), new DateOnly(2024, 6, 30), 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Subgroup",
                columns: new[] { "subgroup_id", "group_id", "subgroup_name" },
                values: new object[,]
                {
                    { 1, 1, "Subgroup A" },
                    { 2, 1, "Subgroup B" },
                    { 3, 2, "Subgroup A" }
                });

            migrationBuilder.InsertData(
                table: "Academic_Leave",
                columns: new[] { "leave_id", "end_date", "enrollment_id", "reason", "start_date", "student_id" },
                values: new object[] { 1, null, 4, "Medical leave", new DateOnly(2023, 1, 16), 3 });

            migrationBuilder.InsertData(
                table: "External_Transfers",
                columns: new[] { "transfer_id", "enrollment_id", "institution_id", "notes", "student_id", "transfer_date", "transfer_type" },
                values: new object[] { 1, 5, 2, "Completed first year at Stanford", 4, new DateOnly(2023, 8, 25), "In" });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "assignment_id", "discipline_id", "semester_no", "status" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "Completed" },
                    { 2, 1, 2, 1, "Completed" },
                    { 3, 2, 1, 1, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Student_Group_Enrollment",
                columns: new[] { "enrollment_id", "date_from", "date_to", "group_id", "reason_end", "reason_start", "student_id", "subgroup_id" },
                values: new object[,]
                {
                    { 1, new DateOnly(2021, 9, 1), new DateOnly(2022, 6, 30), 1, "Transfer to new group", "Admission", 1, 1 },
                    { 2, new DateOnly(2022, 9, 1), null, 2, null, "Transfer", 1, 3 },
                    { 3, new DateOnly(2021, 9, 1), new DateOnly(2024, 6, 30), 1, "Graduation", "Admission", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Grade_Record",
                columns: new[] { "grade_id", "assessment_date", "attempt_no", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { 1, new DateOnly(2022, 1, 15), 1, 1, "A" },
                    { 2, new DateOnly(2022, 1, 18), 1, 2, "B+" },
                    { 3, new DateOnly(2022, 1, 15), 1, 3, "A-" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Leave_enrollment_id",
                table: "Academic_Leave",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Leave_student_id",
                table: "Academic_Leave",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_External_Transfers_enrollment_id",
                table: "External_Transfers",
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
                name: "IX_Grade_Record_course_enrollment_id_attempt_no",
                table: "Grade_Record",
                columns: new[] { "course_enrollment_id", "attempt_no" },
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
                name: "IX_Student_Group_Enrollment_subgroup_id",
                table: "Student_Group_Enrollment",
                column: "subgroup_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Plan_Assignment_plan_id",
                table: "Student_Plan_Assignment",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Plan_Assignment_student_id",
                table: "Student_Plan_Assignment",
                column: "student_id");

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
                name: "Institution");

            migrationBuilder.DropTable(
                name: "Student_Group_Enrollment");

            migrationBuilder.DropTable(
                name: "Student_Course_Enrollment");

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
