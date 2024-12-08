using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTableProductFlashSaleReviewsPropertyNews2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_product_flash_sale_reviews_tb_users_user_id",
                table: "tb_product_flash_sale_reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_product_flash_sale_reviews_tb_users_user_id",
                table: "tb_product_flash_sale_reviews",
                column: "user_id",
                principalTable: "tb_users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_product_flash_sale_reviews_tb_users_user_id",
                table: "tb_product_flash_sale_reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_product_flash_sale_reviews_tb_users_user_id",
                table: "tb_product_flash_sale_reviews",
                column: "user_id",
                principalTable: "tb_users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
