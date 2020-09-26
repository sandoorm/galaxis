using GalaxisProjectWebAPI.Model.DummyDataFactory;

namespace GalaxisProjectWebAPI.DataModel
{
    public class TokenPriceHistoricData : IData
    {
        public int Id { get; set; }
        public int TokenId { get; set; }
        public uint Timestamp { get; set; }
        public double UsdPrice { get; set; }
        public double EurPrice { get; set; }

        public Token Token { get; set; }
    }
}