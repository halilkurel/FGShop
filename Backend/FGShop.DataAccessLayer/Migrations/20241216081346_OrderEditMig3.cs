using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class OrderEditMig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Orders");
        }
    }
}
