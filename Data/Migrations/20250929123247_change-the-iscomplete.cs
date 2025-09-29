using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class changetheiscomplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "tasks");

            migrationBuilder.AddColumn<int>(
                name: "TaskStatus",
                table: "tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "tasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
