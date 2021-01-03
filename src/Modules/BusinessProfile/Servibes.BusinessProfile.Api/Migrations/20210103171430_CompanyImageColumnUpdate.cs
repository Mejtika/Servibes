using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.BusinessProfile.Api.Migrations
{
    public partial class CompanyImageColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPhoto",
                schema: "business",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverPhotoId",
                schema: "business",
                table: "Companies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPhotoId",
                schema: "business",
                table: "Companies");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverPhoto",
                schema: "business",
                table: "Companies",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
