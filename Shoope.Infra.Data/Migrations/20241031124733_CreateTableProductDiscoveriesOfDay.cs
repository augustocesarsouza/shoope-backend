using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableProductDiscoveriesOfDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_product_discoveries_of_days",
                columns: table => new
                {
                    product_discoveries_of_days_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    img_product = table.Column<string>(type: "text", nullable: false),
                    img_product_public_id = table.Column<string>(type: "text", nullable: false),
                    img_part_bottom = table.Column<string>(type: "text", nullable: true),
                    discount_percentage = table.Column<int>(type: "integer", nullable: true),
                    is_ad = table.Column<bool>(type: "boolean", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    quantity_sold = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_discoveries_of_days", x => x.product_discoveries_of_days_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_discoveries_of_days");
        }
    }
}
