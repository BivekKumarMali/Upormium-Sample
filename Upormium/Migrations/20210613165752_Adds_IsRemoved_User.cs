using Microsoft.EntityFrameworkCore.Migrations;

namespace Upormium.Migrations
{
    public partial class Adds_IsRemoved_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
