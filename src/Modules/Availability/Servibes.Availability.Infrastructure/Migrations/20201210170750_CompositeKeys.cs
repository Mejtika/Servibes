using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Availability.Infrastructure.Migrations
{
    public partial class CompositeKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingHours",
                schema: "availability",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeOffs",
                schema: "availability",
                table: "TimeOffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                schema: "availability",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpeningHours",
                schema: "availability",
                table: "OpeningHours");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingHours",
                schema: "availability",
                table: "WorkingHours",
                columns: new[] { "WorkingHourId", "EmployeeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeOffs",
                schema: "availability",
                table: "TimeOffs",
                columns: new[] { "TimeOffId", "EmployeeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                schema: "availability",
                table: "Reservations",
                columns: new[] { "ReservationId", "EmployeeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpeningHours",
                schema: "availability",
                table: "OpeningHours",
                columns: new[] { "OpeningHoursId", "CompanyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingHours",
                schema: "availability",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeOffs",
                schema: "availability",
                table: "TimeOffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                schema: "availability",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpeningHours",
                schema: "availability",
                table: "OpeningHours");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingHours",
                schema: "availability",
                table: "WorkingHours",
                column: "WorkingHourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeOffs",
                schema: "availability",
                table: "TimeOffs",
                column: "TimeOffId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                schema: "availability",
                table: "Reservations",
                column: "ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpeningHours",
                schema: "availability",
                table: "OpeningHours",
                column: "OpeningHoursId");
        }
    }
}
