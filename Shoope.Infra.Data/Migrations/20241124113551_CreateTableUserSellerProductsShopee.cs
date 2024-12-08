using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableUserSellerProductsShopee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_user_seller_products",
                columns: table => new
                {
                    user_seller_products_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    img_perfil = table.Column<string>(type: "text", nullable: false),
                    img_perfil_public_id = table.Column<string>(type: "text", nullable: false),
                    img_floating = table.Column<string>(type: "text", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reviews = table.Column<double>(type: "double precision", nullable: true),
                    chat_response_rate = table.Column<double>(type: "double precision", nullable: true),
                    account_creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantity_of_product_sold = table.Column<double>(type: "double precision", nullable: true),
                    usually_responds_to_chat_in = table.Column<string>(type: "text", nullable: true),
                    followers = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_seller_products", x => x.user_seller_products_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_user_seller_products");
        }
    }
}
