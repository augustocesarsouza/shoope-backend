using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableUserCupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_user_cupons",
                columns: table => new
                {
                    user_cupons_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cupon_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_cupons", x => x.user_cupons_id);
                    table.ForeignKey(
                        name: "FK_tb_user_cupons_tb_cupons_cupon_id",
                        column: x => x.cupon_id,
                        principalTable: "tb_cupons",
                        principalColumn: "cupons_id");
                    table.ForeignKey(
                        name: "FK_tb_user_cupons_tb_users_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_cupons_cupon_id",
                table: "tb_user_cupons",
                column: "cupon_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_cupons_user_id",
                table: "tb_user_cupons",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_user_cupons");
        }
    }
}
