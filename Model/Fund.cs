using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Model
{
    public class Fund
    {
        private readonly DataModel.Fund fund;

        public Fund(DataModel.Fund fund)
        {
            this.fund = fund;
        }

        public int ID => fund.ID;

        public int CompanyId => fund.CompanyID;

        public string FundName => fund.FundName;

        public string InvestmentFundManagerName => fund.InvestmentFundManagerName;

        public double FloorLevel => fund.FloorLevel;

        public Company Company => fund.Company;
    }
}