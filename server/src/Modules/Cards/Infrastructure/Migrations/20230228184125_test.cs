using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cards.Infrastructure.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cards");

            migrationBuilder.CreateTable(
                name: "Owners",
                schema: "cards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sides",
                schema: "cards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: true),
                    Example = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "cards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Front = table.Column<string>(type: "text", nullable: false),
                    Back = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "cards",
                        principalTable: "Owners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                schema: "cards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FrontId = table.Column<long>(type: "bigint", nullable: true),
                    BackId = table.Column<long>(type: "bigint", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "cards",
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Sides_BackId",
                        column: x => x.BackId,
                        principalSchema: "cards",
                        principalTable: "Sides",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Sides_FrontId",
                        column: x => x.FrontId,
                        principalSchema: "cards",
                        principalTable: "Sides",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Details",
                schema: "cards",
                columns: table => new
                {
                    SideType = table.Column<int>(type: "integer", nullable: false),
                    CardId = table.Column<long>(type: "bigint", nullable: false),
                    Drawer = table.Column<int>(type: "SMALLINT", nullable: false),
                    Counter = table.Column<int>(type: "SMALLINT", nullable: false),
                    IsQuestion = table.Column<bool>(type: "boolean", nullable: false),
                    IsTicked = table.Column<bool>(type: "boolean", nullable: false),
                    NextRepeat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => new { x.CardId, x.SideType });
                    table.ForeignKey(
                        name: "FK_Details_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "cards",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BackId",
                schema: "cards",
                table: "Cards",
                column: "BackId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_FrontId",
                schema: "cards",
                table: "Cards",
                column: "FrontId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GroupId",
                schema: "cards",
                table: "Cards",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Id",
                schema: "cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Details_CardId_SideType",
                schema: "cards",
                table: "Details",
                columns: new[] { "CardId", "SideType" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Id",
                schema: "cards",
                table: "Groups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OwnerId",
                schema: "cards",
                table: "Groups",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Id",
                schema: "cards",
                table: "Owners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sides_Id",
                schema: "cards",
                table: "Sides",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details",
                schema: "cards");

            migrationBuilder.DropTable(
                name: "Cards",
                schema: "cards");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "cards");

            migrationBuilder.DropTable(
                name: "Sides",
                schema: "cards");

            migrationBuilder.DropTable(
                name: "Owners",
                schema: "cards");
        }
    }
}
