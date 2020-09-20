using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class AddNewKeyToFundToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FundToken",
                table: "FundToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundToken",
                table: "FundToken",
                columns: new[] { "FundId", "TokenId", "Timestamp" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FundToken",
                table: "FundToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundToken",
                table: "FundToken",
                columns: new[] { "FundId", "TokenId" });
        }
    }
}
