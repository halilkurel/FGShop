using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class ProducthasColorandSizehasStockMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "producthasColorAndSizehasStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducthasColorAndSizeId = table.Column<int>(type: "int", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producthasColorAndSizehasStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_producthasColorAndSizehasStocks_producthasColorAndSizes_ProducthasColorAndSizeId",
                        column: x => x.ProducthasColorAndSizeId,
                        principalTable: "producthasColorAndSizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_producthasColorAndSizehasStocks_ProducthasColorAndSizeId",
                table: "producthasColorAndSizehasStocks",
                column: "ProducthasColorAndSizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producthasColorAndSizehasStocks");
        }
    }
}
