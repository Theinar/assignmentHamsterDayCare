using Microsoft.EntityFrameworkCore.Migrations;

namespace HamsterDayCare.Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_DayCareStays_DayCareStayId",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.RenameTable(
                name: "Activity",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_DayCareStayId",
                table: "Activities",
                newName: "IX_Activities_DayCareStayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_DayCareStays_DayCareStayId",
                table: "Activities",
                column: "DayCareStayId",
                principalTable: "DayCareStays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_DayCareStays_DayCareStayId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Activity");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_DayCareStayId",
                table: "Activity",
                newName: "IX_Activity_DayCareStayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_DayCareStays_DayCareStayId",
                table: "Activity",
                column: "DayCareStayId",
                principalTable: "DayCareStays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
