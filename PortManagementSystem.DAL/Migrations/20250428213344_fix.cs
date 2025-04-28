using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EDTDate",
                table: "Ships",
                newName: "EDT");

            migrationBuilder.RenameColumn(
                name: "EATDate",
                table: "Ships",
                newName: "EAT");

            migrationBuilder.CreateTable(
                name: "TempTerminals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempTerminals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TempShips",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cargoType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EATDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EDTDate = table.Column<DateOnly>(type: "date", nullable: false),
                    destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    length = table.Column<double>(type: "float", nullable: false),
                    width = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tempTerminalId = table.Column<int>(type: "int", nullable: true),
                    tempUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempShips", x => x.id);
                    table.ForeignKey(
                        name: "FK_TempShips_TempTerminals_tempTerminalId",
                        column: x => x.tempTerminalId,
                        principalTable: "TempTerminals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempShips_Users_tempUserId",
                        column: x => x.tempUserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempShipsWaiting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shipId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cargoType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EATDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EDTDate = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempShipsWaiting", x => x.id);
                    table.ForeignKey(
                        name: "FK_TempShipsWaiting_TempShips_shipId",
                        column: x => x.shipId,
                        principalTable: "TempShips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TempShips_tempTerminalId",
                table: "TempShips",
                column: "tempTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_TempShips_tempUserId",
                table: "TempShips",
                column: "tempUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TempShipsWaiting_shipId",
                table: "TempShipsWaiting",
                column: "shipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempShipsWaiting");

            migrationBuilder.DropTable(
                name: "TempShips");

            migrationBuilder.DropTable(
                name: "TempTerminals");

            migrationBuilder.RenameColumn(
                name: "EDT",
                table: "Ships",
                newName: "EDTDate");

            migrationBuilder.RenameColumn(
                name: "EAT",
                table: "Ships",
                newName: "EATDate");
        }
    }
}
