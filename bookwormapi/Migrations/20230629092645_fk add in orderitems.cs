using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookwormapi.Migrations
{
    /// <inheritdoc />
    public partial class fkaddinorderitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderItemsModel_ProductId",
                table: "OrderItemsModel",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemsModel_BookModel_ProductId",
                table: "OrderItemsModel",
                column: "ProductId",
                principalTable: "BookModel",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemsModel_BookModel_ProductId",
                table: "OrderItemsModel");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemsModel_ProductId",
                table: "OrderItemsModel");
        }
    }
}
