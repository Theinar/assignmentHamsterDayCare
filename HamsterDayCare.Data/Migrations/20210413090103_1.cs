using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HamsterDayCare.Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    NrOfHamsters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayCareLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCareLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    NrOfHamsters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayCareStays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HamasterId = table.Column<int>(type: "int", nullable: false),
                    CageId = table.Column<int>(type: "int", nullable: false),
                    DayCareLogId = table.Column<int>(type: "int", nullable: false),
                    Arrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCareStays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayCareStays_DayCareLogs_DayCareLogId",
                        column: x => x.DayCareLogId,
                        principalTable: "DayCareLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hamsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CageId = table.Column<int>(type: "int", nullable: true),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciseAreaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hamsters_Cages_CageId",
                        column: x => x.CageId,
                        principalTable: "Cages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hamsters_ExerciseAreas_ExerciseAreaId",
                        column: x => x.ExerciseAreaId,
                        principalTable: "ExerciseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HamsterId = table.Column<int>(type: "int", nullable: false),
                    TypeOfActivity = table.Column<int>(type: "int", nullable: false),
                    AccuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayCareLogId = table.Column<int>(type: "int", nullable: false),
                    DayCareStayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_DayCareStays_DayCareStayId",
                        column: x => x.DayCareStayId,
                        principalTable: "DayCareStays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DayCareStayId",
                table: "Activities",
                column: "DayCareStayId");

            migrationBuilder.CreateIndex(
                name: "IX_DayCareStays_DayCareLogId",
                table: "DayCareStays",
                column: "DayCareLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_CageId",
                table: "Hamsters",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_ExerciseAreaId",
                table: "Hamsters",
                column: "ExerciseAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Hamsters");

            migrationBuilder.DropTable(
                name: "DayCareStays");

            migrationBuilder.DropTable(
                name: "Cages");

            migrationBuilder.DropTable(
                name: "ExerciseAreas");

            migrationBuilder.DropTable(
                name: "DayCareLogs");
        }
    }
}
