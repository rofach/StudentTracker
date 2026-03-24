using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Grades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grade_Record_course_enrollment_id_attempt_no",
                table: "Grade_Record");

            migrationBuilder.DropColumn(
                name: "attempt_no",
                table: "Grade_Record");

            migrationBuilder.UpdateData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 1,
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 2,
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 3,
                column: "grade_value",
                value: "91");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Record_course_enrollment_id",
                table: "Grade_Record",
                column: "course_enrollment_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grade_Record_course_enrollment_id",
                table: "Grade_Record");

            migrationBuilder.AddColumn<int>(
                name: "attempt_no",
                table: "Grade_Record",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 1,
                columns: new[] { "attempt_no", "grade_value" },
                values: new object[] { 1, "A" });

            migrationBuilder.UpdateData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 2,
                columns: new[] { "attempt_no", "grade_value" },
                values: new object[] { 1, "B+" });

            migrationBuilder.UpdateData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 3,
                columns: new[] { "attempt_no", "grade_value" },
                values: new object[] { 1, "A-" });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Record_course_enrollment_id_attempt_no",
                table: "Grade_Record",
                columns: new[] { "course_enrollment_id", "attempt_no" },
                unique: true);
        }
    }
}
