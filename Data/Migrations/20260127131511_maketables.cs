using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class maketables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_UserId",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "projects",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_projects_UserId",
                table: "projects",
                newName: "IX_projects_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_userId",
                table: "projects",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_userId",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "projects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_projects_userId",
                table: "projects",
                newName: "IX_projects_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_UserId",
                table: "projects",
                column: "UserId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
