using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookwormapi.Migrations
{
    /// <inheritdoc />
    public partial class Reviewmodeldatetimeadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewDateTime",
                table: "ReviewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewDateTime",
                table: "ReviewModel");
        }
    }
}
