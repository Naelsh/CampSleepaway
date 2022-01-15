using Microsoft.EntityFrameworkCore.Migrations;

namespace CampSleepaway.Persistence.Migrations
{
    public partial class dateColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Visit",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Visit",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "CabinCounselorStay",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "CabinCounselorStay",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "CabinCamperStay",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "CabinCamperStay",
                newName: "EndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Visit",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Visit",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "CabinCounselorStay",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "CabinCounselorStay",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "CabinCamperStay",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "CabinCamperStay",
                newName: "End");
        }
    }
}
