using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Sales.Api.Migrations
{
    public partial class RemovedWalkInClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalkInClients",
                schema: "sales");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ReserveeId",
                schema: "sales",
                table: "Appointments",
                column: "ReserveeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clients_ReserveeId",
                schema: "sales",
                table: "Appointments",
                column: "ReserveeId",
                principalSchema: "sales",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clients_ReserveeId",
                schema: "sales",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ReserveeId",
                schema: "sales",
                table: "Appointments");

            migrationBuilder.CreateTable(
                name: "WalkInClients",
                schema: "sales",
                columns: table => new
                {
                    WalkInClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkInClients", x => x.WalkInClientId);
                });
        }
    }
}
