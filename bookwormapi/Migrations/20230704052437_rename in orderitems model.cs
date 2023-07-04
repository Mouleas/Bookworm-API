using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookwormapi.Migrations
{
    /// <inheritdoc />
    public partial class renameinorderitemsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "OrderItemsModel",
                newName: "BookQuantity");

            migrationBuilder.RenameColumn(
                name: "OldPriceBook",
                table: "OrderItemsModel",
                newName: "BookPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookQuantity",
                table: "OrderItemsModel",
                newName: "ProductQuantity");

            migrationBuilder.RenameColumn(
                name: "BookPrice",
                table: "OrderItemsModel",
                newName: "OldPriceBook");
        }
    }
}
