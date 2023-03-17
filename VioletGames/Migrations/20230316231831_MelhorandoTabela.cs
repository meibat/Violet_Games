using Microsoft.EntityFrameworkCore.Migrations;

namespace VioletGames.Migrations
{
    public partial class MelhorandoTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "payment",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "plan",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "planValue",
                table: "Clientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payment",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "plan",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "planValue",
                table: "Clientes");
        }
    }
}
