using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDAPP.Migrations
{
    public partial class registaertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "groupId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Fund",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "groupId",
                table: "Fund",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "groupId",
                table: "Expenses",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "groupId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Fund");

            migrationBuilder.DropColumn(
                name: "groupId",
                table: "Fund");

            migrationBuilder.DropColumn(
                name: "groupId",
                table: "Expenses");
        }
    }
}
