using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutManager.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "WorkoutPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "WorkoutPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "WorkoutPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "WorkoutPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Exercises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Exercises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "ExerciseGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "ExerciseGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "ExerciseGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "ExerciseGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "EquipmentCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "EquipmentCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "EquipmentCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "EquipmentCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Equipment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Equipment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Contraindications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Contraindications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Contraindications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Contraindications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "ExerciseGroups");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "ExerciseGroups");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "ExerciseGroups");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "ExerciseGroups");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "EquipmentCategories");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "EquipmentCategories");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "EquipmentCategories");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "EquipmentCategories");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Contraindications");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Contraindications");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Contraindications");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Contraindications");
        }
    }
}
