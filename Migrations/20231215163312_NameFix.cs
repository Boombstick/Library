using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class NameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelf1_BookCaseId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookshelf1_AllBookshelf_BooksShelfId",
                table: "Bookshelf1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookshelf1",
                table: "Bookshelf1");

            migrationBuilder.DropIndex(
                name: "IX_Bookshelf1_BooksShelfId",
                table: "Bookshelf1");

            migrationBuilder.DropColumn(
                name: "BooksShelfId",
                table: "Bookshelf1");

            migrationBuilder.RenameTable(
                name: "Bookshelf1",
                newName: "Bookshelf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookshelf",
                table: "Bookshelf",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookshelf_BookShelfId",
                table: "Bookshelf",
                column: "BookShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookshelf_BookCaseId",
                table: "Books",
                column: "BookCaseId",
                principalTable: "Bookshelf",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookshelf_AllBookshelf_BookShelfId",
                table: "Bookshelf",
                column: "BookShelfId",
                principalTable: "AllBookshelf",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelf_BookCaseId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookshelf_AllBookshelf_BookShelfId",
                table: "Bookshelf");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookshelf",
                table: "Bookshelf");

            migrationBuilder.DropIndex(
                name: "IX_Bookshelf_BookShelfId",
                table: "Bookshelf");

            migrationBuilder.RenameTable(
                name: "Bookshelf",
                newName: "Bookshelf1");

            migrationBuilder.AddColumn<int>(
                name: "BooksShelfId",
                table: "Bookshelf1",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookshelf1",
                table: "Bookshelf1",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookshelf1_BooksShelfId",
                table: "Bookshelf1",
                column: "BooksShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookshelf1_BookCaseId",
                table: "Books",
                column: "BookCaseId",
                principalTable: "Bookshelf1",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookshelf1_AllBookshelf_BooksShelfId",
                table: "Bookshelf1",
                column: "BooksShelfId",
                principalTable: "AllBookshelf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
