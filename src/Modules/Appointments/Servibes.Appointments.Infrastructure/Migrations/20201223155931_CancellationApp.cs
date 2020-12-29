using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Appointments.Infrastructure.Migrations
{
    public partial class CancellationApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                schema: "appointments",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationReason",
                schema: "appointments",
                table: "Appointments");
        }
    }
}
