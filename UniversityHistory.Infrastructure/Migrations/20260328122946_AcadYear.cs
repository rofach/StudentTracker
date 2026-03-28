using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AcadYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "academic_year_start",
                table: "Student_Course_Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 1,
                column: "academic_year_start",
                value: 2021);

            migrationBuilder.UpdateData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 2,
                column: "academic_year_start",
                value: 2021);

            migrationBuilder.UpdateData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 3,
                column: "academic_year_start",
                value: 2021);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "academic_year_start",
                table: "Student_Course_Enrollment");
        }
    }
}
