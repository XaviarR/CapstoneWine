using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneWine.Data.Migrations
{
    /// <inheritdoc />
    public partial class applicationdbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityKey",
                table: "CustomerModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SubID",
                table: "Orders",
                column: "SubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Subscriptions_SubID",
                table: "Orders",
                column: "SubID",
                principalTable: "Subscriptions",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
