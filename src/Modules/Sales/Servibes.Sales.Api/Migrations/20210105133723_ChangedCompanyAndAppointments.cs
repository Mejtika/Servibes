using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Sales.Api.Migrations
{
    public partial class ChangedCompanyAndAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "sales",
                table: "Companies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WalkInClientId",
                schema: "sales",
                table: "Companies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                schema: "sales",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                schema: "sales",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "sales",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WalkInClientId",
                schema: "sales",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "End",
                schema: "sales",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Start",
                schema: "sales",
                table: "Appointments");
        }
    }
}
