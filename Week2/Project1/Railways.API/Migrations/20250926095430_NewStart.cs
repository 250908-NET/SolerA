using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Railways.API.Migrations
{
    /// <inheritdoc />
    public partial class NewStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Money = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Money = table.Column<int>(type: "int", nullable: false),
                    TotalShares = table.Column<int>(type: "int", nullable: false),
                    StockPriceIndex = table.Column<int>(type: "int", nullable: false),
                    PresidentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Players_PresidentId",
                        column: x => x.PresidentId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    SharesOwned = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Money", "Name", "PresidentId", "StockPriceIndex", "TotalShares" },
                values: new object[,]
                {
                    { 1, 0, "Expansive", null, 0, 5 },
                    { 2, 0, "Express", null, 0, 5 },
                    { 3, 0, "Suburban", null, 0, 5 },
                    { 4, 0, "Resourceful", null, 0, 5 },
                    { 5, 0, "Eastern Mining", null, 0, 5 },
                    { 6, 0, "Spacious", null, 0, 5 },
                    { 7, 0, "Agricultural", null, 0, 5 },
                    { 8, 0, "Bridging", null, 0, 5 },
                    { 9, 0, "Northern Port", null, 0, 5 },
                    { 10, 0, "Adaptive", null, 0, 5 },
                    { 11, 0, "Overnight", null, 0, 5 },
                    { 12, 0, "Tunneling", null, 0, 5 },
                    { 13, 0, "Manufacturing", null, 0, 5 },
                    { 14, 0, "Twin Cities", null, 0, 5 },
                    { 15, 0, "Circus", null, 0, 5 },
                    { 16, 0, "Coupling", null, 0, 5 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Money", "Username" },
                values: new object[] { -1, 0, "Bank" });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PresidentId",
                table: "Companies",
                column: "PresidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CompanyId",
                table: "Stocks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_PlayerId",
                table: "Stocks",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
