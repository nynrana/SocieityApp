using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDAPP.Migrations
{
    public partial class newfileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "givenBy",
                table: "Fund",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "givenTo",
                table: "Fund",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "givenBy",
                table: "Fund");

            migrationBuilder.DropColumn(
                name: "givenTo",
                table: "Fund");
        }
    }
}
