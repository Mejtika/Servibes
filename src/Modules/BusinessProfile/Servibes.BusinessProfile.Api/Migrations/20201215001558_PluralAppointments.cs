using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.BusinessProfile.Api.Migrations
{
    public partial class PluralAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Clients_ClientId",
                schema: "business",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Companies_CompanyId",
                schema: "business",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                schema: "business",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Appointment",
                schema: "business",
                newName: "Appointments",
                newSchema: "business");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_CompanyId",
                schema: "business",
                table: "Appointments",
                newName: "IX_Appointments_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_ClientId",
                schema: "business",
                table: "Appointments",
                newName: "IX_Appointments_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                schema: "business",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clients_ClientId",
                schema: "business",
                table: "Appointments",
                column: "ClientId",
                principalSchema: "business",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Companies_CompanyId",
                schema: "business",
                table: "Appointments",
                column: "CompanyId",
                principalSchema: "business",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clients_ClientId",
                schema: "business",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Companies_CompanyId",
                schema: "business",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                schema: "business",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                schema: "business",
                newName: "Appointment",
                newSchema: "business");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CompanyId",
                schema: "business",
                table: "Appointment",
                newName: "IX_Appointment_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ClientId",
                schema: "business",
                table: "Appointment",
                newName: "IX_Appointment_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                schema: "business",
                table: "Appointment",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Clients_ClientId",
                schema: "business",
                table: "Appointment",
                column: "ClientId",
                principalSchema: "business",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Companies_CompanyId",
                schema: "business",
                table: "Appointment",
                column: "CompanyId",
                principalSchema: "business",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
