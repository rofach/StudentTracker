using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewInstitutesAndPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "academic_unit",
                columns: new[] { "academic_unit_id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Інститут прикладної математики та кібернетики", "Institute" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Навчально-науковий інститут економіки та менеджменту", "Institute" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Факультет лінгвістики та права", "Faculty" }
                });

            migrationBuilder.InsertData(
                table: "discipline",
                columns: new[] { "discipline_id", "description", "discipline_name" },
                values: new object[,]
                {
                    { new Guid("00000046-0000-0000-0000-000000000000"), "Навчальна дисципліна «Спеціалізована фізика для кібернетиків» із професійної підготовки здобувачів ІТ-спеціальностей.", "Спеціалізована фізика для кібернетиків" },
                    { new Guid("00000047-0000-0000-0000-000000000000"), "Навчальна дисципліна «Поглиблена теорія ймовірностей» із професійної підготовки здобувачів ІТ-спеціальностей.", "Поглиблена теорія ймовірностей" },
                    { new Guid("00000048-0000-0000-0000-000000000000"), "Навчальна дисципліна «Історія України та української культури» із професійної підготовки здобувачів ІТ-спеціальностей.", "Історія України та української культури" },
                    { new Guid("00000049-0000-0000-0000-000000000000"), "Навчальна дисципліна «Вища математика для економістів» із професійної підготовки здобувачів ІТ-спеціальностей.", "Вища математика для економістів" },
                    { new Guid("0000004a-0000-0000-0000-000000000000"), "Навчальна дисципліна «Мікроекономіка» із професійної підготовки здобувачів ІТ-спеціальностей.", "Мікроекономіка" },
                    { new Guid("0000004b-0000-0000-0000-000000000000"), "Навчальна дисципліна «Макроекономіка» із професійної підготовки здобувачів ІТ-спеціальностей.", "Макроекономіка" },
                    { new Guid("0000004c-0000-0000-0000-000000000000"), "Навчальна дисципліна «Економіка підприємства» із професійної підготовки здобувачів ІТ-спеціальностей.", "Економіка підприємства" },
                    { new Guid("0000004d-0000-0000-0000-000000000000"), "Навчальна дисципліна «Менеджмент» із професійної підготовки здобувачів ІТ-спеціальностей.", "Менеджмент" },
                    { new Guid("0000004e-0000-0000-0000-000000000000"), "Навчальна дисципліна «Маркетинг» із професійної підготовки здобувачів ІТ-спеціальностей.", "Маркетинг" },
                    { new Guid("0000004f-0000-0000-0000-000000000000"), "Навчальна дисципліна «Фінанси, гроші та кредит» із професійної підготовки здобувачів ІТ-спеціальностей.", "Фінанси, гроші та кредит" },
                    { new Guid("00000050-0000-0000-0000-000000000000"), "Навчальна дисципліна «Бухгалтерський облік» із професійної підготовки здобувачів ІТ-спеціальностей.", "Бухгалтерський облік" },
                    { new Guid("00000051-0000-0000-0000-000000000000"), "Навчальна дисципліна «Статистика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Статистика" },
                    { new Guid("00000052-0000-0000-0000-000000000000"), "Навчальна дисципліна «Економетрика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Економетрика" },
                    { new Guid("00000053-0000-0000-0000-000000000000"), "Навчальна дисципліна «Міжнародна економіка» із професійної підготовки здобувачів ІТ-спеціальностей.", "Міжнародна економіка" },
                    { new Guid("00000054-0000-0000-0000-000000000000"), "Навчальна дисципліна «Інформаційні системи в економіці» із професійної підготовки здобувачів ІТ-спеціальностей.", "Інформаційні системи в економіці" },
                    { new Guid("00000055-0000-0000-0000-000000000000"), "Навчальна дисципліна «Бізнес-аналітика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Бізнес-аналітика" },
                    { new Guid("00000056-0000-0000-0000-000000000000"), "Навчальна дисципліна «Ділове спілкування» із професійної підготовки здобувачів ІТ-спеціальностей.", "Ділове спілкування" },
                    { new Guid("00000057-0000-0000-0000-000000000000"), "Навчальна дисципліна «Курсова робота з макроекономіки» із професійної підготовки здобувачів ІТ-спеціальностей.", "Курсова робота з макроекономіки" },
                    { new Guid("00000058-0000-0000-0000-000000000000"), "Навчальна дисципліна «Виробнича практика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Виробнича практика" },
                    { new Guid("00000059-0000-0000-0000-000000000000"), "Навчальна дисципліна «Вступ до мовознавства» із професійної підготовки здобувачів ІТ-спеціальностей.", "Вступ до мовознавства" },
                    { new Guid("0000005a-0000-0000-0000-000000000000"), "Навчальна дисципліна «Практичний курс англійської мови» із професійної підготовки здобувачів ІТ-спеціальностей.", "Практичний курс англійської мови" },
                    { new Guid("0000005b-0000-0000-0000-000000000000"), "Навчальна дисципліна «Практичний курс другої іноземної мови (німецької)» із професійної підготовки здобувачів ІТ-спеціальностей.", "Практичний курс другої іноземної мови (німецької)" },
                    { new Guid("0000005c-0000-0000-0000-000000000000"), "Навчальна дисципліна «Теорія перекладу» із професійної підготовки здобувачів ІТ-спеціальностей.", "Теорія перекладу" },
                    { new Guid("0000005d-0000-0000-0000-000000000000"), "Навчальна дисципліна «Історія англійської мови» із професійної підготовки здобувачів ІТ-спеціальностей.", "Історія англійської мови" },
                    { new Guid("0000005e-0000-0000-0000-000000000000"), "Навчальна дисципліна «Лінгвокраїнознавство» із професійної підготовки здобувачів ІТ-спеціальностей.", "Лінгвокраїнознавство" },
                    { new Guid("0000005f-0000-0000-0000-000000000000"), "Навчальна дисципліна «Практика письмового перекладу» із професійної підготовки здобувачів ІТ-спеціальностей.", "Практика письмового перекладу" },
                    { new Guid("00000060-0000-0000-0000-000000000000"), "Навчальна дисципліна «Практика усного перекладу» із професійної підготовки здобувачів ІТ-спеціальностей.", "Практика усного перекладу" },
                    { new Guid("00000061-0000-0000-0000-000000000000"), "Навчальна дисципліна «Основи термінознавства» із професійної підготовки здобувачів ІТ-спеціальностей.", "Основи термінознавства" },
                    { new Guid("00000062-0000-0000-0000-000000000000"), "Навчальна дисципліна «Комп'ютерні технології в перекладі» із професійної підготовки здобувачів ІТ-спеціальностей.", "Комп'ютерні технології в перекладі" },
                    { new Guid("00000063-0000-0000-0000-000000000000"), "Навчальна дисципліна «Основи правознавства» із професійної підготовки здобувачів ІТ-спеціальностей.", "Основи правознавства" },
                    { new Guid("00000064-0000-0000-0000-000000000000"), "Навчальна дисципліна «Інформаційне право» із професійної підготовки здобувачів ІТ-спеціальностей.", "Інформаційне право" },
                    { new Guid("00000065-0000-0000-0000-000000000000"), "Навчальна дисципліна «Курсова робота з теорії перекладу» із професійної підготовки здобувачів ІТ-спеціальностей.", "Курсова робота з теорії перекладу" },
                    { new Guid("00000066-0000-0000-0000-000000000000"), "Навчальна дисципліна «Виробнича (перекладацька) практика» із професійної підготовки здобувачів ІТ-спеціальностей.", "Виробнича (перекладацька) практика" }
                });

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bba-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bca-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bce-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bda-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bde-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bea-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000beb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bec-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bed-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bee-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bef-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bff-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c00-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c01-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c02-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c03-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c04-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c05-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c06-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c07-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c08-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c10-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c11-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c12-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c13-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c14-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c15-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c16-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c17-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c18-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c19-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c21-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c22-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c23-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c24-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c25-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c26-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c27-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c28-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c29-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c30-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c31-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c32-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c33-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c34-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c35-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c36-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c37-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c38-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c39-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c40-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c41-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c42-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c43-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c45-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c47-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c49-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c50-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c51-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c52-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c53-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c54-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c55-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c56-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c57-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c58-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c60-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c61-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c62-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c63-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c64-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c65-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c66-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c67-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c68-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c69-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c70-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c72-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c73-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c74-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c75-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c76-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c77-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c78-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c79-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c80-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c81-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c82-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c83-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c84-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c85-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c86-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c87-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c88-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c89-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c91-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c92-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c93-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c94-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c95-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c96-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c97-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c98-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c99-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cab-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cac-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cad-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cae-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.InsertData(
                table: "student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "patronymic", "phone" },
                values: new object[,]
                {
                    { new Guid("00000021-0000-0000-0000-000000000000"), new DateOnly(2006, 1, 15), "student33@campus.ua", "Олег", "Сидоренко", "Іванович", "+38067010033" },
                    { new Guid("00000022-0000-0000-0000-000000000000"), new DateOnly(2006, 3, 20), "student34@campus.ua", "Марина", "Коваленко", "Олександрівна", "+38067010034" },
                    { new Guid("00000023-0000-0000-0000-000000000000"), new DateOnly(2006, 5, 25), "student35@campus.ua", "Іван", "Мороз", "Віталійович", "+38067010035" },
                    { new Guid("00000024-0000-0000-0000-000000000000"), new DateOnly(2006, 7, 10), "student36@campus.ua", "Наталія", "Кравченко", "Сергіївна", "+38067010036" },
                    { new Guid("00000025-0000-0000-0000-000000000000"), new DateOnly(2006, 9, 5), "student37@campus.ua", "Антон", "Вовк", "Миколайович", "+38067010037" },
                    { new Guid("00000026-0000-0000-0000-000000000000"), new DateOnly(2006, 11, 12), "student38@campus.ua", "Дарія", "Григоренко", "Андріївна", "+38067010038" },
                    { new Guid("00000027-0000-0000-0000-000000000000"), new DateOnly(2006, 2, 8), "student39@campus.ua", "Євген", "Ткаченко", "Володимирович", "+38067010039" },
                    { new Guid("00000028-0000-0000-0000-000000000000"), new DateOnly(2006, 4, 18), "student40@campus.ua", "Валерія", "Онищенко", "Ігорівна", "+38067010040" },
                    { new Guid("00000029-0000-0000-0000-000000000000"), new DateOnly(2006, 6, 22), "student41@campus.ua", "Станіслав", "Лисенко", "Петрович", "+38067010041" }
                });

            migrationBuilder.InsertData(
                table: "study_plan",
                columns: new[] { "plan_id", "plan_name", "specialty_code", "valid_from" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Кібернетика та ШІ 2025", "122", new DateOnly(2025, 9, 1) },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "Міжнародна економіка 2025", "051", new DateOnly(2025, 9, 1) },
                    { new Guid("00000006-0000-0000-0000-000000000000"), "Прикладна лінгвістика 2025", "035", new DateOnly(2025, 9, 1) }
                });

            migrationBuilder.InsertData(
                table: "department",
                columns: new[] { "department_id", "academic_unit_id", "name" },
                values: new object[,]
                {
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "Кафедра кібернетики та штучного інтелекту" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Кафедра міжнародної економіки" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Кафедра менеджменту та маркетингу" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Кафедра теорії та практики перекладу" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Кафедра інформаційного права" }
                });

            migrationBuilder.InsertData(
                table: "plan_disciplines",
                columns: new[] { "plan_discipline_id", "control_type", "credits", "discipline_id", "plan_id", "semester_no" },
                values: new object[,]
                {
                    { new Guid("00000453-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000454-0000-0000-0000-000000000000"), "Exam", 11m, new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000455-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000456-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("00000046-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000457-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000047-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000458-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000459-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000005-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000045a-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000045b-0000-0000-0000-000000000000"), "Exam", 9m, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000045c-0000-0000-0000-000000000000"), "Exam", 7m, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000045d-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000045e-0000-0000-0000-000000000000"), "Exam", 9m, new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 3 },
                    { new Guid("0000045f-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000460-0000-0000-0000-000000000000"), "Exam", 8m, new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000461-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000462-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000463-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000464-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000465-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000012-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000466-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000467-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000002a-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000468-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000013-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000469-0000-0000-0000-000000000000"), "Credit", 1m, new Guid("00000015-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 4 },
                    { new Guid("0000046a-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000002b-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000046b-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("0000002c-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000046c-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000018-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000046d-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("0000002d-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000046e-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("0000002e-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 5 },
                    { new Guid("0000046f-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("0000002f-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000470-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000030-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 5 },
                    { new Guid("00000471-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000031-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000472-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000032-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000473-0000-0000-0000-000000000000"), "Exam", 4m, new Guid("00000033-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000474-0000-0000-0000-000000000000"), "Credit", 12m, new Guid("00000034-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000475-0000-0000-0000-000000000000"), "Coursework", 6m, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), 6 },
                    { new Guid("00000476-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000048-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000477-0000-0000-0000-000000000000"), "Exam", 8m, new Guid("00000049-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000478-0000-0000-0000-000000000000"), "Credit", 6m, new Guid("00000006-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 1 },
                    { new Guid("00000479-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("0000004a-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000047a-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("0000004b-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000047b-0000-0000-0000-000000000000"), "Exam", 7m, new Guid("0000004c-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000047c-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("0000004d-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000047d-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("0000004e-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000047e-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("0000004f-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000047f-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000050-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000480-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000051-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000481-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000052-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000482-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000053-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000483-0000-0000-0000-000000000000"), "Credit", 5m, new Guid("00000054-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000484-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("00000055-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000485-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000056-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000486-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000057-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000487-0000-0000-0000-000000000000"), "Credit", 12m, new Guid("00000058-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000488-0000-0000-0000-000000000000"), "Coursework", 8m, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000489-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000048-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000048a-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000059-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000048b-0000-0000-0000-000000000000"), "Exam", 12m, new Guid("0000005a-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000048c-0000-0000-0000-000000000000"), "Exam", 10m, new Guid("0000005b-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 1 },
                    { new Guid("0000048d-0000-0000-0000-000000000000"), "Exam", 6m, new Guid("0000005c-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000048e-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("0000005d-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 2 },
                    { new Guid("0000048f-0000-0000-0000-000000000000"), "Credit", 5m, new Guid("0000005e-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000490-0000-0000-0000-000000000000"), "Exam", 8m, new Guid("0000005f-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 2 },
                    { new Guid("00000491-0000-0000-0000-000000000000"), "Exam", 8m, new Guid("00000060-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000492-0000-0000-0000-000000000000"), "Credit", 5m, new Guid("00000061-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000493-0000-0000-0000-000000000000"), "Credit", 4m, new Guid("00000062-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000494-0000-0000-0000-000000000000"), "Credit", 3m, new Guid("00000063-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000495-0000-0000-0000-000000000000"), "Exam", 5m, new Guid("00000064-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000496-0000-0000-0000-000000000000"), "Coursework", 3m, new Guid("00000065-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 3 },
                    { new Guid("00000497-0000-0000-0000-000000000000"), "Credit", 12m, new Guid("00000066-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 4 },
                    { new Guid("00000498-0000-0000-0000-000000000000"), "Coursework", 6m, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000"), 4 }
                });

            migrationBuilder.InsertData(
                table: "study_group",
                columns: new[] { "group_id", "date_closed", "date_created", "department_id", "group_code" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000005-0000-0000-0000-000000000000"), "ШІ-25" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000006-0000-0000-0000-000000000000"), "МЕ-25" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), null, new DateOnly(2025, 9, 1), new Guid("00000008-0000-0000-0000-000000000000"), "ПЛ-25" }
                });

            migrationBuilder.InsertData(
                table: "group_plan_assignment",
                columns: new[] { "group_plan_assignment_id", "date_from", "date_to", "group_id", "plan_id" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000006-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_group_enrollment",
                columns: new[] { "enrollment_id", "date_from", "date_to", "group_id", "reason_end", "reason_start", "student_id" },
                values: new object[,]
                {
                    { new Guid("00000021-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000007-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000021-0000-0000-0000-000000000000") },
                    { new Guid("00000022-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000007-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000022-0000-0000-0000-000000000000") },
                    { new Guid("00000023-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000007-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000023-0000-0000-0000-000000000000") },
                    { new Guid("00000024-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000008-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000024-0000-0000-0000-000000000000") },
                    { new Guid("00000025-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000008-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000025-0000-0000-0000-000000000000") },
                    { new Guid("00000026-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000008-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000026-0000-0000-0000-000000000000") },
                    { new Guid("00000027-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000009-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000027-0000-0000-0000-000000000000") },
                    { new Guid("00000028-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000009-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000028-0000-0000-0000-000000000000") },
                    { new Guid("00000029-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000009-0000-0000-0000-000000000000"), null, "Вступ", new Guid("00000029-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "subgroup",
                columns: new[] { "subgroup_id", "group_id", "subgroup_name" },
                values: new object[,]
                {
                    { new Guid("00000047-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000048-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("00000051-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("00000052-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), "Підгрупа 2" },
                    { new Guid("0000005b-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), "Підгрупа 1" },
                    { new Guid("0000005c-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), "Підгрупа 2" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a64-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000453-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a65-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000454-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a66-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000455-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a67-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000456-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a68-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000459-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a69-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a6a-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a6b-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a6c-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000458-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a6d-0000-0000-0000-000000000000"), 2025, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000457-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a6e-0000-0000-0000-000000000000"), 2026, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045e-0000-0000-0000-000000000000") },
                    { new Guid("00000a6f-0000-0000-0000-000000000000"), 2026, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045f-0000-0000-0000-000000000000") },
                    { new Guid("00000a70-0000-0000-0000-000000000000"), 2026, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000460-0000-0000-0000-000000000000") },
                    { new Guid("00000a71-0000-0000-0000-000000000000"), 2026, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000461-0000-0000-0000-000000000000") },
                    { new Guid("00000a72-0000-0000-0000-000000000000"), 2026, new Guid("00000021-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045d-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a73-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000453-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a74-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000454-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a75-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000455-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a76-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000456-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a77-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000459-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a78-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a79-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a7a-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a7b-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000458-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a7c-0000-0000-0000-000000000000"), 2025, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000457-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a7d-0000-0000-0000-000000000000"), 2026, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045e-0000-0000-0000-000000000000") },
                    { new Guid("00000a7e-0000-0000-0000-000000000000"), 2026, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045f-0000-0000-0000-000000000000") },
                    { new Guid("00000a7f-0000-0000-0000-000000000000"), 2026, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000460-0000-0000-0000-000000000000") },
                    { new Guid("00000a80-0000-0000-0000-000000000000"), 2026, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000461-0000-0000-0000-000000000000") },
                    { new Guid("00000a81-0000-0000-0000-000000000000"), 2026, new Guid("00000022-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045d-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a82-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000453-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a83-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000454-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a84-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000455-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a85-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000456-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a86-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000459-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a87-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a88-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a89-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045a-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a8a-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000458-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a8b-0000-0000-0000-000000000000"), 2025, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000457-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a8c-0000-0000-0000-000000000000"), 2026, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045e-0000-0000-0000-000000000000") },
                    { new Guid("00000a8d-0000-0000-0000-000000000000"), 2026, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045f-0000-0000-0000-000000000000") },
                    { new Guid("00000a8e-0000-0000-0000-000000000000"), 2026, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000460-0000-0000-0000-000000000000") },
                    { new Guid("00000a8f-0000-0000-0000-000000000000"), 2026, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("00000461-0000-0000-0000-000000000000") },
                    { new Guid("00000a90-0000-0000-0000-000000000000"), 2026, new Guid("00000023-0000-0000-0000-000000000000"), new Guid("00000007-0000-0000-0000-000000000000"), new Guid("0000045d-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000a91-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000478-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a92-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000476-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a93-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000477-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a94-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000479-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a95-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047a-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000a96-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a97-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a98-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a99-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000a9a-0000-0000-0000-000000000000"), 2025, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000a9b-0000-0000-0000-000000000000"), 2026, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000480-0000-0000-0000-000000000000") },
                    { new Guid("00000a9c-0000-0000-0000-000000000000"), 2026, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000481-0000-0000-0000-000000000000") },
                    { new Guid("00000a9d-0000-0000-0000-000000000000"), 2026, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000482-0000-0000-0000-000000000000") },
                    { new Guid("00000a9e-0000-0000-0000-000000000000"), 2026, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000483-0000-0000-0000-000000000000") },
                    { new Guid("00000a9f-0000-0000-0000-000000000000"), 2026, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000484-0000-0000-0000-000000000000") },
                    { new Guid("00000aa0-0000-0000-0000-000000000000"), 2026, new Guid("00000024-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000485-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000aa1-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000478-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000aa2-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000476-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000aa3-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000477-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000aa4-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000479-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000aa5-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047a-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000aa6-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000aa7-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000aa8-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000aa9-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000aaa-0000-0000-0000-000000000000"), 2025, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000aab-0000-0000-0000-000000000000"), 2026, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000480-0000-0000-0000-000000000000") },
                    { new Guid("00000aac-0000-0000-0000-000000000000"), 2026, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000481-0000-0000-0000-000000000000") },
                    { new Guid("00000aad-0000-0000-0000-000000000000"), 2026, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000482-0000-0000-0000-000000000000") },
                    { new Guid("00000aae-0000-0000-0000-000000000000"), 2026, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000483-0000-0000-0000-000000000000") },
                    { new Guid("00000aaf-0000-0000-0000-000000000000"), 2026, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000484-0000-0000-0000-000000000000") },
                    { new Guid("00000ab0-0000-0000-0000-000000000000"), 2026, new Guid("00000025-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000485-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000ab1-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000478-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ab2-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000476-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ab3-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000477-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ab4-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000479-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ab5-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047a-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ab6-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047b-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ab7-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047c-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ab8-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ab9-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000aba-0000-0000-0000-000000000000"), 2025, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("0000047f-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000abb-0000-0000-0000-000000000000"), 2026, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000480-0000-0000-0000-000000000000") },
                    { new Guid("00000abc-0000-0000-0000-000000000000"), 2026, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000481-0000-0000-0000-000000000000") },
                    { new Guid("00000abd-0000-0000-0000-000000000000"), 2026, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000482-0000-0000-0000-000000000000") },
                    { new Guid("00000abe-0000-0000-0000-000000000000"), 2026, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000483-0000-0000-0000-000000000000") },
                    { new Guid("00000abf-0000-0000-0000-000000000000"), 2026, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000484-0000-0000-0000-000000000000") },
                    { new Guid("00000ac0-0000-0000-0000-000000000000"), 2026, new Guid("00000026-0000-0000-0000-000000000000"), new Guid("00000008-0000-0000-0000-000000000000"), new Guid("00000485-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000ac1-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000489-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ac2-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048a-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ac3-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ac4-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ac5-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ac6-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ac7-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ac8-0000-0000-0000-000000000000"), 2025, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000490-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000ac9-0000-0000-0000-000000000000"), 2026, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000491-0000-0000-0000-000000000000") },
                    { new Guid("00000aca-0000-0000-0000-000000000000"), 2026, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000492-0000-0000-0000-000000000000") },
                    { new Guid("00000acb-0000-0000-0000-000000000000"), 2026, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000493-0000-0000-0000-000000000000") },
                    { new Guid("00000acc-0000-0000-0000-000000000000"), 2026, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000494-0000-0000-0000-000000000000") },
                    { new Guid("00000acd-0000-0000-0000-000000000000"), 2026, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000495-0000-0000-0000-000000000000") },
                    { new Guid("00000ace-0000-0000-0000-000000000000"), 2026, new Guid("00000027-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000496-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000acf-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000489-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ad0-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048a-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ad1-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ad2-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ad3-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ad4-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ad5-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ad6-0000-0000-0000-000000000000"), 2025, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000490-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000ad7-0000-0000-0000-000000000000"), 2026, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000491-0000-0000-0000-000000000000") },
                    { new Guid("00000ad8-0000-0000-0000-000000000000"), 2026, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000492-0000-0000-0000-000000000000") },
                    { new Guid("00000ad9-0000-0000-0000-000000000000"), 2026, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000493-0000-0000-0000-000000000000") },
                    { new Guid("00000ada-0000-0000-0000-000000000000"), 2026, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000494-0000-0000-0000-000000000000") },
                    { new Guid("00000adb-0000-0000-0000-000000000000"), 2026, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000495-0000-0000-0000-000000000000") },
                    { new Guid("00000adc-0000-0000-0000-000000000000"), 2026, new Guid("00000028-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000496-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id", "status" },
                values: new object[,]
                {
                    { new Guid("00000add-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000489-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ade-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048a-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000adf-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048b-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ae0-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048c-0000-0000-0000-000000000000"), "Completed" },
                    { new Guid("00000ae1-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048d-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ae2-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048e-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ae3-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("0000048f-0000-0000-0000-000000000000"), "InProgress" },
                    { new Guid("00000ae4-0000-0000-0000-000000000000"), 2025, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000490-0000-0000-0000-000000000000"), "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "student_course_enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "enrollment_id", "group_plan_assignment_id", "plan_discipline_id" },
                values: new object[,]
                {
                    { new Guid("00000ae5-0000-0000-0000-000000000000"), 2026, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000491-0000-0000-0000-000000000000") },
                    { new Guid("00000ae6-0000-0000-0000-000000000000"), 2026, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000492-0000-0000-0000-000000000000") },
                    { new Guid("00000ae7-0000-0000-0000-000000000000"), 2026, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000493-0000-0000-0000-000000000000") },
                    { new Guid("00000ae8-0000-0000-0000-000000000000"), 2026, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000494-0000-0000-0000-000000000000") },
                    { new Guid("00000ae9-0000-0000-0000-000000000000"), 2026, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000495-0000-0000-0000-000000000000") },
                    { new Guid("00000aea-0000-0000-0000-000000000000"), 2026, new Guid("00000029-0000-0000-0000-000000000000"), new Guid("00000009-0000-0000-0000-000000000000"), new Guid("00000496-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "student_subgroup_enrollment",
                columns: new[] { "subgroup_enrollment_id", "date_from", "date_to", "enrollment_id", "reason", "subgroup_id" },
                values: new object[,]
                {
                    { new Guid("00000403-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000021-0000-0000-0000-000000000000"), "Вступ", new Guid("00000047-0000-0000-0000-000000000000") },
                    { new Guid("00000404-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000022-0000-0000-0000-000000000000"), "Вступ", new Guid("00000048-0000-0000-0000-000000000000") },
                    { new Guid("00000405-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000024-0000-0000-0000-000000000000"), "Вступ", new Guid("00000051-0000-0000-0000-000000000000") },
                    { new Guid("00000406-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000025-0000-0000-0000-000000000000"), "Вступ", new Guid("00000052-0000-0000-0000-000000000000") },
                    { new Guid("00000407-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000027-0000-0000-0000-000000000000"), "Вступ", new Guid("0000005b-0000-0000-0000-000000000000") },
                    { new Guid("00000408-0000-0000-0000-000000000000"), new DateOnly(2025, 9, 1), null, new Guid("00000028-0000-0000-0000-000000000000"), "Вступ", new Guid("0000005c-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "grade_record",
                columns: new[] { "grade_id", "assessment_date", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { new Guid("00000cc6-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a64-0000-0000-0000-000000000000"), 82 },
                    { new Guid("00000cc7-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a65-0000-0000-0000-000000000000"), 78 },
                    { new Guid("00000cc8-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a66-0000-0000-0000-000000000000"), 76 },
                    { new Guid("00000cc9-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a67-0000-0000-0000-000000000000"), 86 },
                    { new Guid("00000cca-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a73-0000-0000-0000-000000000000"), 74 },
                    { new Guid("00000ccb-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a74-0000-0000-0000-000000000000"), 88 },
                    { new Guid("00000ccc-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a75-0000-0000-0000-000000000000"), 93 },
                    { new Guid("00000ccd-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a76-0000-0000-0000-000000000000"), 81 },
                    { new Guid("00000cce-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a82-0000-0000-0000-000000000000"), 78 },
                    { new Guid("00000ccf-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a83-0000-0000-0000-000000000000"), 72 },
                    { new Guid("00000cd0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a84-0000-0000-0000-000000000000"), 76 },
                    { new Guid("00000cd1-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a85-0000-0000-0000-000000000000"), 79 },
                    { new Guid("00000cd2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a91-0000-0000-0000-000000000000"), 91 },
                    { new Guid("00000cd3-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a92-0000-0000-0000-000000000000"), 81 },
                    { new Guid("00000cd4-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a93-0000-0000-0000-000000000000"), 76 },
                    { new Guid("00000cd5-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a94-0000-0000-0000-000000000000"), 93 },
                    { new Guid("00000cd6-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000a95-0000-0000-0000-000000000000"), 89 },
                    { new Guid("00000cd7-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000aa1-0000-0000-0000-000000000000"), 90 },
                    { new Guid("00000cd8-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000aa2-0000-0000-0000-000000000000"), 72 },
                    { new Guid("00000cd9-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000aa3-0000-0000-0000-000000000000"), 92 },
                    { new Guid("00000cda-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000aa4-0000-0000-0000-000000000000"), 91 },
                    { new Guid("00000cdb-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000aa5-0000-0000-0000-000000000000"), 79 },
                    { new Guid("00000cdc-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ab1-0000-0000-0000-000000000000"), 90 },
                    { new Guid("00000cdd-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ab2-0000-0000-0000-000000000000"), 93 },
                    { new Guid("00000cde-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ab3-0000-0000-0000-000000000000"), 90 },
                    { new Guid("00000cdf-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ab4-0000-0000-0000-000000000000"), 81 },
                    { new Guid("00000ce0-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ab5-0000-0000-0000-000000000000"), 80 },
                    { new Guid("00000ce1-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ac1-0000-0000-0000-000000000000"), 84 },
                    { new Guid("00000ce2-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ac2-0000-0000-0000-000000000000"), 75 },
                    { new Guid("00000ce3-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ac3-0000-0000-0000-000000000000"), 78 },
                    { new Guid("00000ce4-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ac4-0000-0000-0000-000000000000"), 93 },
                    { new Guid("00000ce5-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000acf-0000-0000-0000-000000000000"), 78 },
                    { new Guid("00000ce6-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ad0-0000-0000-0000-000000000000"), 84 },
                    { new Guid("00000ce7-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ad1-0000-0000-0000-000000000000"), 82 },
                    { new Guid("00000ce8-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ad2-0000-0000-0000-000000000000"), 77 },
                    { new Guid("00000ce9-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000add-0000-0000-0000-000000000000"), 85 },
                    { new Guid("00000cea-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ade-0000-0000-0000-000000000000"), 72 },
                    { new Guid("00000ceb-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000adf-0000-0000-0000-000000000000"), 80 },
                    { new Guid("00000cec-0000-0000-0000-000000000000"), new DateOnly(2026, 1, 20), new Guid("00000ae0-0000-0000-0000-000000000000"), 82 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "department",
                keyColumn: "department_id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "department",
                keyColumn: "department_id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cca-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ccb-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ccc-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ccd-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cce-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ccf-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd0-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd1-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd2-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd3-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd4-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd5-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cd9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cda-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cdb-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cdc-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cdd-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cde-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cdf-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce0-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce1-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce2-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce3-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce4-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce5-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ce9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cea-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ceb-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cec-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000462-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000463-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000464-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000465-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000466-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000467-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000468-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000469-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000046a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000046b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000046c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000046d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000046e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000046f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000470-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000471-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000472-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000473-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000474-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000475-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000486-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000487-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000488-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000497-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000498-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a68-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a69-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a6a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a6b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a6c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a6d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a6e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a6f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a70-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a71-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a72-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a77-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a78-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a79-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a7a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a7b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a7c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a7d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a7e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a7f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a80-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a81-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a86-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a87-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a88-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a89-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a8a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a8b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a8c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a8d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a8e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a8f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a90-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a96-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a97-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a98-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a99-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a9a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a9b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a9c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a9d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a9e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a9f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa0-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aaa-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aab-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aac-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aad-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aae-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aaf-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab0-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aba-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000abb-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000abc-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000abd-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000abe-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000abf-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac0-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac5-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aca-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000acb-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000acc-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000acd-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ace-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad3-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad4-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad5-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ada-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000adb-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000adc-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae1-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae2-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae3-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae4-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae5-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae6-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae7-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae8-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae9-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aea-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_subgroup_enrollment",
                keyColumn: "subgroup_enrollment_id",
                keyValue: new Guid("00000403-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_subgroup_enrollment",
                keyColumn: "subgroup_enrollment_id",
                keyValue: new Guid("00000404-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_subgroup_enrollment",
                keyColumn: "subgroup_enrollment_id",
                keyValue: new Guid("00000405-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_subgroup_enrollment",
                keyColumn: "subgroup_enrollment_id",
                keyValue: new Guid("00000406-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_subgroup_enrollment",
                keyColumn: "subgroup_enrollment_id",
                keyValue: new Guid("00000407-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_subgroup_enrollment",
                keyColumn: "subgroup_enrollment_id",
                keyValue: new Guid("00000408-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000057-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000058-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000066-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000457-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000458-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000459-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000045a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000045b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000045c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000045d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000045e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000045f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000460-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000461-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000047b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000047c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000047d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000047e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000047f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000480-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000481-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000482-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000483-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000484-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000485-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000048d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000048e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000048f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000490-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000491-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000492-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000493-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000494-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000495-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000496-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a64-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a65-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a66-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a67-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a73-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a74-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a75-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a76-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a82-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a83-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a84-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a85-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a91-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a92-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a93-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a94-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000a95-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa1-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa2-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa3-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa4-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000aa5-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab1-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab2-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab3-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab4-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ab5-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac1-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac2-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac3-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ac4-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000acf-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad0-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad1-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ad2-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000add-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ade-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000adf-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_course_enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: new Guid("00000ae0-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "subgroup",
                keyColumn: "subgroup_id",
                keyValue: new Guid("00000047-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "subgroup",
                keyColumn: "subgroup_id",
                keyValue: new Guid("00000048-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "subgroup",
                keyColumn: "subgroup_id",
                keyValue: new Guid("00000051-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "subgroup",
                keyColumn: "subgroup_id",
                keyValue: new Guid("00000052-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "subgroup",
                keyColumn: "subgroup_id",
                keyValue: new Guid("0000005b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "subgroup",
                keyColumn: "subgroup_id",
                keyValue: new Guid("0000005c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000047-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000004c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000004d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000004e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000004f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000050-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000051-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000052-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000053-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000054-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000055-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000056-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000005c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000005d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000005e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000005f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000060-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000061-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000062-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000063-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000064-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000065-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "group_plan_assignment",
                keyColumn: "group_plan_assignment_id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "group_plan_assignment",
                keyColumn: "group_plan_assignment_id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "group_plan_assignment",
                keyColumn: "group_plan_assignment_id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000453-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000454-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000455-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000456-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000476-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000477-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000478-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000479-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000047a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("00000489-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000048a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000048b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "plan_disciplines",
                keyColumn: "plan_discipline_id",
                keyValue: new Guid("0000048c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student_group_enrollment",
                keyColumn: "enrollment_id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000046-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000048-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000049-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000004a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000004b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("00000059-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000005a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "discipline",
                keyColumn: "discipline_id",
                keyValue: new Guid("0000005b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "student_id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "study_group",
                keyColumn: "group_id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "study_group",
                keyColumn: "group_id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "study_group",
                keyColumn: "group_id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "study_plan",
                keyColumn: "plan_id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "study_plan",
                keyColumn: "plan_id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "study_plan",
                keyColumn: "plan_id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "department",
                keyColumn: "department_id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "department",
                keyColumn: "department_id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "department",
                keyColumn: "department_id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "academic_unit",
                keyColumn: "academic_unit_id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "academic_unit",
                keyColumn: "academic_unit_id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "academic_unit",
                keyColumn: "academic_unit_id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bba-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bc9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bca-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bce-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bcf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bd9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bda-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bde-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bdf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000be9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bea-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000beb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bec-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bed-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bee-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bef-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bf8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfb-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bfe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000bff-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c00-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c01-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c02-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c03-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c04-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c05-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c06-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c07-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c08-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c0f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c10-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c11-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c12-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c13-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c14-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c15-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c16-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c17-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c18-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c19-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c1f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c21-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c22-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c23-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c24-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c25-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c26-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c27-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c28-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c29-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c2f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c30-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c31-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c32-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c33-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c34-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c35-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c36-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c37-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c38-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c39-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c3f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c40-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c41-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c42-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c43-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c45-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c47-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c49-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c4f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c50-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c51-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c52-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c53-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c54-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c55-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c56-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c57-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c58-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c5f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c60-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c61-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c62-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c63-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c64-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c65-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c66-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c67-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c68-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 95);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c69-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c6f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c70-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c72-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c73-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c74-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c75-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c76-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c77-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c78-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c79-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c7f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c80-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c81-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c82-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c83-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 80);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c84-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c85-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c86-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c87-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c88-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c89-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c8f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c91-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c92-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c93-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c94-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c95-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c96-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c97-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 88);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c98-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c99-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9a-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9b-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9c-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9d-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9e-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000c9f-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 82);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 75);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000ca7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 87);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caa-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 92);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cab-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 84);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cac-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 72);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cad-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 93);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cae-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 89);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000caf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 76);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 83);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 73);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 94);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 79);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb6-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb7-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb8-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cb9-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbc-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 81);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbd-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbe-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cbf-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 85);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc0-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 78);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc1-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 86);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc2-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 91);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc3-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 74);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc4-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 77);

            migrationBuilder.UpdateData(
                table: "grade_record",
                keyColumn: "grade_id",
                keyValue: new Guid("00000cc5-0000-0000-0000-000000000000"),
                column: "grade_value",
                value: 90);
        }
    }
}
