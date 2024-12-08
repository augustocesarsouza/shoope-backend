using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTableProductOptionImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "tb_product_option_images",
                columns: table => new
                {
                    product_option_images_id = table.Column<Guid>(type: "uuid", nullable: false),
                    option_name = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    public_id = table.Column<string>(type: "text", nullable: false),
                    products_offer_flash_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlashSaleProductImageAllId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_option_images", x => x.product_option_images_id);
                    table.ForeignKey(
                        name: "FK_tb_product_option_images_FlashSaleProductImageAll_FlashSale~",
                        column: x => x.FlashSaleProductImageAllId,
                        principalTable: "FlashSaleProductImageAll",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_option_images_FlashSaleProductImageAllId",
                table: "tb_product_option_images",
                column: "FlashSaleProductImageAllId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_option_images");

            migrationBuilder.DropTable(
                name: "FlashSaleProductImageAll");
        }
    }
}
