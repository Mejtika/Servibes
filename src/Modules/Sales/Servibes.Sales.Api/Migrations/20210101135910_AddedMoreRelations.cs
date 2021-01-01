using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Sales.Api.Migrations
{
    public partial class AddedMoreRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                schema: "sales",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                schema: "sales",
                table: "Appointments",
                column: "EmployeeId",
                principalSchema: "sales",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                schema: "sales",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_EmployeeId",
                schema: "sales",
                table: "Appointments");
        }
    }
}
