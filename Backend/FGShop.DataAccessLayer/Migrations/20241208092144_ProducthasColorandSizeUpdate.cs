using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class ProducthasColorandSizeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producthasColorAndSizeAndStocks");

            migrationBuilder.CreateTable(
                name: "producthasColorAndSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducthasColorId = table.Column<int>(type: "int", nullable: true),
                    SizeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producthasColorAndSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_producthasColorAndSizes_ProducthasColors_ProducthasColorId",
                        column: x => x.ProducthasColorId,
                        principalTable: "ProducthasColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_producthasColorAndSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_producthasColorAndSizes_ProducthasColorId",
                table: "producthasColorAndSizes",
                column: "ProducthasColorId");

            migrationBuilder.CreateIndex(
                name: "IX_producthasColorAndSizes_SizeId",
                table: "producthasColorAndSizes",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producthasColorAndSizes");

            migrationBuilder.CreateTable(
                name: "producthasColorAndSizeAndStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducthasColorId = table.Column<int>(type: "int", nullable: true),
                    SizeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producthasColorAndSizeAndStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_producthasColorAndSizeAndStocks_ProducthasColors_ProducthasColorId",
                        column: x => x.ProducthasColorId,
                        principalTable: "ProducthasColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_producthasColorAndSizeAndStocks_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_producthasColorAndSizeAndStocks_ProducthasColorId",
                table: "producthasColorAndSizeAndStocks",
                column: "ProducthasColorId");

            migrationBuilder.CreateIndex(
                name: "IX_producthasColorAndSizeAndStocks_SizeId",
                table: "producthasColorAndSizeAndStocks",
                column: "SizeId");
        }
    }
}
