namespace GalaxisProjectWebAPI.DataModel
{
    public class Token
    {
        public int ID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Decimals { get; set; }
        public FundToken FundToken { get; set; }

        public TokenPriceHistory TokenPriceHistory { get; set; }
    }
}