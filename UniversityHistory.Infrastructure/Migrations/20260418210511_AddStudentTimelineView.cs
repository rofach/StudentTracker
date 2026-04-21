using Microsoft.EntityFrameworkCore.Migrations;
using UniversityHistory.Infrastructure.Migrations.Sql;

#nullable disable

namespace UniversityHistory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentTimelineView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(StudentTimelineViewSql.Drop);
            migrationBuilder.Sql(StudentTimelineViewSql.Create);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(StudentTimelineViewSql.Drop);
        }
    }
}
