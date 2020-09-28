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
                    FundShareTokenAddress = "0xA293F54e8241BF32dD8D34b3475b5480DD0867F5",
                    InvestmentFundManager = "Gordon Gekko",
                    InvestmentFocus = "Earn interest by integrating different DEFI protocols",
                    HighWaterMark = false,
                    HurdleRate = true,
                    HurdleRatePercentage = 1.2,
                    DepositStartTimeStamp = 1601144460,
                    DepositCloseTimeStamp = 1601231100,
                    CloseTimeStamp = 1759343100,
                    BaseCurrency = "ETH",
                    ReportingCurrency = "USD",
                    MinimumContribution = 0,
                    MaximumContribution = 10,
                    MinimumCapital = 0,
                    MaximumCapital = 10
                }
            };
        }
    }
}