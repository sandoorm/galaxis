using System.Collections.Generic;

namespace GalaxisProjectWebAPI.DataModel
{
    public class Fund
    {
        public Fund()
        {
            FundTokens = new List<FundToken>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FundName { get; internal set; }
        public string InvestmentFundManagerName { get; internal set; }
        public double FloorLevel { get; internal set; }

        public Company Company { get; set; }
        public List<FundToken> FundTokens { get; set; }
    }
}