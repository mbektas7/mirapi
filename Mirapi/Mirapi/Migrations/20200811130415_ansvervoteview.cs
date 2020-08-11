using Microsoft.EntityFrameworkCore.Migrations;

namespace Mirapi.Migrations
{
    public partial class ansvervoteview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "answer",
                table: "Post",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "view",
                table: "Post",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "vote",
                table: "Post",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "answer",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "view",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "vote",
                table: "Post");
        }
    }
}
