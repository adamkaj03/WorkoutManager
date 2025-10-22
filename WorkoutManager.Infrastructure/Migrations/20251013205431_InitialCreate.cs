using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contraindications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contraindications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EquipmentCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipmentCategories_EquipmentCategoryId",
                        column: x => x.EquipmentCategoryId,
                        principalTable: "EquipmentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    WorkoutProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseGroups_WorkoutPrograms_WorkoutProgramId",
                        column: x => x.WorkoutProgramId,
                        principalTable: "WorkoutPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentContraindications",
                columns: table => new
                {
                    ContraindicationsId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentContraindications", x => new { x.ContraindicationsId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_EquipmentContraindications_Contraindications_ContraindicationsId",
                        column: x => x.ContraindicationsId,
                        principalTable: "Contraindications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentContraindications_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ExerciseGroupId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Exercises_ExerciseGroups_ExerciseGroupId",
                        column: x => x.ExerciseGroupId,
                        principalTable: "ExerciseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseContraindications",
                columns: table => new
                {
                    ContraindicationsId = table.Column<int>(type: "int", nullable: false),
                    ExercisesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseContraindications", x => new { x.ContraindicationsId, x.ExercisesId });
                    table.ForeignKey(
                        name: "FK_ExerciseContraindications_Contraindications_ContraindicationsId",
                        column: x => x.ContraindicationsId,
                        principalTable: "Contraindications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseContraindications_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentCategoryId",
                table: "Equipment",
                column: "EquipmentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentContraindications_EquipmentId",
                table: "EquipmentContraindications",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseContraindications_ExercisesId",
                table: "ExerciseContraindications",
                column: "ExercisesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroups_WorkoutProgramId",
                table: "ExerciseGroups",
                column: "WorkoutProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_EquipmentId",
                table: "Exercises",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ExerciseGroupId",
                table: "Exercises",
                column: "ExerciseGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentContraindications");

            migrationBuilder.DropTable(
                name: "ExerciseContraindications");

            migrationBuilder.DropTable(
                name: "Contraindications");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "ExerciseGroups");

            migrationBuilder.DropTable(
                name: "EquipmentCategories");

            migrationBuilder.DropTable(
                name: "WorkoutPrograms");
        }
    }
}
