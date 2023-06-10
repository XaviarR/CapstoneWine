using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneWine.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedorderstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_CustomerModel_AspNetUsers_IdentityUserId",
                table: "CustomerModel");

            migrationBuilder.DropIndex(
                name: "IX_CustomerModel_IdentityUserId",
                table: "CustomerModel");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "CustomerModel");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityKey",
                table: "CustomerModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityKey",
                table: "CustomerModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
