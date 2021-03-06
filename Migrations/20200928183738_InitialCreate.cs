﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    vatNumber = table.Column<string>(nullable: true),
                    OfficialEmailAddress = table.Column<string>(nullable: true),
                    ContactPersonName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    ContactPersonPhoneNumber = table.Column<string>(nullable: true),
                    ContactPersonEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Symbol = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Decimals = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CompanyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    FundAddress = table.Column<string>(nullable: true),
                    FundShareTokenAddress = table.Column<string>(nullable: true),
                    InvestmentFundManager = table.Column<string>(nullable: true),
                    InvestmentFocus = table.Column<string>(nullable: true),
                    HighWaterMark = table.Column<bool>(nullable: false),
                    HurdleRate = table.Column<bool>(nullable: false),
                    HurdleRatePercentage = table.Column<double>(nullable: false),
                    DepositStartTimeStamp = table.Column<long>(nullable: false),
                    DepositCloseTimeStamp = table.Column<long>(nullable: false),
                    CloseTimeStamp = table.Column<long>(nullable: false),
                    BaseCurrency = table.Column<string>(nullable: true),
                    ReportingCurrency = table.Column<string>(nullable: true),
                    MinimumContribution = table.Column<double>(nullable: false),
                    MaximumContribution = table.Column<double>(nullable: false),
                    MinimumCapital = table.Column<double>(nullable: false),
                    MaximumCapital = table.Column<double>(nullable: false),
                    IsLaunched = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funds_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenPriceHistoricDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TokenId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false),
                    UsdPrice = table.Column<double>(nullable: false),
                    EurPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenPriceHistoricDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenPriceHistoricDatas_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundTokens",
                columns: table => new
                {
                    Timestamp = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    FundId = table.Column<int>(nullable: false),
                    TokenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTokens", x => new { x.FundId, x.TokenId, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_FundTokens_Funds_FundId",
                        column: x => x.FundId,
                        principalTable: "Funds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundTokens_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funds_CompanyId",
                table: "Funds",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FundTokens_TokenId",
                table: "FundTokens",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenPriceHistoricDatas_TokenId",
                table: "TokenPriceHistoricDatas",
                column: "TokenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundTokens");

            migrationBuilder.DropTable(
                name: "TokenPriceHistoricDatas");

            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
