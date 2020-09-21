using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;

namespace GalaxisProjectWebAPI.Model
{
    public class CompanyAndFunds
    {
        public string CompanyName { get; set; }

        public DataModelFund Fund { get; set; }
    }
}