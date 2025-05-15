using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDAPP.Migrations
{
    public partial class exp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "festivalId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fundForYear",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "festivalId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "fundForYear",
                table: "Expenses");
        }
    }
}
