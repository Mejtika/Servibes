using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Sales.Api.Migrations
{
    public partial class RefactoredSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                schema: "sales",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "sales",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "sales",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "sales",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "WalkInClients",
                schema: "sales",
                columns: table => new
                {
                    WalkInClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkInClients", x => x.WalkInClientId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CompanyId",
                schema: "sales",
                table: "Appointments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Companies_CompanyId",
                schema: "sales",
                table: "Appointments",
                column: "CompanyId",
                principalSchema: "sales",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Companies_CompanyId",
                schema: "sales",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "WalkInClients",
                schema: "sales");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CompanyId",
                schema: "sales",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                schema: "sales",
                table: "Appointments");
        }
    }
}
