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
                    CompanyName = "Galaxis LTD",
                    CompanyAddress = "22-26 Shelton Street, London, UK",
                    RegistrationNumber = "WC2H 9JQ",
                    vatNumber = "GB 55 888 5555 444",
                    OfficialEmailAddress = "hello@galaxis.network",
                    ContactPersonName = "Alice Hodl",
                    Position = "pm",
                    ContactPersonPhoneNumber = "+44 756 387 85 85",
                    ContactPersonEmail = "pm@galaxis.network"
                }
            };
        }
    }
}