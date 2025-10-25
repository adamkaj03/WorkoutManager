using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteOrderColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ExerciseGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ExerciseGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
