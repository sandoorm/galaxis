using System.Collections.Generic;
using DataModelCompany = GalaxisProjectWebAPI.DataModel.Company;

namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{
    public class DummyCompanyFactory : IDummyDataFactory<DataModelCompany>
    {
        public List<DataModelCompany> CreateDummyDatas()
        {
            return new List<DataModelCompany>
            {
                new DataModelCompany
                {
                    CompanyName = "Solidity Services Ltd.",
                    CompanyAddress = "X street 1.",
                    RegistrationNumber = "343434",
                    vatNumber = "34898734",
                    OfficialEmailAddress = "office@x.com"
                },
                new DataModelCompany
                {
                    CompanyName = "Y Ltd.",
                    CompanyAddress = "Y street 10.",
                    RegistrationNumber = "343434",
                    vatNumber = "34898734",
                    OfficialEmailAddress = "office@y.com"
                }
            };
        }
    }
}