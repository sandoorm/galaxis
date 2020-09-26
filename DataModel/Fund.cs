using System.Collections.Generic;
using GalaxisProjectWebAPI.Model.DummyDataFactory;

namespace GalaxisProjectWebAPI.DataModel
{
    public class Fund : IData
    {
        public Fund()
        {
            FundTokens = new List<FundToken>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string FundAddress { get; set; }
        public string InvestmentFundManager { get; set; }
        public string InvestmentFocus { get; set; }
        public bool HighWaterMark { get; set; }
        public bool HurdleRate { get; set; }
        public double HurdleRatePercentage { get; set; }
        public uint DepositStartTimeStamp { get; set; }
        public uint DepositCloseTimeStamp { get; set; }
        public uint CloseTimeStamp { get; set; }
        public string BaseCurrency { get; set; }
        public uint MinimumContribution { get; set; }
        public uint MaximumContribution { get; set; }
        public bool IsLaunched { get; set; }

        public Company Company { get; set; }
        public List<FundToken> FundTokens { get; set; }
    }
}