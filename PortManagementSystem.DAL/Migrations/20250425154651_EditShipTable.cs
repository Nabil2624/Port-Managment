using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditShipTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Terminals_terminalId",
                table: "Ships");

            migrationBuilder.AlterColumn<int>(
                name: "terminalId",
                table: "Ships",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Terminals_terminalId",
                table: "Ships",
                column: "terminalId",
                principalTable: "Terminals",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Terminals_terminalId",
                table: "Ships");

            migrationBuilder.AlterColumn<int>(
                name: "terminalId",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Terminals_terminalId",
                table: "Ships",
                column: "terminalId",
                principalTable: "Terminals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
