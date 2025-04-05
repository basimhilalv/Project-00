using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_00.Migrations
{
    /// <inheritdoc />
    public partial class paymentCartFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentCartId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_PaymentCartId",
                table: "CartItems",
                column: "PaymentCartId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCarts_UserId",
                table: "PaymentCarts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_PaymentCarts_PaymentCartId",
                table: "CartItems",
                column: "PaymentCartId",
                principalTable: "PaymentCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_PaymentCarts_PaymentCartId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "PaymentCarts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_PaymentCartId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PaymentCartId",
                table: "CartItems");
        }
    }
}
