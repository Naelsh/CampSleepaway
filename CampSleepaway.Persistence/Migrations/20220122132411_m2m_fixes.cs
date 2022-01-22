using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampSleepaway.Persistence.Migrations
{
    public partial class m2m_fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinCamperStay");

            migrationBuilder.DropTable(
                name: "CabinCounselorStay");

            migrationBuilder.DropTable(
                name: "CamperNextOfKin");

            migrationBuilder.CreateTable(
                name: "CabinCamperStays",
                columns: table => new
                {
                    CamperId = table.Column<int>(type: "int", nullable: false),
                    CabinId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinCamperStays", x => new { x.CabinId, x.CamperId });
                    table.ForeignKey(
                        name: "FK_CabinCamperStays_Cabins_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinCamperStays_Campers_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Campers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinCounselorStays",
                columns: table => new
                {
                    CounselorId = table.Column<int>(type: "int", nullable: false),
                    CabinId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinCounselorStays", x => new { x.CabinId, x.CounselorId });
                    table.ForeignKey(
                        name: "FK_CabinCounselorStays_Cabins_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinCounselorStays_Counselors_CounselorId",
                        column: x => x.CounselorId,
                        principalTable: "Counselors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CamperNextOfKins",
                columns: table => new
                {
                    NextOfKinId = table.Column<int>(type: "int", nullable: false),
                    CamperId = table.Column<int>(type: "int", nullable: false),
                    NextOfKinRelationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamperNextOfKins", x => new { x.CamperId, x.NextOfKinId });
                    table.ForeignKey(
                        name: "FK_CamperNextOfKins_Campers_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Campers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CamperNextOfKins_NextOfKins_NextOfKinId",
                        column: x => x.NextOfKinId,
                        principalTable: "NextOfKins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabinCamperStays_CamperId",
                table: "CabinCamperStays",
                column: "CamperId");

            migrationBuilder.CreateIndex(
                name: "IX_CabinCounselorStays_CounselorId",
                table: "CabinCounselorStays",
                column: "CounselorId");

            migrationBuilder.CreateIndex(
                name: "IX_CamperNextOfKins_NextOfKinId",
                table: "CamperNextOfKins",
                column: "NextOfKinId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinCamperStays");

            migrationBuilder.DropTable(
                name: "CabinCounselorStays");

            migrationBuilder.DropTable(
                name: "CamperNextOfKins");

            migrationBuilder.CreateTable(
                name: "CabinCamperStay",
                columns: table => new
                {
                    CabinId = table.Column<int>(type: "int", nullable: false),
                    CamperId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinCamperStay", x => new { x.CabinId, x.CamperId });
                    table.ForeignKey(
                        name: "FK_CabinCamperStay_Cabins_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinCamperStay_Campers_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Campers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinCounselorStay",
                columns: table => new
                {
                    CabinId = table.Column<int>(type: "int", nullable: false),
                    CounselorId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinCounselorStay", x => new { x.CabinId, x.CounselorId });
                    table.ForeignKey(
                        name: "FK_CabinCounselorStay_Cabins_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinCounselorStay_Counselors_CounselorId",
                        column: x => x.CounselorId,
                        principalTable: "Counselors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CamperNextOfKin",
                columns: table => new
                {
                    CamperId = table.Column<int>(type: "int", nullable: false),
                    NextOfKinId = table.Column<int>(type: "int", nullable: false),
                    NextOfKinRelationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamperNextOfKin", x => new { x.CamperId, x.NextOfKinId });
                    table.ForeignKey(
                        name: "FK_CamperNextOfKin_Campers_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Campers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CamperNextOfKin_NextOfKins_NextOfKinId",
                        column: x => x.NextOfKinId,
                        principalTable: "NextOfKins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabinCamperStay_CamperId",
                table: "CabinCamperStay",
                column: "CamperId");

            migrationBuilder.CreateIndex(
                name: "IX_CabinCounselorStay_CounselorId",
                table: "CabinCounselorStay",
                column: "CounselorId");

            migrationBuilder.CreateIndex(
                name: "IX_CamperNextOfKin_NextOfKinId",
                table: "CamperNextOfKin",
                column: "NextOfKinId");
        }
    }
}
