using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationsToWorkoutProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainWorkoutDurationMinutes",
                table: "WorkoutPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarmupDurationMinutes",
                table: "WorkoutPrograms",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainWorkoutDurationMinutes",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "WarmupDurationMinutes",
                table: "WorkoutPrograms");
        }
    }
}
