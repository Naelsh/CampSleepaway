using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampSleepaway.Persistence.Migrations
{
    public partial class constraints1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    CamperId = table.Column<int>(type: "int", nullable: false),
                    NextOfKinId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => new { x.CamperId, x.NextOfKinId });
                    table.ForeignKey(
                        name: "FK_Visit_Campers_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Campers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_NextOfKins_NextOfKinId",
                        column: x => x.NextOfKinId,
                        principalTable: "NextOfKins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visit_NextOfKinId",
                table: "Visit",
                column: "NextOfKinId");
        }
    }
}
