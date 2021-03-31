using Microsoft.EntityFrameworkCore.Migrations;

namespace HamsterDayCare.Data.Migrations
{
    public partial class asdadv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NrOfHamsters",
                table: "ExerciseArea",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrOfHamsters",
                table: "ExerciseArea");
        }
    }
}
