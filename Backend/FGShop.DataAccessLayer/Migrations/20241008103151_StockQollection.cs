using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class StockQollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_producthasStocks_ProductId",
                table: "producthasStocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_producthasStocks_StockId",
                table: "producthasStocks",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_producthasStocks_Products_ProductId",
                table: "producthasStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_producthasStocks_Stocks_StockId",
                table: "producthasStocks",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_producthasStocks_Products_ProductId",
                table: "producthasStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_producthasStocks_Stocks_StockId",
                table: "producthasStocks");

            migrationBuilder.DropIndex(
                name: "IX_producthasStocks_ProductId",
                table: "producthasStocks");

            migrationBuilder.DropIndex(
                name: "IX_producthasStocks_StockId",
                table: "producthasStocks");
        }
    }
}
