using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class newstockMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producthasStocks");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.CreateTable(
                name: "producthasColorAndProducthasSizeStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducthasColorId = table.Column<int>(type: "int", nullable: true),
                    ProducthasSizeId = table.Column<int>(type: "int", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producthasColorAndProducthasSizeStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_producthasColorAndProducthasSizeStocks_ProducthasColors_ProducthasColorId",
                        column: x => x.ProducthasColorId,
                        principalTable: "ProducthasColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_producthasColorAndProducthasSizeStocks_ProducthasSizes_ProducthasSizeId",
                        column: x => x.ProducthasSizeId,
                        principalTable: "ProducthasSizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_producthasColorAndProducthasSizeStocks_ProducthasColorId",
                table: "producthasColorAndProducthasSizeStocks",
                column: "ProducthasColorId");

            migrationBuilder.CreateIndex(
                name: "IX_producthasColorAndProducthasSizeStocks_ProducthasSizeId",
                table: "producthasColorAndProducthasSizeStocks",
                column: "ProducthasSizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producthasColorAndProducthasSizeStocks");

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "producthasStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producthasStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_producthasStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producthasStocks_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_producthasStocks_ProductId",
                table: "producthasStocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_producthasStocks_StockId",
                table: "producthasStocks",
                column: "StockId");
        }
    }
}
