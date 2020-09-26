using System.Collections.Generic;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;

namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{

    public class DummyFundFactory : IDummyDataFactory<DataModelFund>
    {
        public List<DataModelFund> CreateDummyDatas()
        {
            return new List<DataModelFund>
            {
                new DataModelFund
                {
                    CompanyId = 1,
                    Name = "Galaxis Fund_1",
                    FundAddress = "0xdac17f958d2ee523a2206206994597c13d831ec7",
                    InvestmentFundManager = "Mate Brezovszki",
                    InvestmentFocus = "The fund seeks to make investment in Ether",
                    HighWaterMark = false,
                    HurdleRate = true,
                    HurdleRatePercentage = 0.7,
                    DepositStartTimeStamp = 1601130611,
                    DepositCloseTimeStamp = 1601130811,
                    CloseTimeStamp = 1601130850,
                    BaseCurrency = "USD",
                    MinimumContribution = 0.6,
                    MaximumContribution = 1.9
                },
                new DataModelFund
                {
                    CompanyId = 1,
                    Name = "Galaxis Fund_2",
                    FundAddress = "0xB8c77482e45F1F44dE1745F52C74426C631bDD52",
                    InvestmentFundManager = "Daniel Szego",
                    InvestmentFocus = "The fund seeks to make investment in various utility tokens",
                    HighWaterMark = false,
                    HurdleRate = true,
                    HurdleRatePercentage = 0.7,
                    DepositStartTimeStamp = 1702130500,
                    DepositCloseTimeStamp = 1702130900,
                    CloseTimeStamp = 1601130850,
                    BaseCurrency = "USD",
                    MinimumContribution = 0.6,
                    MaximumContribution = 5

                },
                new DataModelFund
                {
                    CompanyId = 1,
                    Name = "Galaxis Fund_3",
                    FundAddress = "0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48",
                    InvestmentFundManager = "Sandor Maraczy",
                    InvestmentFocus = "The fund seeks to make investment in carefully selected Blockchain based utility tokens",
                    HighWaterMark = false,
                    HurdleRate = true,
                    HurdleRatePercentage = 0.7,
                    DepositStartTimeStamp = 1702130100,
                    DepositCloseTimeStamp = 1702130800,
                    CloseTimeStamp = 1601130850,
                    BaseCurrency = "USD",
                    MinimumContribution = 0.3,
                    MaximumContribution = 13
                }
            };
        }
    }
}