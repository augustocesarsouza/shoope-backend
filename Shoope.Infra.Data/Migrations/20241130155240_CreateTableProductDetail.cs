using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableProductDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_product_details",
                columns: table => new
                {
                    product_details_id = table.Column<Guid>(type: "uuid", nullable: false),
                    promotional_stock = table.Column<int>(type: "integer", nullable: false),
                    total_stock = table.Column<int>(type: "integer", nullable: false),
                    sending_of = table.Column<string>(type: "text", nullable: false),
                    mark = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    warrantly_duration = table.Column<string>(type: "text", nullable: true),
                    warrantly_type = table.Column<string>(type: "text", nullable: true),
                    product_weight = table.Column<string>(type: "text", nullable: true),
                    energy_consumption = table.Column<string>(type: "text", nullable: true),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_details", x => x.product_details_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_details");
        }
    }
}
