namespace GalaxisProjectWebAPI.Model
{
    public class PriceHistoricData
    {
        public string BaseTokenSymbol { get; set; }
        public uint Timestamp { get; set; }
        public double USD_Price { get; set; }
        public double EUR_Price { get; set; }
    }
}