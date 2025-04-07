using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_00.Migrations
{
    /// <inheritdoc />
    public partial class purchasedProductsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductPurachases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurachases_OrderId",
                table: "ProductPurachases",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurachases_Orders_OrderId",
                table: "ProductPurachases",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurachases_Orders_OrderId",
                table: "ProductPurachases");

            migrationBuilder.DropIndex(
                name: "IX_ProductPurachases_OrderId",
                table: "ProductPurachases");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductPurachases");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
