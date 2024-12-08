using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTableFlashSaleProductAllInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_flash_sale_id",
                table: "tb_flash_sale_product_all_infos",
                newName: "products_offer_flash_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_flash_sale_product_all_infos_products_offer_flash_id",
                table: "tb_flash_sale_product_all_infos",
                column: "products_offer_flash_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_flash_sale_product_all_infos_tb_products_offer_flash_pro~",
                table: "tb_flash_sale_product_all_infos",
                column: "products_offer_flash_id",
                principalTable: "tb_products_offer_flash",
                principalColumn: "products_offer_flash_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_flash_sale_product_all_infos_tb_products_offer_flash_pro~",
                table: "tb_flash_sale_product_all_infos");

            migrationBuilder.DropIndex(
                name: "IX_tb_flash_sale_product_all_infos_products_offer_flash_id",
                table: "tb_flash_sale_product_all_infos");

            migrationBuilder.RenameColumn(
                name: "products_offer_flash_id",
                table: "tb_flash_sale_product_all_infos",
                newName: "product_flash_sale_id");
        }
    }
}
