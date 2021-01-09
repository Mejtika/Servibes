using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Appointments.Infrastructure.Migrations
{
    public partial class LinkAppointmentWithClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "appointments",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "appointments",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "appointments",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ReserveeId",
                schema: "appointments",
                table: "Appointments",
                column: "ReserveeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clients_ReserveeId",
                schema: "appointments",
                table: "Appointments",
                column: "ReserveeId",
                principalSchema: "appointments",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clients_ReserveeId",
                schema: "appointments",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ReserveeId",
                schema: "appointments",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "appointments",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "appointments",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "appointments",
                table: "Clients");
        }
    }
}
