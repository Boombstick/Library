using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class AlterReaderProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Readers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Readers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Readers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Readers",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Readers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
