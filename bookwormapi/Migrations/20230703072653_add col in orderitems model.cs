using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookwormapi.Migrations
{
    /// <inheritdoc />
    public partial class addcolinorderitemsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderItemsModel");

            migrationBuilder.AddColumn<string>(
                name: "BookAuthor",
                table: "OrderItemsModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookDescription",
                table: "OrderItemsModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookLanguage",
                table: "OrderItemsModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "OrderItemsModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TotalPages",
                table: "OrderItemsModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookAuthor",
                table: "OrderItemsModel");

            migrationBuilder.DropColumn(
                name: "BookDescription",
                table: "OrderItemsModel");

            migrationBuilder.DropColumn(
                name: "BookLanguage",
                table: "OrderItemsModel");

            migrationBuilder.DropColumn(
                name: "BookName",
                table: "OrderItemsModel");

            migrationBuilder.DropColumn(
                name: "TotalPages",
                table: "OrderItemsModel");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderItemsModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
