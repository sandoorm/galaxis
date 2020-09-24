namespace GalaxisProjectWebAPI.Model
{
    public class PriceHistoricData
    {
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public uint Timestamp { get; set; }
        public double USD_Price { get; set; }
        public double EUR_Price { get; set; }
    }
}