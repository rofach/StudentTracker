using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeTo3NF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Academic_Leave_Student_student_id",
                table: "Academic_Leave");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Group_Enrollment_Subgroup_subgroup_id",
                table: "Student_Group_Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Student_Group_Enrollment_subgroup_id",
                table: "Student_Group_Enrollment");

            migrationBuilder.DropColumn(
                name: "total_credits",
                table: "Study_Plan");

            migrationBuilder.DropColumn(
                name: "creation_year",
                table: "Study_Group");

            migrationBuilder.DropColumn(
                name: "subgroup_id",
                table: "Student_Group_Enrollment");

            migrationBuilder.DropColumn(
                name: "semester_no",
                table: "Student_Course_Enrollment");

            migrationBuilder.RenameColumn(
                name: "student_id",
                table: "Academic_Leave",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Academic_Leave_student_id",
                table: "Academic_Leave",
                newName: "IX_Academic_Leave_StudentId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Academic_Leave",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.UpdateData(
                table: "Academic_Leave",
                keyColumn: "leave_id",
                keyValue: 1,
                column: "StudentId",
                value: null);

            migrationBuilder.InsertData(
                table: "Student_Subgroup_Assignment",
                columns: new[] { "enrollment_id", "subgroup_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_Subgroup_Assignment_subgroup_id",
                table: "Student_Subgroup_Assignment",
                column: "subgroup_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Academic_Leave_Student_StudentId",
                table: "Academic_Leave",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Academic_Leave_Student_StudentId",
                table: "Academic_Leave");

            migrationBuilder.DropTable(
                name: "Student_Subgroup_Assignment");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Academic_Leave",
                newName: "student_id");

            migrationBuilder.RenameIndex(
                name: "IX_Academic_Leave_StudentId",
                table: "Academic_Leave",
                newName: "IX_Academic_Leave_student_id");

            migrationBuilder.AddColumn<int>(
                name: "total_credits",
                table: "Study_Plan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "creation_year",
                table: "Study_Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "subgroup_id",
                table: "Student_Group_Enrollment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "semester_no",
                table: "Student_Course_Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "student_id",
                table: "Academic_Leave",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Academic_Leave",
                keyColumn: "leave_id",
                keyValue: 1,
                column: "student_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 1,
                column: "semester_no",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 2,
                column: "semester_no",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 3,
                column: "semester_no",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 1,
                column: "subgroup_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 2,
                column: "subgroup_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 3,
                column: "subgroup_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 4,
                column: "subgroup_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 5,
                column: "subgroup_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 1,
                column: "creation_year",
                value: 2021);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 2,
                column: "creation_year",
                value: 2022);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 3,
                column: "creation_year",
                value: 2023);

            migrationBuilder.UpdateData(
                table: "Study_Plan",
                keyColumn: "plan_id",
                keyValue: 1,
                column: "total_credits",
                value: 240);

            migrationBuilder.UpdateData(
                table: "Study_Plan",
                keyColumn: "plan_id",
                keyValue: 2,
                column: "total_credits",
                value: 240);

            migrationBuilder.CreateIndex(
                name: "IX_Student_Group_Enrollment_subgroup_id",
                table: "Student_Group_Enrollment",
                column: "subgroup_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Academic_Leave_Student_student_id",
                table: "Academic_Leave",
                column: "student_id",
                principalTable: "Student",
                principalColumn: "student_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Group_Enrollment_Subgroup_subgroup_id",
                table: "Student_Group_Enrollment",
                column: "subgroup_id",
                principalTable: "Subgroup",
                principalColumn: "subgroup_id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
