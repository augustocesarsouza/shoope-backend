using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class UpdateTablePromotionColumn01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "What_is_the_promotion",
                table: "tb_promotion",
                newName: "what_is_the_promotion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "what_is_the_promotion",
                table: "tb_promotion",
                newName: "What_is_the_promotion");
        }
    }
}
