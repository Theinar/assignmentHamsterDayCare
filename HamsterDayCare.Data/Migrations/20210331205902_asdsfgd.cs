using Microsoft.EntityFrameworkCore.Migrations;

namespace HamsterDayCare.Data.Migrations
{
    public partial class asdsfgd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hamsters_ExerciseArea_ExerciseAreaId",
                table: "Hamsters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseArea",
                table: "ExerciseArea");

            migrationBuilder.RenameTable(
                name: "ExerciseArea",
                newName: "ExerciseAreas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseAreas",
                table: "ExerciseAreas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hamsters_ExerciseAreas_ExerciseAreaId",
                table: "Hamsters",
                column: "ExerciseAreaId",
                principalTable: "ExerciseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hamsters_ExerciseAreas_ExerciseAreaId",
                table: "Hamsters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseAreas",
                table: "ExerciseAreas");

            migrationBuilder.RenameTable(
                name: "ExerciseAreas",
                newName: "ExerciseArea");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseArea",
                table: "ExerciseArea",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hamsters_ExerciseArea_ExerciseAreaId",
                table: "Hamsters",
                column: "ExerciseAreaId",
                principalTable: "ExerciseArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
