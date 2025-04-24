using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_00.Migrations
{
    /// <inheritdoc />
    public partial class addresschangeagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentProducts_AddressId",
                table: "PaymentProducts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCarts_AddressId",
                table: "PaymentCarts",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCarts_Addresses_AddressId",
                table: "PaymentCarts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProducts_Addresses_AddressId",
                table: "PaymentProducts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCarts_Addresses_AddressId",
                table: "PaymentCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProducts_Addresses_AddressId",
                table: "PaymentProducts");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProducts_AddressId",
                table: "PaymentProducts");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCarts_AddressId",
                table: "PaymentCarts");
        }
    }
}
