using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lessons.Infrastructure.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lessons");

            migrationBuilder.CreateTable(
                name: "Performances",
                schema: "lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                schema: "lessons",
                columns: table => new
                {
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    TimeCounter = table.Column<int>(type: "integer", nullable: false),
                    PerformenceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => new { x.UserId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_Lessons_Performances_PerformenceId",
                        column: x => x.PerformenceId,
                        principalSchema: "lessons",
                        principalTable: "Performances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_PerformenceId",
                schema: "lessons",
                table: "Lessons",
                column: "PerformenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessons",
                schema: "lessons");

            migrationBuilder.DropTable(
                name: "Performances",
                schema: "lessons");
        }
    }
}
