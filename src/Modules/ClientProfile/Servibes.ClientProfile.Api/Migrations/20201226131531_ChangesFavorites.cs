using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.ClientProfile.Api.Migrations
{
    public partial class ChangesFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Clients_ClientId",
                schema: "client",
                table: "Favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                schema: "client",
                table: "Favorite");

            migrationBuilder.RenameTable(
                name: "Favorite",
                schema: "client",
                newName: "Favorites",
                newSchema: "client");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_ClientId",
                schema: "client",
                table: "Favorites",
                newName: "IX_Favorites_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                schema: "client",
                table: "Favorites",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Clients_ClientId",
                schema: "client",
                table: "Favorites",
                column: "ClientId",
                principalSchema: "client",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Clients_ClientId",
                schema: "client",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                schema: "client",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                schema: "client",
                newName: "Favorite",
                newSchema: "client");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_ClientId",
                schema: "client",
                table: "Favorite",
                newName: "IX_Favorite_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                schema: "client",
                table: "Favorite",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Clients_ClientId",
                schema: "client",
                table: "Favorite",
                column: "ClientId",
                principalSchema: "client",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
