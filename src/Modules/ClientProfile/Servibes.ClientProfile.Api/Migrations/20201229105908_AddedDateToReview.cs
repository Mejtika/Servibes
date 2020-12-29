using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.ClientProfile.Api.Migrations
{
    public partial class AddedDateToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                schema: "client",
                table: "Reviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                schema: "client",
                table: "Reviews");
        }
    }
}
