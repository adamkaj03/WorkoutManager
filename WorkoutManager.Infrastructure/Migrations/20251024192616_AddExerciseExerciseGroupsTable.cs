using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExerciseExerciseGroupsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Equipment_EquipmentId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseGroups_ExerciseGroupId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ExerciseGroupId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseGroupId",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "ExerciseExerciseGroups",
                columns: table => new
                {
                    ExerciseGroupsId = table.Column<int>(type: "int", nullable: false),
                    ExercisesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseExerciseGroups", x => new { x.ExerciseGroupsId, x.ExercisesId });
                    table.ForeignKey(
                        name: "FK_ExerciseExerciseGroups_ExerciseGroups_ExerciseGroupsId",
                        column: x => x.ExerciseGroupsId,
                        principalTable: "ExerciseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseExerciseGroups_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseExerciseGroups_ExercisesId",
                table: "ExerciseExerciseGroups",
                column: "ExercisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Equipment_EquipmentId",
                table: "Exercises",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Equipment_EquipmentId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "ExerciseExerciseGroups");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseGroupId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ExerciseGroupId",
                table: "Exercises",
                column: "ExerciseGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Equipment_EquipmentId",
                table: "Exercises",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseGroups_ExerciseGroupId",
                table: "Exercises",
                column: "ExerciseGroupId",
                principalTable: "ExerciseGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
