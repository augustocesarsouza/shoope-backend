using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableProductSeller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_product_sellers",
                columns: table => new
                {
                    product_sellers_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_seller_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_sellers", x => x.product_sellers_id);
                    table.ForeignKey(
                        name: "FK_tb_product_sellers_tb_user_seller_products_user_seller_prod~",
                        column: x => x.user_seller_product_id,
                        principalTable: "tb_user_seller_products",
                        principalColumn: "user_seller_products_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_seller_user_seller_product_id",
                table: "tb_product_sellers",
                column: "user_seller_product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_sellers");
        }
    }
}
