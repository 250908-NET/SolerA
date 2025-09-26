using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Railways.API.Migrations
{
    /// <inheritdoc />
    public partial class Working : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Players_PlayerId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Players_PlayerId",
                table: "Stocks",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Players_PlayerId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Players_PlayerId",
                table: "Stocks",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
