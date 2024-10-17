using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreatePromotionTable0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_promotion",
                columns: table => new
                {
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    what_is_the_promotion = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    img = table.Column<string>(type: "text", nullable: false),
                    img_inner_first = table.Column<string>(type: "text", nullable: true),
                    alt_img_inner_first = table.Column<string>(type: "text", nullable: true),
                    img_inner_second = table.Column<string>(type: "text", nullable: true),
                    alt_img_inner_second = table.Column<string>(type: "text", nullable: true),
                    img_inner_third = table.Column<string>(type: "text", nullable: true),
                    alt_img_inner_third = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotion", x => x.promotion_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_promotion");
        }
    }
}
