using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneWine.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedorderandcmtbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
