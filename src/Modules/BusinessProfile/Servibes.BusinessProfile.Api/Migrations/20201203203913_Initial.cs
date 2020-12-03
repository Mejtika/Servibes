using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.BusinessProfile.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "business");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "business",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    CoverPhoto = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "business",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "business",
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                schema: "business",
                columns: table => new
                {
                    OpeningHourId = table.Column<Guid>(nullable: false),
                    Monday_IsAvailable = table.Column<bool>(nullable: true),
                    Monday_Start = table.Column<TimeSpan>(nullable: true),
                    Monday_End = table.Column<TimeSpan>(nullable: true),
                    Tuesday_IsAvailable = table.Column<bool>(nullable: true),
                    Tuesday_Start = table.Column<TimeSpan>(nullable: true),
                    Tuesday_End = table.Column<TimeSpan>(nullable: true),
                    Wednesday_IsAvailable = table.Column<bool>(nullable: true),
                    Wednesday_Start = table.Column<TimeSpan>(nullable: true),
                    Wednesday_End = table.Column<TimeSpan>(nullable: true),
                    Thursday_IsAvailable = table.Column<bool>(nullable: true),
                    Thursday_Start = table.Column<TimeSpan>(nullable: true),
                    Thursday_End = table.Column<TimeSpan>(nullable: true),
                    Friday_IsAvailable = table.Column<bool>(nullable: true),
                    Friday_Start = table.Column<TimeSpan>(nullable: true),
                    Friday_End = table.Column<TimeSpan>(nullable: true),
                    Saturday_IsAvailable = table.Column<bool>(nullable: true),
                    Saturday_Start = table.Column<TimeSpan>(nullable: true),
                    Saturday_End = table.Column<TimeSpan>(nullable: true),
                    Sunday_IsAvailable = table.Column<bool>(nullable: true),
                    Sunday_Start = table.Column<TimeSpan>(nullable: true),
                    Sunday_End = table.Column<TimeSpan>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.OpeningHourId);
                    table.ForeignKey(
                        name: "FK_OpeningHours_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "business",
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "business",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(nullable: false),
                    ServiceName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Services_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "business",
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                schema: "business",
                columns: table => new
                {
                    WorkingHoursId = table.Column<Guid>(nullable: false),
                    Monday_IsAvailable = table.Column<bool>(nullable: true),
                    Monday_Start = table.Column<TimeSpan>(nullable: true),
                    Monday_End = table.Column<TimeSpan>(nullable: true),
                    Tuesday_IsAvailable = table.Column<bool>(nullable: true),
                    Tuesday_Start = table.Column<TimeSpan>(nullable: true),
                    Tuesday_End = table.Column<TimeSpan>(nullable: true),
                    Wednesday_IsAvailable = table.Column<bool>(nullable: true),
                    Wednesday_Start = table.Column<TimeSpan>(nullable: true),
                    Wednesday_End = table.Column<TimeSpan>(nullable: true),
                    Thursday_IsAvailable = table.Column<bool>(nullable: true),
                    Thursday_Start = table.Column<TimeSpan>(nullable: true),
                    Thursday_End = table.Column<TimeSpan>(nullable: true),
                    Friday_IsAvailable = table.Column<bool>(nullable: true),
                    Friday_Start = table.Column<TimeSpan>(nullable: true),
                    Friday_End = table.Column<TimeSpan>(nullable: true),
                    Saturday_IsAvailable = table.Column<bool>(nullable: true),
                    Saturday_Start = table.Column<TimeSpan>(nullable: true),
                    Saturday_End = table.Column<TimeSpan>(nullable: true),
                    Sunday_IsAvailable = table.Column<bool>(nullable: true),
                    Sunday_Start = table.Column<TimeSpan>(nullable: true),
                    Sunday_End = table.Column<TimeSpan>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.WorkingHoursId);
                    table.ForeignKey(
                        name: "FK_WorkingHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "business",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                schema: "business",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PerformerId = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performers_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "business",
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                schema: "business",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_CompanyId",
                schema: "business",
                table: "OpeningHours",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Performers_ServiceId",
                schema: "business",
                table: "Performers",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CompanyId",
                schema: "business",
                table: "Services",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_EmployeeId",
                schema: "business",
                table: "WorkingHours",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningHours",
                schema: "business");

            migrationBuilder.DropTable(
                name: "Performers",
                schema: "business");

            migrationBuilder.DropTable(
                name: "WorkingHours",
                schema: "business");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "business");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "business");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "business");
        }
    }
}
