using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.ClientProfile.Api.Migrations
{
    public partial class ChangePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                schema: "client",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                schema: "client",
                table: "Favorite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                schema: "client",
                table: "Favorite",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                schema: "client",
                table: "Favorite");

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteId",
                schema: "client",
                table: "Favorite",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                schema: "client",
                table: "Favorite",
                column: "FavoriteId");
        }
    }
}
