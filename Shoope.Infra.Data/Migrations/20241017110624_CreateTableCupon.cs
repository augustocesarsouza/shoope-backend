using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoope.Infra.Data.Migrations
{
    public partial class CreateTableCupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_promotion_user_tb_promotion_promotion_id",
                table: "tb_promotion_user");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_promotion_user_tb_users_user_id",
                table: "tb_promotion_user");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "tb_promotion_user",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "promotion_id",
                table: "tb_promotion_user",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "tb_cupons",
                columns: table => new
                {
                    cupons_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_text = table.Column<string>(type: "text", nullable: false),
                    second_text = table.Column<string>(type: "text", nullable: false),
                    third_text = table.Column<string>(type: "text", nullable: false),
                    date_validate_cupon = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantity_cupons = table.Column<int>(type: "integer", nullable: false),
                    what_cupon_number = table.Column<int>(type: "integer", nullable: false),
                    second_img = table.Column<string>(type: "text", nullable: true),
                    second_img_alt = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cupons", x => x.cupons_id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tb_promotion_user_tb_promotion_promotion_id",
                table: "tb_promotion_user",
                column: "promotion_id",
                principalTable: "tb_promotion",
                principalColumn: "promotion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_promotion_user_tb_users_user_id",
                table: "tb_promotion_user",
                column: "user_id",
                principalTable: "tb_users",
                principalColumn: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_promotion_user_tb_promotion_promotion_id",
                table: "tb_promotion_user");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_promotion_user_tb_users_user_id",
                table: "tb_promotion_user");

            migrationBuilder.DropTable(
                name: "tb_cupons");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "tb_promotion_user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "promotion_id",
                table: "tb_promotion_user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_promotion_user_tb_promotion_promotion_id",
                table: "tb_promotion_user",
                column: "promotion_id",
                principalTable: "tb_promotion",
                principalColumn: "promotion_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_promotion_user_tb_users_user_id",
                table: "tb_promotion_user",
                column: "user_id",
                principalTable: "tb_users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
