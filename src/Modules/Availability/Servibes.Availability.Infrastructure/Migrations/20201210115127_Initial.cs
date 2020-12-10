using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servibes.Availability.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "availability");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "availability",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "availability",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "availability",
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                schema: "availability",
                columns: table => new
                {
                    OpeningHoursId = table.Column<Guid>(nullable: false),
                    DayOfWeek = table.Column<string>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Start = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.OpeningHoursId);
                    table.ForeignKey(
                        name: "FK_OpeningHours_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "availability",
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "availability",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "availability",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffs",
                schema: "availability",
                columns: table => new
                {
                    TimeOffId = table.Column<Guid>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffs", x => x.TimeOffId);
                    table.ForeignKey(
                        name: "FK_TimeOffs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "availability",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                schema: "availability",
                columns: table => new
                {
                    WorkingHourId = table.Column<Guid>(nullable: false),
                    DayOfWeek = table.Column<string>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Start = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.WorkingHourId);
                    table.ForeignKey(
                        name: "FK_WorkingHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "availability",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                schema: "availability",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_CompanyId",
                schema: "availability",
                table: "OpeningHours",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EmployeeId",
                schema: "availability",
                table: "Reservations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffs_EmployeeId",
                schema: "availability",
                table: "TimeOffs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_EmployeeId",
                schema: "availability",
                table: "WorkingHours",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningHours",
                schema: "availability");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "availability");

            migrationBuilder.DropTable(
                name: "TimeOffs",
                schema: "availability");

            migrationBuilder.DropTable(
                name: "WorkingHours",
                schema: "availability");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "availability");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "availability");
        }
    }
}
