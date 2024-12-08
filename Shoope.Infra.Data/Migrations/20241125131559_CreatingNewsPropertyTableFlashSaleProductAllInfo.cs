using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreatingNewsPropertyTableFlashSaleProductAllInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Coins",
                table: "tb_flash_sale_product_all_infos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCard",
                table: "tb_flash_sale_product_all_infos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityPiece",
                table: "tb_flash_sale_product_all_infos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "tb_flash_sale_product_all_infos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Voltage",
                table: "tb_flash_sale_product_all_infos",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coins",
                table: "tb_flash_sale_product_all_infos");

            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "tb_flash_sale_product_all_infos");

            migrationBuilder.DropColumn(
                name: "QuantityPiece",
                table: "tb_flash_sale_product_all_infos");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "tb_flash_sale_product_all_infos");

            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "tb_flash_sale_product_all_infos");
        }
    }
}
