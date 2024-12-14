using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class stockMig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producthasColorAndProducthasSizeStocks");

            migrationBuilder.DropTable(
                name: "ProducthasSizes");

            migrationBuilder.CreateTable(
                name: "producthasColorAndSizeAndStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducthasColorId = table.Column<int>(type: "int", nullable: true),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producthasColorAndSizeAndStocks");

            migrationBuilder.CreateTable(
                name: "ProducthasSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducthasSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProducthasSizes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProducthasSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProducthasSizes_ProductId",
                table: "ProducthasSizes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProducthasSizes_SizeId",
                table: "ProducthasSizes",
                column: "SizeId");
        }
    }
}
