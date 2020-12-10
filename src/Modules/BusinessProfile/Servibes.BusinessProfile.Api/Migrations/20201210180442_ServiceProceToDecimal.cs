using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.BusinessProfile.Api.Migrations
{
    public partial class ServiceProceToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "business",
                table: "Services",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "business",
                table: "Services",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
