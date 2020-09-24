namespace GalaxisProjectWebAPI.Model
{
    public class PriceHistoricData
    {
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public uint Timestamp { get; set; }
        public double Price { get; set; }
    }
}