using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCityAndCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_institution_city_city_id",
                table: "institution");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropIndex(
                name: "IX_institution_city_id",
                table: "institution");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "institution");

            migrationBuilder.DropColumn(
                name: "country",
                table: "institution");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bba-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bca-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bce-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bda-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bde-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bea-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000beb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bed-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bee-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bef-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bff-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c00-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c01-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c02-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c03-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c04-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c05-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c06-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c07-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c08-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c09-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c10-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c11-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c12-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c13-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c14-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c15-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c16-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c17-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c18-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c20-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c21-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c22-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c23-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c24-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c25-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c26-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c27-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c28-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c29-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c30-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c31-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c32-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c33-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c34-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c35-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c36-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c37-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c38-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c39-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c40-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c41-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c42-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c43-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c44-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c45-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c46-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c47-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c48-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c49-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c50-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c51-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c52-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c53-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c54-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c55-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c56-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c57-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c59-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c60-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c61-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c62-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c63-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c64-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c65-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c66-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c67-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c68-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c69-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c70-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c71-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c72-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c73-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c74-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c75-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c76-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c77-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c78-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c79-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c80-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c81-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c82-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c83-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c84-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c85-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c86-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c87-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c88-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c89-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c90-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c91-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c92-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c93-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c96-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c97-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c98-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cab-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cac-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cad-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cba-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "city_id",
                table: "institution",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "institution",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    city_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.city_id);
                });

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bba-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bca-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bce-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bda-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bde-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bea-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000beb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bed-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bee-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bef-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bff-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c00-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c01-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c02-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c03-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c04-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c05-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c06-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c07-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c08-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c09-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c10-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c11-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c12-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c13-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c14-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c15-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c16-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c17-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c18-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c20-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c21-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c22-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c23-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c24-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c25-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c26-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c27-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c28-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c29-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c30-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c31-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c32-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c33-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c34-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c35-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c36-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c37-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c38-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c39-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c40-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c41-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c42-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c43-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c44-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c45-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c46-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c47-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c48-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c49-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c50-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c51-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c52-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "86");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c53-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c54-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c55-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c56-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c57-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c59-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c60-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c61-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c62-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c63-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c64-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c65-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c66-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c67-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c68-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c69-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c70-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "82");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c71-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c72-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c73-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c74-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c75-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c76-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c77-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c78-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c79-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c80-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c81-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c82-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c83-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c84-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c85-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c86-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c87-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c88-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c89-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "73");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c90-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c91-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "76");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c92-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c93-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "94");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c96-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c97-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "93");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c98-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "92");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "77");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "90");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cab-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cac-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cad-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "95");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "84");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "79");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "83");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "85");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "75");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "87");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "91");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cba-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "89");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "74");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "72");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "78");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "88");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "81");

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: "80");

            migrationBuilder.UpdateData(
                table: "institution",
                keyColumn: "institution_id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"),
                columns: new[] { "city_id", "country" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "institution",
                keyColumn: "institution_id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"),
                columns: new[] { "city_id", "country" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "institution",
                keyColumn: "institution_id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000000"),
                columns: new[] { "city_id", "country" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_institution_city_id",
                table: "institution",
                column: "city_id");

            migrationBuilder.AddForeignKey(
                name: "fk_institution_city_city_id",
                table: "institution",
                column: "city_id",
                principalTable: "city",
                principalColumn: "city_id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
