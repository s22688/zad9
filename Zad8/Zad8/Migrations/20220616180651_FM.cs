using Microsoft.EntityFrameworkCore.Migrations;

namespace Zad8.Migrations
{
    public partial class FM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "SystemUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemUsers",
                table: "SystemUsers",
                column: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemUsers",
                table: "SystemUsers");

            migrationBuilder.RenameTable(
                name: "SystemUsers",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Login");
        }
    }
}
