using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookwormapi.Migrations
{
    /// <inheritdoc />
    public partial class OrderModeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModel", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemsModel",
                columns: table => new
                {
                    OrderItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemsModel", x => x.OrderItemsId);
                    table.ForeignKey(
                        name: "FK_OrderItemsModel_OrderModel_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderModel",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemsModel_OrderId",
                table: "OrderItemsModel",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemsModel");

            migrationBuilder.DropTable(
                name: "OrderModel");
        }
    }
}
