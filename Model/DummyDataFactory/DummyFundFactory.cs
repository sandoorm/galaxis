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
                    Name = "DEFI test fund",
                    FundAddress = "0x82cC21bFa2745d5E03ef342662676B2AA89Bc658",
                    InvestmentFundManager = "Gordon Gekko",
                    InvestmentFocus = "Earn interest by integrating different DEFI protocols",
                    HighWaterMark = false,
                    HurdleRate = true,
                    HurdleRatePercentage = 1.2,
                    DepositStartTimeStamp = 1601144460,
                    DepositCloseTimeStamp = 1601231100,
                    CloseTimeStamp = 1759343100,
                    BaseCurrency = "USD",
                    MinimumContribution = 0,
                    MaximumContribution = 10,
                    MinimumCapital = 0,
                    MaximumCapital = 10
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
                    HurdleRatePercentage = 1.1,
                    DepositStartTimeStamp = 1702130500,
                    DepositCloseTimeStamp = 1702130900,
                    CloseTimeStamp = 1601130850,
                    BaseCurrency = "USD",
                    MinimumContribution = 0.6,
                    MaximumContribution = 5,
                    MinimumCapital = 0,
                    MaximumCapital = 15

                },
                new DataModelFund
                {
                    CompanyId = 2,
                    Name = "Galaxis Fund_3",
                    FundAddress = "0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48",
                    InvestmentFundManager = "Sandor Maraczy",
                    InvestmentFocus = "The fund seeks to make investment in carefully selected Blockchain based utility tokens",
                    HighWaterMark = false,
                    HurdleRate = true,
                    HurdleRatePercentage = 1,
                    DepositStartTimeStamp = 1702130100,
                    DepositCloseTimeStamp = 1702130800,
                    CloseTimeStamp = 1601130850,
                    BaseCurrency = "USD",
                    MinimumContribution = 0.3,
                    MaximumContribution = 13,
                    MinimumCapital = 0,
                    MaximumCapital = 13
                }
            };
        }
    }
}