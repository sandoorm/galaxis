namespace GalaxisProjectWebAPI.Model
{
    public class PriceHistoricData
    {
        public string BaseTokenSymbol { get; set; }
        public uint Timestamp { get; set; }
        public double USDPrice { get; set; }
        public double EURPrice { get; set; }
    }
}