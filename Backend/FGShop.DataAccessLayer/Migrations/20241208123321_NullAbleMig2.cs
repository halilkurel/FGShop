using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGShop.DataAccessLayer.Migrations
{
    public partial class NullAbleMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasCategories_Categories_CategoryId",
                table: "ProducthasCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasCategories_Products_ProductId",
                table: "ProducthasCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_producthasColorAndSizehasStocks_producthasColorAndSizes_ProducthasColorAndSizeId",
                table: "producthasColorAndSizehasStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_producthasColorAndSizes_ProducthasColors_ProducthasColorId",
                table: "producthasColorAndSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_producthasColorAndSizes_Sizes_SizeId",
                table: "producthasColorAndSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasColors_Colors_ColorId",
                table: "ProducthasColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasColors_Products_ProductId",
                table: "ProducthasColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasImages_Images_ImageId",
                table: "ProducthasImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasImages_Products_ProductId",
                table: "ProducthasImages");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducthasImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ProducthasImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducthasColors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "ProducthasColors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "producthasColorAndSizes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProducthasColorId",
                table: "producthasColorAndSizes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "producthasColorAndSizehasStocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProducthasColorAndSizeId",
                table: "producthasColorAndSizehasStocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducthasCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProducthasCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasCategories_Categories_CategoryId",
                table: "ProducthasCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasCategories_Products_ProductId",
                table: "ProducthasCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_producthasColorAndSizehasStocks_producthasColorAndSizes_ProducthasColorAndSizeId",
                table: "producthasColorAndSizehasStocks",
                column: "ProducthasColorAndSizeId",
                principalTable: "producthasColorAndSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_producthasColorAndSizes_ProducthasColors_ProducthasColorId",
                table: "producthasColorAndSizes",
                column: "ProducthasColorId",
                principalTable: "ProducthasColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_producthasColorAndSizes_Sizes_SizeId",
                table: "producthasColorAndSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasColors_Colors_ColorId",
                table: "ProducthasColors",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasColors_Products_ProductId",
                table: "ProducthasColors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasImages_Images_ImageId",
                table: "ProducthasImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasImages_Products_ProductId",
                table: "ProducthasImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasCategories_Categories_CategoryId",
                table: "ProducthasCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasCategories_Products_ProductId",
                table: "ProducthasCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_producthasColorAndSizehasStocks_producthasColorAndSizes_ProducthasColorAndSizeId",
                table: "producthasColorAndSizehasStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_producthasColorAndSizes_ProducthasColors_ProducthasColorId",
                table: "producthasColorAndSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_producthasColorAndSizes_Sizes_SizeId",
                table: "producthasColorAndSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasColors_Colors_ColorId",
                table: "ProducthasColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasColors_Products_ProductId",
                table: "ProducthasColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasImages_Images_ImageId",
                table: "ProducthasImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducthasImages_Products_ProductId",
                table: "ProducthasImages");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducthasImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ProducthasImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducthasColors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "ProducthasColors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "producthasColorAndSizes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProducthasColorId",
                table: "producthasColorAndSizes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "producthasColorAndSizehasStocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProducthasColorAndSizeId",
                table: "producthasColorAndSizehasStocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducthasCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProducthasCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasCategories_Categories_CategoryId",
                table: "ProducthasCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasCategories_Products_ProductId",
                table: "ProducthasCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_producthasColorAndSizehasStocks_producthasColorAndSizes_ProducthasColorAndSizeId",
                table: "producthasColorAndSizehasStocks",
                column: "ProducthasColorAndSizeId",
                principalTable: "producthasColorAndSizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_producthasColorAndSizes_ProducthasColors_ProducthasColorId",
                table: "producthasColorAndSizes",
                column: "ProducthasColorId",
                principalTable: "ProducthasColors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_producthasColorAndSizes_Sizes_SizeId",
                table: "producthasColorAndSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasColors_Colors_ColorId",
                table: "ProducthasColors",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasColors_Products_ProductId",
                table: "ProducthasColors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasImages_Images_ImageId",
                table: "ProducthasImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducthasImages_Products_ProductId",
                table: "ProducthasImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
