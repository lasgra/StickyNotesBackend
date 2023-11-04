using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StickyNotesDatabase.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedNotes",
                table: "LikedNotes");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "LikedNotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "LikedNotes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedNotes",
                table: "LikedNotes",
                column: "Author");
        }
    }
}
