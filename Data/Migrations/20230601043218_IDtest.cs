using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneWine.Data.Migrations
{
    /// <inheritdoc />
    public partial class IDtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "CustomerModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerModel_IdentityUserId",
                table: "CustomerModel",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerModel_AspNetUsers_IdentityUserId",
                table: "CustomerModel",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerModel_AspNetUsers_IdentityUserId",
                table: "CustomerModel");

            migrationBuilder.DropIndex(
                name: "IX_CustomerModel_IdentityUserId",
                table: "CustomerModel");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "CustomerModel");
        }
    }
}
