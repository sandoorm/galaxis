using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class TokenPriceHistoryTableFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "USD_Price",
                table: "TokenPriceHistoricDatas",
                newName: "UsdPrice");

            migrationBuilder.RenameColumn(
                name: "EUR_Price",
                table: "TokenPriceHistoricDatas",
                newName: "EurPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsdPrice",
                table: "TokenPriceHistoricDatas",
                newName: "USD_Price");

            migrationBuilder.RenameColumn(
                name: "EurPrice",
                table: "TokenPriceHistoricDatas",
                newName: "EUR_Price");
        }
    }
}
