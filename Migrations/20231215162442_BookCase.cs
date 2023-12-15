using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class BookCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelf_BookShelfId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Bookshelf");

            migrationBuilder.RenameColumn(
                name: "BookShelfId",
                table: "Books",
                newName: "BookCaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BookShelfId",
                table: "Books",
                newName: "IX_Books_BookCaseId");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfReading",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AllBookshelf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OccupiedСells = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllBookshelf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookshelf1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    row = table.Column<int>(type: "int", nullable: false),
                    column = table.Column<int>(type: "int", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmpty = table.Column<bool>(type: "bit", nullable: false),
                    BookShelfId = table.Column<int>(type: "int", nullable: true),
                    BooksShelfId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshelf1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookshelf1_AllBookshelf_BooksShelfId",
                        column: x => x.BooksShelfId,
                        principalTable: "AllBookshelf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelf1_BookCaseId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Bookshelf1");

            migrationBuilder.DropTable(
                name: "AllBookshelf");

            migrationBuilder.DropColumn(
                name: "NumberOfReading",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BookCaseId",
                table: "Books",
                newName: "BookShelfId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BookCaseId",
                table: "Books",
                newName: "IX_Books_BookShelfId");

            migrationBuilder.CreateTable(
                name: "Bookshelf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshelf", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookshelf_BookShelfId",
                table: "Books",
                column: "BookShelfId",
                principalTable: "Bookshelf",
                principalColumn: "Id");
        }
    }
}
