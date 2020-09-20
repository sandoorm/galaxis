using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class TryFundTokenTableWithoutId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "FundToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FundToken",
                nullable: false,
                defaultValue: 0);
        }
    }
}
