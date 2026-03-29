using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discipline",
                columns: new[] { "discipline_id", "discipline_name" },
                values: new object[,]
                {
                    { 5, "Discrete Mathematics" },
                    { 6, "Databases" },
                    { 7, "Operating Systems" },
                    { 8, "Computer Networks" }
                });

            migrationBuilder.InsertData(
                table: "Institution",
                columns: new[] { "institution_id", "city", "country", "institution_name" },
                values: new object[,]
                {
                    { 3, "Kyiv", "Ukraine", "Kyiv Polytechnic Institute" },
                    { 4, "Lviv", "Ukraine", "Lviv Polytechnic" }
                });

            migrationBuilder.InsertData(
                table: "Plan_Disciplines",
                columns: new[] { "discipline_id", "plan_id", "control_type", "credits", "hours", "semester_no" },
                values: new object[] { 4, 1, "Exam", 4.0m, 120, 2 });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[,]
                {
                    { 5, new DateOnly(2003, 3, 2), "evan@example.com", "Evan", "Davis", "555-0105" },
                    { 6, new DateOnly(2004, 2, 19), "fiona@example.com", "Fiona", "Miller", "555-0106" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[,]
                {
                    { 7, new DateOnly(2002, 9, 9), "george@example.com", "George", "Wilson", "555-0107", "Graduated" },
                    { 8, new DateOnly(2004, 6, 10), "hannah@example.com", "Hannah", "Moore", "555-0108", "OnLeave" },
                    { 9, new DateOnly(2003, 12, 1), "ivan@example.com", "Ivan", "Taylor", "555-0109", "Expelled" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone" },
                values: new object[,]
                {
                    { 10, new DateOnly(2005, 4, 11), "julia@example.com", "Julia", "Anderson", "555-0110" },
                    { 11, new DateOnly(2004, 8, 5), "kevin@example.com", "Kevin", "Thomas", "555-0111" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "student_id", "birth_date", "email", "first_name", "last_name", "phone", "status" },
                values: new object[] { 12, new DateOnly(2002, 5, 23), "laura@example.com", "Laura", "Jackson", "555-0112", "Graduated" });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 4, 2022, 1, 3, "Completed" },
                    { 5, 2022, 1, 4, "Completed" },
                    { 7, 2021, 2, 2, "Completed" },
                    { 8, 2022, 2, 3, "Completed" },
                    { 9, 2022, 2, 4, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Student_Plan_Assignment",
                columns: new[] { "assignment_id", "date_from", "date_to", "plan_id", "student_id" },
                values: new object[,]
                {
                    { 3, new DateOnly(2022, 9, 1), null, 2, 3 },
                    { 4, new DateOnly(2023, 9, 1), null, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Study_Group",
                columns: new[] { "group_id", "date_closed", "date_created", "faculty", "group_code" },
                values: new object[,]
                {
                    { 4, null, new DateOnly(2024, 9, 1), "Computer Science", "CS-24" },
                    { 5, null, new DateOnly(2025, 9, 1), "Computer Science", "CS-25" }
                });

            migrationBuilder.InsertData(
                table: "Subgroup",
                columns: new[] { "subgroup_id", "group_id", "subgroup_name" },
                values: new object[] { 4, 3, "Subgroup A" });

            migrationBuilder.InsertData(
                table: "Grade_Record",
                columns: new[] { "grade_id", "assessment_date", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { 4, new DateOnly(2022, 6, 10), 4, "89" },
                    { 5, new DateOnly(2022, 6, 14), 5, "84" },
                    { 6, new DateOnly(2022, 1, 16), 7, "90" },
                    { 7, new DateOnly(2022, 6, 12), 8, "86" },
                    { 8, new DateOnly(2022, 6, 15), 9, "79" }
                });

            migrationBuilder.InsertData(
                table: "Plan_Disciplines",
                columns: new[] { "discipline_id", "plan_id", "control_type", "credits", "hours", "semester_no" },
                values: new object[,]
                {
                    { 5, 1, "Credit", 3.0m, 90, 1 },
                    { 6, 1, "Exam", 5.0m, 150, 3 },
                    { 7, 1, "Exam", 5.0m, 150, 4 },
                    { 5, 2, "Credit", 3.0m, 90, 2 },
                    { 6, 2, "Exam", 5.0m, 150, 3 },
                    { 8, 2, "Exam", 5.0m, 150, 4 }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 6, 2023, 1, 6, "InProgress" },
                    { 10, 2023, 4, 1, "Completed" },
                    { 11, 2023, 4, 2, "Completed" },
                    { 12, 2024, 4, 4, "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Group_Enrollment",
                columns: new[] { "enrollment_id", "date_from", "date_to", "group_id", "reason_end", "reason_start", "student_id" },
                values: new object[,]
                {
                    { 6, new DateOnly(2023, 9, 1), null, 3, null, "Transfer", 5 },
                    { 7, new DateOnly(2024, 9, 1), null, 4, null, "Admission", 6 },
                    { 8, new DateOnly(2022, 9, 1), new DateOnly(2025, 6, 30), 2, "Graduation", "Admission", 7 },
                    { 9, new DateOnly(2024, 9, 1), null, 4, null, "Admission", 8 },
                    { 10, new DateOnly(2023, 9, 1), new DateOnly(2024, 12, 20), 3, "Expelled", "Admission", 9 },
                    { 11, new DateOnly(2025, 9, 1), null, 5, null, "Admission", 10 },
                    { 12, new DateOnly(2024, 9, 1), null, 4, null, "Transfer", 11 },
                    { 13, new DateOnly(2021, 9, 1), new DateOnly(2024, 6, 30), 1, "Graduation", "Admission", 12 },
                    { 14, new DateOnly(2022, 9, 1), new DateOnly(2023, 6, 30), 2, "Transfer to new group", "Admission", 5 }
                });

            migrationBuilder.InsertData(
                table: "Student_Plan_Assignment",
                columns: new[] { "assignment_id", "date_from", "date_to", "plan_id", "student_id" },
                values: new object[,]
                {
                    { 5, new DateOnly(2022, 9, 1), null, 1, 5 },
                    { 6, new DateOnly(2024, 9, 1), null, 2, 6 },
                    { 7, new DateOnly(2022, 9, 1), new DateOnly(2025, 6, 30), 1, 7 },
                    { 8, new DateOnly(2024, 9, 1), null, 2, 8 },
                    { 9, new DateOnly(2025, 9, 1), null, 2, 10 },
                    { 10, new DateOnly(2024, 9, 1), null, 2, 11 },
                    { 11, new DateOnly(2021, 9, 1), new DateOnly(2024, 6, 30), 1, 12 },
                    { 12, new DateOnly(2023, 9, 1), new DateOnly(2024, 12, 20), 1, 9 }
                });

            migrationBuilder.InsertData(
                table: "Subgroup",
                columns: new[] { "subgroup_id", "group_id", "subgroup_name" },
                values: new object[,]
                {
                    { 5, 4, "Subgroup A" },
                    { 6, 4, "Subgroup B" },
                    { 7, 5, "Subgroup A" }
                });

            migrationBuilder.InsertData(
                table: "Academic_Leave",
                columns: new[] { "leave_id", "end_date", "enrollment_id", "reason", "start_date" },
                values: new object[,]
                {
                    { 2, new DateOnly(2024, 2, 10), 8, "Internship pause", new DateOnly(2023, 11, 10) },
                    { 3, null, 9, "Family circumstances", new DateOnly(2025, 2, 1) }
                });

            migrationBuilder.InsertData(
                table: "External_Transfers",
                columns: new[] { "transfer_id", "enrollment_id", "institution_id", "notes", "student_id", "transfer_date", "transfer_type" },
                values: new object[,]
                {
                    { 2, 12, 3, "Transferred after first semester", 11, new DateOnly(2024, 8, 20), "In" },
                    { 3, 13, 4, "Started master's program", 12, new DateOnly(2024, 7, 1), "Out" }
                });

            migrationBuilder.InsertData(
                table: "Grade_Record",
                columns: new[] { "grade_id", "assessment_date", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { 9, new DateOnly(2024, 1, 20), 10, "92" },
                    { 10, new DateOnly(2024, 1, 22), 11, "87" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 13, 2022, 5, 1, "Completed" },
                    { 14, 2022, 5, 2, "Completed" },
                    { 15, 2023, 5, 3, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id" },
                values: new object[] { 16, 2024, 5, 6 });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 17, 2024, 6, 1, "Completed" },
                    { 18, 2024, 6, 2, "Completed" },
                    { 19, 2025, 6, 4, "InProgress" },
                    { 20, 2022, 7, 1, "Completed" },
                    { 21, 2022, 7, 2, "Completed" },
                    { 22, 2023, 7, 3, "Completed" },
                    { 23, 2023, 7, 4, "Completed" },
                    { 24, 2024, 7, 6, "Completed" },
                    { 25, 2024, 8, 1, "Retake" },
                    { 26, 2024, 8, 2, "Completed" },
                    { 27, 2025, 9, 1, "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id" },
                values: new object[] { 28, 2025, 9, 5 });

            migrationBuilder.InsertData(
                table: "Student_Course_Enrollment",
                columns: new[] { "course_enrollment_id", "academic_year_start", "assignment_id", "discipline_id", "status" },
                values: new object[,]
                {
                    { 29, 2024, 10, 1, "Completed" },
                    { 30, 2024, 10, 2, "Completed" },
                    { 31, 2025, 10, 8, "InProgress" },
                    { 32, 2021, 11, 1, "Completed" },
                    { 33, 2021, 11, 2, "Completed" },
                    { 34, 2022, 11, 3, "Completed" },
                    { 35, 2022, 11, 4, "Completed" },
                    { 36, 2023, 12, 1, "Completed" },
                    { 37, 2023, 12, 2, "Retake" }
                });

            migrationBuilder.InsertData(
                table: "Student_Subgroup_Assignment",
                columns: new[] { "enrollment_id", "subgroup_id" },
                values: new object[,]
                {
                    { 6, 4 },
                    { 7, 5 },
                    { 8, 3 },
                    { 9, 6 },
                    { 11, 7 },
                    { 12, 5 },
                    { 13, 2 },
                    { 14, 3 }
                });

            migrationBuilder.InsertData(
                table: "Grade_Record",
                columns: new[] { "grade_id", "assessment_date", "course_enrollment_id", "grade_value" },
                values: new object[,]
                {
                    { 11, new DateOnly(2023, 1, 18), 13, "81" },
                    { 12, new DateOnly(2023, 1, 20), 14, "85" },
                    { 13, new DateOnly(2023, 6, 19), 15, "88" },
                    { 14, new DateOnly(2025, 1, 21), 17, "93" },
                    { 15, new DateOnly(2025, 1, 23), 18, "89" },
                    { 16, new DateOnly(2023, 1, 19), 20, "76" },
                    { 17, new DateOnly(2023, 1, 22), 21, "82" },
                    { 18, new DateOnly(2023, 6, 15), 22, "80" },
                    { 19, new DateOnly(2023, 6, 20), 23, "78" },
                    { 20, new DateOnly(2024, 12, 18), 24, "84" },
                    { 21, new DateOnly(2025, 1, 28), 25, "74" },
                    { 22, new DateOnly(2025, 1, 25), 26, "83" },
                    { 23, new DateOnly(2025, 1, 24), 29, "91" },
                    { 24, new DateOnly(2025, 1, 26), 30, "88" },
                    { 25, new DateOnly(2022, 1, 18), 32, "94" },
                    { 26, new DateOnly(2022, 1, 21), 33, "90" },
                    { 27, new DateOnly(2022, 6, 16), 34, "87" },
                    { 28, new DateOnly(2022, 6, 18), 35, "85" },
                    { 29, new DateOnly(2024, 1, 15), 36, "69" },
                    { 30, new DateOnly(2024, 1, 19), 37, "72" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Academic_Leave",
                keyColumn: "leave_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Academic_Leave",
                keyColumn: "leave_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "External_Transfers",
                keyColumn: "transfer_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "External_Transfers",
                keyColumn: "transfer_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Grade_Record",
                keyColumn: "grade_id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Plan_Disciplines",
                keyColumns: new[] { "discipline_id", "plan_id" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "Plan_Disciplines",
                keyColumns: new[] { "discipline_id", "plan_id" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Plan_Disciplines",
                keyColumns: new[] { "discipline_id", "plan_id" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Plan_Disciplines",
                keyColumns: new[] { "discipline_id", "plan_id" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "Plan_Disciplines",
                keyColumns: new[] { "discipline_id", "plan_id" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "Plan_Disciplines",
                keyColumns: new[] { "discipline_id", "plan_id" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "Plan_Disciplines",
                keyColumns: new[] { "discipline_id", "plan_id" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Student_Subgroup_Assignment",
                keyColumn: "enrollment_id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Discipline",
                keyColumn: "discipline_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Discipline",
                keyColumn: "discipline_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Discipline",
                keyColumn: "discipline_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Institution",
                keyColumn: "institution_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Institution",
                keyColumn: "institution_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Student_Course_Enrollment",
                keyColumn: "course_enrollment_id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Student_Group_Enrollment",
                keyColumn: "enrollment_id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Subgroup",
                keyColumn: "subgroup_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subgroup",
                keyColumn: "subgroup_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subgroup",
                keyColumn: "subgroup_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subgroup",
                keyColumn: "subgroup_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Discipline",
                keyColumn: "discipline_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Student_Plan_Assignment",
                keyColumn: "assignment_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Study_Group",
                keyColumn: "group_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "student_id",
                keyValue: 12);
        }
    }
}
