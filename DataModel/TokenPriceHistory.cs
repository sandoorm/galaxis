namespace GalaxisProjectWebAPI.DataModel
{
    public class TokenPriceHistory
    {
        public int ID { get; set; }
        public int TokenID { get; set; }
        public uint Timestamp { get; set; }
        public double USD_Price { get; set; }
        public double EUR_Price { get; set; }

        public Token Token { get; set; }
    }
}