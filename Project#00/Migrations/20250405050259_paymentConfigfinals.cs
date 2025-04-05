using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_00.Migrations
{
    /// <inheritdoc />
    public partial class paymentConfigfinals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PaymentProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PaymentProducts");
        }
    }
}
