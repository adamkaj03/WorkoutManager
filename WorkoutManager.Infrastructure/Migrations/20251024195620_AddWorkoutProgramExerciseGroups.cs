using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutProgramExerciseGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_WorkoutPrograms_WorkoutProgramId",
                table: "ExerciseGroups");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseGroups_WorkoutProgramId",
                table: "ExerciseGroups");

            migrationBuilder.DropColumn(
                name: "WorkoutProgramId",
                table: "ExerciseGroups");

            migrationBuilder.CreateTable(
                name: "WorkoutProgramExerciseGroups",
                columns: table => new
                {
                    ExerciseGroupsId = table.Column<int>(type: "int", nullable: false),
                    WorkoutProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutProgramExerciseGroups", x => new { x.ExerciseGroupsId, x.WorkoutProgramId });
                    table.ForeignKey(
                        name: "FK_WorkoutProgramExerciseGroups_ExerciseGroups_ExerciseGroupsId",
                        column: x => x.ExerciseGroupsId,
                        principalTable: "ExerciseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutProgramExerciseGroups_WorkoutPrograms_WorkoutProgramId",
                        column: x => x.WorkoutProgramId,
                        principalTable: "WorkoutPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutProgramExerciseGroups_WorkoutProgramId",
                table: "WorkoutProgramExerciseGroups",
                column: "WorkoutProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutProgramExerciseGroups");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutProgramId",
                table: "ExerciseGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroups_WorkoutProgramId",
                table: "ExerciseGroups",
                column: "WorkoutProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseGroups_WorkoutPrograms_WorkoutProgramId",
                table: "ExerciseGroups",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
