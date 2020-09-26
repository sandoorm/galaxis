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
                    InvestmentFundManager = "Mate Brezovszki"
                },
                new DataModelFund
                {
                    CompanyId = 1,
                    Name = "Galaxis Fund_2",
                    InvestmentFundManager = "Daniel Szego"
                    
                },
                new DataModelFund
                {
                    CompanyId = 1,
                    Name = "Galaxis Fund_3",
                    InvestmentFundManager = "Sandor Maraczy"
                }
            };
        }
    }
}