using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StickyNotesDatabase.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "List<string>");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserDTO",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "StickyNotesDTO",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "LikedNotes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDTO",
                table: "UserDTO",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StickyNotesDTO",
                table: "StickyNotesDTO",
                column: "Author");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedNotes",
                table: "LikedNotes",
                column: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDTO",
                table: "UserDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StickyNotesDTO",
                table: "StickyNotesDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedNotes",
                table: "LikedNotes");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserDTO",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "StickyNotesDTO",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "LikedNotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "List<string>",
                columns: table => new
                {
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
