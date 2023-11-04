using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StickyNotesDatabase.Migrations
{
    /// <inheritdoc />
    public partial class final4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "LikedNotes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Users",
                table: "LikedNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedNotes",
                table: "LikedNotes",
                column: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedNotes",
                table: "LikedNotes");

            migrationBuilder.DropColumn(
                name: "Users",
                table: "LikedNotes");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "LikedNotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
