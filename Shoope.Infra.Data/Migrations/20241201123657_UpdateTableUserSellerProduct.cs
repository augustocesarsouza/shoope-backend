using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTableUserSellerProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgFloatingPublicId",
                table: "tb_user_seller_products",
                newName: "img_floating_public_id");

            migrationBuilder.AlterColumn<string>(
                name: "img_floating",
                table: "tb_user_seller_products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "img_floating_public_id",
                table: "tb_user_seller_products",
                newName: "ImgFloatingPublicId");

            migrationBuilder.AlterColumn<string>(
                name: "img_floating",
                table: "tb_user_seller_products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
