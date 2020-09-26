using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaxisProjectWebAPI.Migrations
{
    public partial class FundExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvestmentFundManagerName",
                table: "Funds",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FundName",
                table: "Funds",
                newName: "InvestmentFundManager");

            migrationBuilder.RenameColumn(
                name: "FloorLevel",
                table: "Funds",
                newName: "HurdleRatePercentage");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Funds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaseCurrency",
                table: "Funds",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CloseTimeStamp",
                table: "Funds",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DepositCloseTimeStamp",
                table: "Funds",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DepositStartTimeStamp",
                table: "Funds",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "HighWaterMark",
                table: "Funds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HurdleRate",
                table: "Funds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InvestmentFocus",
                table: "Funds",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaximumContribution",
                table: "Funds",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MinimumContribution",
                table: "Funds",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "BaseCurrency",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "CloseTimeStamp",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "DepositCloseTimeStamp",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "DepositStartTimeStamp",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "HighWaterMark",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "HurdleRate",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "InvestmentFocus",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "MaximumContribution",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "MinimumContribution",
                table: "Funds");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Funds",
                newName: "InvestmentFundManagerName");

            migrationBuilder.RenameColumn(
                name: "InvestmentFundManager",
                table: "Funds",
                newName: "FundName");

            migrationBuilder.RenameColumn(
                name: "HurdleRatePercentage",
                table: "Funds",
                newName: "FloorLevel");
        }
    }
}
