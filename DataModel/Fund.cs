using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Name { get; set; }
        public string FundAddress { get; set; }
        public string FundShareTokenAddress { get; set; }
        public string InvestmentFundManager { get; set; }
        public string InvestmentFocus { get; set; }
        public bool HighWaterMark { get; set; }
        public bool HurdleRate { get; set; }
        public double HurdleRatePercentage { get; set; }
        public uint DepositStartTimeStamp { get; set; }
        public uint DepositCloseTimeStamp { get; set; }
        public uint CloseTimeStamp { get; set; }
        public string BaseCurrency { get; set; }
        public string ReportingCurrency { get; set; }
        public double MinimumContribution { get; set; }
        public double MaximumContribution { get; set; }
        public double MinimumCapital { get; set; }
        public double MaximumCapital { get; set; }
        public bool IsLaunched { get; set; }

        public Company Company { get; set; }
        public List<FundToken> FundTokens { get; set; }   
    }
}