using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Appointments.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "appointments");

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    ReserveeId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    ServicePrice = table.Column<decimal>(nullable: true),
                    Start = table.Column<DateTime>(nullable: true),
                    End = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                });

            migrationBuilder.CreateTable(
                name: "TimeReservations",
                schema: "appointments",
                columns: table => new
                {
                    TimeReservationId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    Start = table.Column<DateTime>(nullable: true),
                    End = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeReservations", x => x.TimeReservationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "appointments");

            migrationBuilder.DropTable(
                name: "TimeReservations",
                schema: "appointments");
        }
    }
}
