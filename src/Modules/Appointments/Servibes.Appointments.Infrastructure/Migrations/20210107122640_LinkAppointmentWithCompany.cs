using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Appointments.Infrastructure.Migrations
{
    public partial class LinkAppointmentWithCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CompanyId",
                schema: "appointments",
                table: "Appointments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Companies_CompanyId",
                schema: "appointments",
                table: "Appointments",
                column: "CompanyId",
                principalSchema: "appointments",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Companies_CompanyId",
                schema: "appointments",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CompanyId",
                schema: "appointments",
                table: "Appointments");
        }
    }
}
