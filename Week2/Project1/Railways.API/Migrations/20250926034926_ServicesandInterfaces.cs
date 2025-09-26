using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Railways.API.Migrations
{
    /// <inheritdoc />
    public partial class ServicesandInterfaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Players_PresidentID",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "PresidentID",
                table: "Companies",
                newName: "PresidentId");

            migrationBuilder.RenameColumn(
                name: "StockValue",
                table: "Companies",
                newName: "TotalShares");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_PresidentID",
                table: "Companies",
                newName: "IX_Companies_PresidentId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "StockPriceIndex",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Players_PresidentId",
                table: "Companies",
                column: "PresidentId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Players_PresidentId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StockPriceIndex",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "PresidentId",
                table: "Companies",
                newName: "PresidentID");

            migrationBuilder.RenameColumn(
                name: "TotalShares",
                table: "Companies",
                newName: "StockValue");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_PresidentId",
                table: "Companies",
                newName: "IX_Companies_PresidentID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Players_PresidentID",
                table: "Companies",
                column: "PresidentID",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
