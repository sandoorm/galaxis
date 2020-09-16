namespace GalaxisProjectWebAPI.DataModel
{
    public class FundToken
    {
        public int FundTokenId { get; set; }
        public uint Timestamp { get; set; }
        public int Quantity { get; set; }

        public int FundId { get; set; }
        public int TokenId { get; set; }
    }
}