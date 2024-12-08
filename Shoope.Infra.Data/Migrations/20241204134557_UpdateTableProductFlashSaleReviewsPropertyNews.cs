using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTableProductFlashSaleReviewsPropertyNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "tb_product_flash_sale_reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_flash_sale_reviews_user_id",
                table: "tb_product_flash_sale_reviews",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_product_flash_sale_reviews_tb_users_user_id",
                table: "tb_product_flash_sale_reviews",
                column: "user_id",
                principalTable: "tb_users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_product_flash_sale_reviews_tb_users_user_id",
                table: "tb_product_flash_sale_reviews");

            migrationBuilder.DropIndex(
                name: "IX_tb_product_flash_sale_reviews_user_id",
                table: "tb_product_flash_sale_reviews");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tb_product_flash_sale_reviews");
        }
    }
}
