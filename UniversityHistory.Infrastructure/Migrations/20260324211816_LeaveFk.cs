using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeaveFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Academic_Leave_Student_StudentId",
                table: "Academic_Leave");

            migrationBuilder.DropIndex(
                name: "IX_Academic_Leave_StudentId",
                table: "Academic_Leave");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Academic_Leave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Academic_Leave",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Academic_Leave",
                keyColumn: "leave_id",
                keyValue: 1,
                column: "StudentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Leave_StudentId",
                table: "Academic_Leave",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Academic_Leave_Student_StudentId",
                table: "Academic_Leave",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "student_id");
        }
    }
}
