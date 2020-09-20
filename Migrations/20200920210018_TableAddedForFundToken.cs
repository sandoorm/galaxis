using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class TableAddedForFundToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundToken_Funds_FundId",
                table: "FundToken");

            migrationBuilder.DropForeignKey(
                name: "FK_FundToken_Tokens_TokenId",
                table: "FundToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FundToken",
                table: "FundToken");

            migrationBuilder.RenameTable(
                name: "FundToken",
                newName: "FundTokens");

            migrationBuilder.RenameIndex(
                name: "IX_FundToken_TokenId",
                table: "FundTokens",
                newName: "IX_FundTokens_TokenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundTokens",
                table: "FundTokens",
                columns: new[] { "FundId", "TokenId", "Timestamp" });

            migrationBuilder.AddForeignKey(
                name: "FK_FundTokens_Funds_FundId",
                table: "FundTokens",
                column: "FundId",
                principalTable: "Funds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FundTokens_Tokens_TokenId",
                table: "FundTokens",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundTokens_Funds_FundId",
                table: "FundTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_FundTokens_Tokens_TokenId",
                table: "FundTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FundTokens",
                table: "FundTokens");

            migrationBuilder.RenameTable(
                name: "FundTokens",
                newName: "FundToken");

            migrationBuilder.RenameIndex(
                name: "IX_FundTokens_TokenId",
                table: "FundToken",
                newName: "IX_FundToken_TokenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundToken",
                table: "FundToken",
                columns: new[] { "FundId", "TokenId", "Timestamp" });

            migrationBuilder.AddForeignKey(
                name: "FK_FundToken_Funds_FundId",
                table: "FundToken",
                column: "FundId",
                principalTable: "Funds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FundToken_Tokens_TokenId",
                table: "FundToken",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
