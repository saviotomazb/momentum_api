using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Momentum.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueTaskListNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE UNIQUE INDEX "IX_task_lists_UserId_NormalizedName"
                ON task_lists ("UserId", (LOWER(TRIM("Name"))));
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DROP INDEX IF EXISTS "IX_task_lists_UserId_NormalizedName";
                """);
        }
    }
}
