using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookwormapi.Migrations
{
    /// <inheritdoc />
    public partial class addtotalnoofitemsinordermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfItems",
                table: "OrderModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalCost",
                table: "OrderModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfItems",
                table: "OrderModel");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "OrderModel");
        }
    }
}
