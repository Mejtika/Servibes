using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.BusinessProfile.Api.Migrations
{
    public partial class ReviewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "business",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StarsCount = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "business",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "business",
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                schema: "business",
                table: "Reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyId",
                schema: "business",
                table: "Reviews",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "business");
        }
    }
}
