using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTableProductRemoveImgPartBottomPublicId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_part_bottom_public_id",
                table: "tb_products_offer_flash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img_part_bottom_public_id",
                table: "tb_products_offer_flash",
                type: "text",
                nullable: true);
        }
    }
}
