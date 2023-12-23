using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class db_shipping_address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingAddressId",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ShippingAddressId",
                table: "Invoice",
                column: "ShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Address_ShippingAddressId",
                table: "Invoice",
                column: "ShippingAddressId",
                principalTable: "Address",
                principalColumn: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Address_ShippingAddressId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ShippingAddressId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                table: "Invoice");
        }
    }
}
