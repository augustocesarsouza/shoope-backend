using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdatingPropertyTableProductOptionImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "option_name",
                table: "tb_product_option_images",
                newName: "option_type");

            migrationBuilder.AddColumn<string>(
                name: "img_alt",
                table: "tb_product_option_images",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_alt",
                table: "tb_product_option_images");

            migrationBuilder.RenameColumn(
                name: "option_type",
                table: "tb_product_option_images",
                newName: "option_name");
        }
    }
}
