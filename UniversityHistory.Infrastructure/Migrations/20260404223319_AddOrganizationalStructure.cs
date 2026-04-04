using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationalStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "faculty",
                table: "Study_Group");

            migrationBuilder.AddColumn<int>(
                name: "department_id",
                table: "Study_Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Academic_Unit",
                columns: table => new
                {
                    academic_unit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic_Unit", x => x.academic_unit_id);
                    table.CheckConstraint("chk_academic_unit_type", "type IN ('Faculty','Institute')");
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    department_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    academic_unit_id = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Academic_Unit",
                columns: new[] { "academic_unit_id", "name", "type" },
                values: new object[,]
                {
                    { 1, "Факультет інформатики та обчислювальної техніки", "Faculty" },
                    { 2, "Факультет прикладної математики", "Faculty" },
                    { 3, "Факультет комп'ютерної інженерії", "Faculty" }
                });

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 1,
                column: "department_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 2,
                column: "department_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 3,
                column: "department_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 4,
                column: "department_id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 5,
                column: "department_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 6,
                column: "department_id",
                value: 5);

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "department_id", "academic_unit_id", "name" },
                values: new object[,]
                {
                    { 1, 1, "Кафедра програмування" },
                    { 2, 1, "Кафедра комп'ютерних наук" },
                    { 3, 2, "Кафедра прикладної математики" },
                    { 4, 2, "Кафедра програмного забезпечення" },
                    { 5, 3, "Кафедра комп'ютерної інженерії" },
                    { 6, 3, "Кафедра вбудованих систем" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Study_Group_department_id",
                table: "Study_Group",
                column: "department_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Study_Group_Department_department_id",
                table: "Study_Group",
                column: "department_id",
                principalTable: "Department",
                principalColumn: "department_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_Group_Department_department_id",
                table: "Study_Group");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Academic_Unit");

            migrationBuilder.DropIndex(
                name: "IX_Study_Group_department_id",
                table: "Study_Group");

            migrationBuilder.DropColumn(
                name: "department_id",
                table: "Study_Group");

            migrationBuilder.AddColumn<string>(
                name: "faculty",
                table: "Study_Group",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 1,
                column: "faculty",
                value: "Факультет інформатики та обчислювальної техніки");

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 2,
                column: "faculty",
                value: "Факультет інформатики та обчислювальної техніки");

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 3,
                column: "faculty",
                value: "Факультет прикладної математики");

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 4,
                column: "faculty",
                value: "Факультет комп'ютерної інженерії");

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 5,
                column: "faculty",
                value: "Факультет прикладної математики");

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 6,
                column: "faculty",
                value: "Факультет комп'ютерної інженерії");
        }
    }
}
