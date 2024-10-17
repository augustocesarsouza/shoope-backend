using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreatePromotionUserTable01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_promotion_user",
                columns: table => new
                {
                    promotion_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotion_user", x => x.promotion_user_id);
                    table.ForeignKey(
                        name: "FK_tb_promotion_user_tb_promotion_promotion_id",
                        column: x => x.promotion_id,
                        principalTable: "tb_promotion",
                        principalColumn: "promotion_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_promotion_user_tb_users_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_promotion_user_user_id",
                table: "tb_promotion_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_promotion_user_promotion_id",
                table: "tb_promotion_user",
                column: "promotion_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_promotion_user");
        }
    }
}
