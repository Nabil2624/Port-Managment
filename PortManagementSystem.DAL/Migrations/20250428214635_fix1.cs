using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EDT",
                table: "Ships",
                newName: "EDTDate");

            migrationBuilder.RenameColumn(
                name: "EAT",
                table: "Ships",
                newName: "EATDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EDTDate",
                table: "Ships",
                newName: "EDT");

            migrationBuilder.RenameColumn(
                name: "EATDate",
                table: "Ships",
                newName: "EAT");
        }
    }
}
