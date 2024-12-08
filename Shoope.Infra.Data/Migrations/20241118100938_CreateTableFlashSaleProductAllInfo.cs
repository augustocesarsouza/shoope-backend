using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableFlashSaleProductAllInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_flash_sale_product_all_infos",
                columns: table => new
                {
                    flash_sale_product_all_infos_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_flash_sale_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_reviews_rate = table.Column<double>(type: "double precision", nullable: false),
                    quantity_sold = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flash_sale_product_all_infos", x => x.flash_sale_product_all_infos_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_flash_sale_product_all_infos");
        }
    }
}
