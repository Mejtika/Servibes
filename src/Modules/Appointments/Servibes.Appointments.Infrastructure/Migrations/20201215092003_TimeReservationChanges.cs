using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Appointments.Infrastructure.Migrations
{
    public partial class TimeReservationChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                schema: "appointments",
                table: "TimeReservations");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "appointments",
                table: "TimeReservations",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "appointments",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "appointments",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(nullable: false),
                    WalkInId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "appointments");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "appointments");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "appointments",
                table: "TimeReservations");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                schema: "appointments",
                table: "TimeReservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
