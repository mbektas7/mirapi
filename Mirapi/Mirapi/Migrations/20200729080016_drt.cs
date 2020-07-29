using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mirapi.Migrations
{
    public partial class drt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "carId",
                table: "Test",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_carId",
                table: "Test",
                column: "carId");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Cars_carId",
                table: "Test",
                column: "carId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Cars_carId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_carId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "carId",
                table: "Test");
        }
    }
}
