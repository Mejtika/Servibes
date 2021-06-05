using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Appointments.Infrastructure.Migrations
{
    public partial class LinkTimeReservationWithCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TimeReservations_CompanyId",
                schema: "appointments",
                table: "TimeReservations",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeReservations_Companies_CompanyId",
                schema: "appointments",
                table: "TimeReservations",
                column: "CompanyId",
                principalSchema: "appointments",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeReservations_Companies_CompanyId",
                schema: "appointments",
                table: "TimeReservations");

            migrationBuilder.DropIndex(
                name: "IX_TimeReservations_CompanyId",
                schema: "appointments",
                table: "TimeReservations");
        }
    }
}
