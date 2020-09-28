using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class QuantityResolved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "FundTokens",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "FundTokens",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
