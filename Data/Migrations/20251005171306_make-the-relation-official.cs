using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class maketherelationofficial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_ProjectId",
                table: "tasks");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_ProjectId",
                table: "tasks",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_ProjectId",
                table: "tasks");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_ProjectId",
                table: "tasks",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id");
        }
    }
}
