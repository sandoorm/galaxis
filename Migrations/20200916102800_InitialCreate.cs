using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Symbol = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Decimals = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRepresentative",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRepresentative", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyRepresentative_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fund",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CompanyID = table.Column<int>(nullable: false),
                    FundName = table.Column<string>(nullable: true),
                    InvestmentFundManagerName = table.Column<string>(nullable: true),
                    FloorLevel = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fund", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fund_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenPriceHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TokenID = table.Column<int>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false),
                    USD_Price = table.Column<double>(nullable: false),
                    EUR_Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenPriceHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TokenPriceHistory_Token_TokenID",
                        column: x => x.TokenID,
                        principalTable: "Token",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundToken",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<int>(nullable: false),
                    TokenID = table.Column<int>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false),
                    TokenQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundToken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundToken_Fund_FundID",
                        column: x => x.FundID,
                        principalTable: "Fund",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundToken_Token_TokenID",
                        column: x => x.TokenID,
                        principalTable: "Token",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRepresentative_CompanyID",
                table: "CompanyRepresentative",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Fund_CompanyID",
                table: "Fund",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_FundToken_FundID",
                table: "FundToken",
                column: "FundID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundToken_TokenID",
                table: "FundToken",
                column: "TokenID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokenPriceHistory_TokenID",
                table: "TokenPriceHistory",
                column: "TokenID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyRepresentative");

            migrationBuilder.DropTable(
                name: "FundToken");

            migrationBuilder.DropTable(
                name: "TokenPriceHistory");

            migrationBuilder.DropTable(
                name: "Fund");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
