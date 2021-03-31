using Microsoft.EntityFrameworkCore.Migrations;

namespace HamsterDayCare.Data.Migrations
{
    public partial class asdffsakjhsdrf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CageId",
                table: "DayCareStays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CageId",
                table: "DayCareStays");
        }
    }
}
