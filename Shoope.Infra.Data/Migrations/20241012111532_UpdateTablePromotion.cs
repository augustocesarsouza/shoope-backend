using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTablePromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "public_id_img",
                table: "tb_promotion",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "public_id_img",
                table: "tb_promotion");
        }
    }
}
