using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Transfer3NFUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_External_Transfers_Student_Group_Enrollment_enrollment_id",
                table: "External_Transfers");

            migrationBuilder.DropIndex(
                name: "IX_External_Transfers_enrollment_id",
                table: "External_Transfers");

            migrationBuilder.DropColumn(
                name: "enrollment_id",
                table: "External_Transfers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "enrollment_id",
                table: "External_Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "External_Transfers",
                keyColumn: "transfer_id",
                keyValue: 1,
                column: "enrollment_id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "External_Transfers",
                keyColumn: "transfer_id",
                keyValue: 2,
                column: "enrollment_id",
                value: 12);

            migrationBuilder.UpdateData(
                table: "External_Transfers",
                keyColumn: "transfer_id",
                keyValue: 3,
                column: "enrollment_id",
                value: 13);

            migrationBuilder.CreateIndex(
                name: "IX_External_Transfers_enrollment_id",
                table: "External_Transfers",
                column: "enrollment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_External_Transfers_Student_Group_Enrollment_enrollment_id",
                table: "External_Transfers",
                column: "enrollment_id",
                principalTable: "Student_Group_Enrollment",
                principalColumn: "enrollment_id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
