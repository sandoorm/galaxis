using GalaxisProjectWebAPI.Model.DummyDataFactory;

namespace GalaxisProjectWebAPI.DataModel
{
    public class CompanyRepresentative : IData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}