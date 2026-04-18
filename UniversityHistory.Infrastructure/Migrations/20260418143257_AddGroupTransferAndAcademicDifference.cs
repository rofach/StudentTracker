using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupTransferAndAcademicDifference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_Subgroup_Enrollment_enrollment_id",
                table: "Student_Subgroup_Enrollment");

            migrationBuilder.RenameIndex(
                name: "IX_Student_Subgroup_Enrollment_subgroup_id",
                table: "Student_Subgroup_Enrollment",
                newName: "IX_StudentSubgroupEnrollment_SubgroupId");

            migrationBuilder.CreateTable(
                name: "Student_Group_Transfer",
                columns: table => new
                {
                    transfer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    old_enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    new_enrollment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transfer_date = table.Column<DateOnly>(type: "date", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Group_Transfer", x => x.transfer_id);
                    table.ForeignKey(
                        name: "FK_Student_Group_Transfer_Student_Group_Enrollment_new_enrollment_id",
                        column: x => x.new_enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Group_Transfer_Student_Group_Enrollment_old_enrollment_id",
                        column: x => x.old_enrollment_id,
                        principalTable: "Student_Group_Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Academic_Difference_Item",
                columns: table => new
                {
                    difference_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transfer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    plan_discipline_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'Pending'"),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic_Difference_Item", x => x.difference_item_id);
                    table.CheckConstraint("chk_diff_item_status", "status IN ('Pending','Completed','Waived')");
                    table.ForeignKey(
                        name: "FK_Academic_Difference_Item_Plan_Disciplines_plan_discipline_id",
                        column: x => x.plan_discipline_id,
                        principalTable: "Plan_Disciplines",
                        principalColumn: "plan_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Academic_Difference_Item_Student_Group_Transfer_transfer_id",
                        column: x => x.transfer_id,
                        principalTable: "Student_Group_Transfer",
                        principalColumn: "transfer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubgroupEnrollment_EnrollmentId_DateFrom",
                table: "Student_Subgroup_Enrollment",
                columns: new[] { "enrollment_id", "date_from" });

            migrationBuilder.CreateIndex(
                name: "UX_StudentSubgroupEnrollment_Open",
                table: "Student_Subgroup_Enrollment",
                column: "enrollment_id",
                unique: true,
                filter: "[date_to] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Academic_Difference_Item_plan_discipline_id",
                table: "Academic_Difference_Item",
                column: "plan_discipline_id");

            migrationBuilder.CreateIndex(
                name: "UX_AcademicDifferenceItem_TransferId_PlanDisciplineId",
                table: "Academic_Difference_Item",
                columns: new[] { "transfer_id", "plan_discipline_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupTransfer_NewEnrollmentId",
                table: "Student_Group_Transfer",
                column: "new_enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupTransfer_OldEnrollmentId",
                table: "Student_Group_Transfer",
                column: "old_enrollment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academic_Difference_Item");

            migrationBuilder.DropTable(
                name: "Student_Group_Transfer");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubgroupEnrollment_EnrollmentId_DateFrom",
                table: "Student_Subgroup_Enrollment");

            migrationBuilder.DropIndex(
                name: "UX_StudentSubgroupEnrollment_Open",
                table: "Student_Subgroup_Enrollment");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubgroupEnrollment_SubgroupId",
                table: "Student_Subgroup_Enrollment",
                newName: "IX_Student_Subgroup_Enrollment_subgroup_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Subgroup_Enrollment_enrollment_id",
                table: "Student_Subgroup_Enrollment",
                column: "enrollment_id");
        }
    }
}
