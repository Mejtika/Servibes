using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.BusinessProfile.Api.Migrations
{
    public partial class CategoriesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "business",
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("d8f762ca-b7a5-486d-a793-e142f856d103"), "Hair Salon" },
                    { new Guid("40b22590-3f08-4928-a86c-5c1d5d75d3bb"), "Barber" },
                    { new Guid("87467e7d-4185-4bd1-bc32-1a0f62f5726a"), "Massage" },
                    { new Guid("3ac00a98-2b01-46d5-99f2-546b9e302b18"), "Makeup" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "business",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("3ac00a98-2b01-46d5-99f2-546b9e302b18"));

            migrationBuilder.DeleteData(
                schema: "business",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("40b22590-3f08-4928-a86c-5c1d5d75d3bb"));

            migrationBuilder.DeleteData(
                schema: "business",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("87467e7d-4185-4bd1-bc32-1a0f62f5726a"));

            migrationBuilder.DeleteData(
                schema: "business",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("d8f762ca-b7a5-486d-a793-e142f856d103"));
        }
    }
}
