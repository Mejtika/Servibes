using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.ClientProfile.Api.Migrations
{
    public partial class AddedUniquePKToFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                schema: "client",
                table: "Favorites");

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteId",
                schema: "client",
                table: "Favorites",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                schema: "client",
                table: "Favorites",
                column: "FavoriteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                schema: "client",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                schema: "client",
                table: "Favorites");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                schema: "client",
                table: "Favorites",
                column: "CompanyId");
        }
    }
}
