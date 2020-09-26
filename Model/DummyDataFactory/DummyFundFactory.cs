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
                    FundName = "Galaxis Fund_1",
                    InvestmentFundManagerName = "Mate Brezovszki",
                    FloorLevel = 0.65
                },
                new DataModelFund
                {
                    CompanyId = 1,
                    FundName = "Galaxis Fund_2",
                    InvestmentFundManagerName = "Daniel Szego",
                    FloorLevel = 0.7,
                    
                },
                new DataModelFund
                {
                    CompanyId = 1,
                    FundName = "Galaxis Fund_3",
                    InvestmentFundManagerName = "Sandor Maraczy",
                    FloorLevel = 0.65
                }
            };
        }
    }
}