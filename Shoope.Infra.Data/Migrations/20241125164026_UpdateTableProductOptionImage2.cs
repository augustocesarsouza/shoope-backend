using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTableProductOptionImage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_product_option_images_FlashSaleProductImageAll_FlashSale~",
                table: "tb_product_option_images");

            migrationBuilder.DropTable(
                name: "FlashSaleProductImageAll");

            migrationBuilder.DropIndex(
                name: "IX_tb_product_option_images_FlashSaleProductImageAllId",
                table: "tb_product_option_images");

            migrationBuilder.DropColumn(
                name: "FlashSaleProductImageAllId",
                table: "tb_product_option_images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FlashSaleProductImageAllId",
                table: "tb_product_option_images",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FlashSaleProductImageAll",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsOfferFlashId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashSaleProductImageAll", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_option_images_FlashSaleProductImageAllId",
                table: "tb_product_option_images",
                column: "FlashSaleProductImageAllId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_product_option_images_FlashSaleProductImageAll_FlashSale~",
                table: "tb_product_option_images",
                column: "FlashSaleProductImageAllId",
                principalTable: "FlashSaleProductImageAll",
                principalColumn: "Id");
        }
    }
}
