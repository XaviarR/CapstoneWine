using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneWine.Data.Migrations
{
    /// <inheritdoc />
    public partial class cmfieldupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CustomerModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "CustomerModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RewardPoints",
                table: "CustomerModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "CustomerModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suburb",
                table: "CustomerModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "CustomerModel");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "CustomerModel");

            migrationBuilder.DropColumn(
                name: "RewardPoints",
                table: "CustomerModel");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "CustomerModel");

            migrationBuilder.DropColumn(
                name: "Suburb",
                table: "CustomerModel");
        }
    }
}
