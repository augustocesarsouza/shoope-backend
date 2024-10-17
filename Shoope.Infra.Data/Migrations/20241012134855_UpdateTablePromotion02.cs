using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTablePromotion02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img_inner_first_public_id",
                table: "tb_promotion",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "img_inner_second_public_id",
                table: "tb_promotion",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "img_inner_third_public-id",
                table: "tb_promotion",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_inner_first_public_id",
                table: "tb_promotion");

            migrationBuilder.DropColumn(
                name: "img_inner_second_public_id",
                table: "tb_promotion");

            migrationBuilder.DropColumn(
                name: "img_inner_third_public-id",
                table: "tb_promotion");
        }
    }
}
