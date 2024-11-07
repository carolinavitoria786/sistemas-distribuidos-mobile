using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookOnTable.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorColumnToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDePublicação",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Título",
                table: "Book",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Sinopse",
                table: "Book",
                newName: "PublishDate");

            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Book",
                newName: "Key");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Book",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Book",
                newName: "Título");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Book",
                newName: "Sinopse");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Book",
                newName: "Autor");

            migrationBuilder.AddColumn<int>(
                name: "DataDePublicação",
                table: "Book",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
