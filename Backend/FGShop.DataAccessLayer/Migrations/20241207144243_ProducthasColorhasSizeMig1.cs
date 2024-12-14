using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class ProducthasColorhasSizeMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "producthasColorAndSizeAndStocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "producthasColorAndSizeAndStocks",
                type: "int",
                nullable: true);
        }
    }
}
