using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDAPP.Migrations
{
    public partial class fundname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "givenByName",
                table: "Fund",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "givenToName",
                table: "Fund",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "givenByName",
                table: "Fund");

            migrationBuilder.DropColumn(
                name: "givenToName",
                table: "Fund");
        }
    }
}
