﻿using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Model
{
    public class Fund
    {
        private readonly DataModel.Fund fund;

        public Fund(DataModel.Fund fund)
        {
            this.fund = fund;
        }

        public int ID => fund.Id;

        public int CompanyId => fund.CompanyId;

        public string FundName => fund.FundName;

        public string InvestmentFundManagerName => fund.InvestmentFundManagerName;

        public double FloorLevel => fund.FloorLevel;

        public Company Company => fund.Company;
    }
}