using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupLifecycleDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "date_closed",
                table: "Study_Group",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "date_created",
                table: "Study_Group",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 1,
                columns: new[] { "date_closed", "date_created" },
                values: new object[] { null, new DateOnly(2021, 9, 1) });

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 2,
                columns: new[] { "date_closed", "date_created" },
                values: new object[] { null, new DateOnly(2022, 9, 1) });

            migrationBuilder.UpdateData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 3,
                columns: new[] { "date_closed", "date_created" },
                values: new object[] { null, new DateOnly(2023, 9, 1) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_closed",
                table: "Study_Group");

            migrationBuilder.DropColumn(
                name: "date_created",
                table: "Study_Group");
        }
    }
}
