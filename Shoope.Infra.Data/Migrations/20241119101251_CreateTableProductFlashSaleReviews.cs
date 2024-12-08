using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableProductFlashSaleReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_product_flash_sale_reviews",
                columns: table => new
                {
                    product_flash_sale_reviews_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cost_benefit = table.Column<string>(type: "text", nullable: false),
                    similar_to_ad = table.Column<string>(type: "text", nullable: false),
                    star_quantity = table.Column<int>(type: "integer", nullable: false),
                    products_offer_flash_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_flash_sale_reviews", x => x.product_flash_sale_reviews_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_flash_sale_reviews");
        }
    }
}
