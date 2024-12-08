using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreatingNewsPropertyTableFlashSaleProductAllInfo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Voltage",
                table: "tb_flash_sale_product_all_infos",
                newName: "voltage");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "tb_flash_sale_product_all_infos",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "Coins",
                table: "tb_flash_sale_product_all_infos",
                newName: "coins");

            migrationBuilder.RenameColumn(
                name: "QuantityPiece",
                table: "tb_flash_sale_product_all_infos",
                newName: "quantity_piece");

            migrationBuilder.RenameColumn(
                name: "CreditCard",
                table: "tb_flash_sale_product_all_infos",
                newName: "credit_card");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "voltage",
                table: "tb_flash_sale_product_all_infos",
                newName: "Voltage");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "tb_flash_sale_product_all_infos",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "coins",
                table: "tb_flash_sale_product_all_infos",
                newName: "Coins");

            migrationBuilder.RenameColumn(
                name: "quantity_piece",
                table: "tb_flash_sale_product_all_infos",
                newName: "QuantityPiece");

            migrationBuilder.RenameColumn(
                name: "credit_card",
                table: "tb_flash_sale_product_all_infos",
                newName: "CreditCard");
        }
    }
}
