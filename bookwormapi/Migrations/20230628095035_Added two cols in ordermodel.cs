using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookwormapi.Migrations
{
    /// <inheritdoc />
    public partial class Addedtwocolsinordermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "OrderModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "OrderModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "OrderModel");

            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "OrderModel");
        }
    }
}
