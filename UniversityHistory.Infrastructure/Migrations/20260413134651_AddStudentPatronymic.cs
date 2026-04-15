using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentPatronymic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                table: "Student",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 1,
                column: "patronymic",
                value: "Олександрович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 2,
                column: "patronymic",
                value: "Ігорівна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 3,
                column: "patronymic",
                value: "Сергійович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 4,
                column: "patronymic",
                value: "Василівна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 5,
                column: "patronymic",
                value: "Романович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 6,
                column: "patronymic",
                value: "Андріївна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 7,
                column: "patronymic",
                value: "Олегович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 8,
                column: "patronymic",
                value: "Вікторівна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 9,
                column: "patronymic",
                value: "Миколайович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 10,
                column: "patronymic",
                value: "Петрівна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 11,
                column: "patronymic",
                value: "Володимирович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 12,
                column: "patronymic",
                value: "Юріївна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 13,
                column: "patronymic",
                value: "Степанович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 14,
                column: "patronymic",
                value: "Павлівна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 15,
                column: "patronymic",
                value: "Олексійович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 16,
                column: "patronymic",
                value: "Іванівна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 17,
                column: "patronymic",
                value: "Михайлович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 18,
                column: "patronymic",
                value: "Богданівна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 19,
                column: "patronymic",
                value: "Дмитрович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 20,
                column: "patronymic",
                value: "Сергіївна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 21,
                column: "patronymic",
                value: "Віталійович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 22,
                column: "patronymic",
                value: "Анатоліївна");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 23,
                column: "patronymic",
                value: "Романович");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 24,
                column: "patronymic",
                value: "Миколаївна");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "patronymic",
                table: "Student");
        }
    }
}
