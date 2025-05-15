using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDAPP.Migrations
{
    public partial class addfestive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "festivalId",
                table: "Fund",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fundForYear",
                table: "Fund",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Festival",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Festival");

            migrationBuilder.DropColumn(
                name: "festivalId",
                table: "Fund");

            migrationBuilder.DropColumn(
                name: "fundForYear",
                table: "Fund");
        }
    }
}
