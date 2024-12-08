using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_products_offer_flash",
                columns: table => new
                {
                    products_offer_flash_id = table.Column<Guid>(type: "uuid", nullable: false),
                    img_product = table.Column<string>(type: "text", nullable: false),
                    img_product_public_id = table.Column<string>(type: "text", nullable: false),
                    alt_value = table.Column<string>(type: "text", nullable: false),
                    img_part_bottom = table.Column<string>(type: "text", nullable: true),
                    img_part_bottom_public_id = table.Column<string>(type: "text", nullable: true),
                    price_product = table.Column<double>(type: "double precision", nullable: false),
                    popularity_percentage = table.Column<int>(type: "integer", nullable: false),
                    discount_percentage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products_offer_flash", x => x.products_offer_flash_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_products_offer_flash");
        }
    }
}
