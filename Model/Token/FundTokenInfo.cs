namespace GalaxisProjectWebAPI.Model.Token
{
    public class FundTokenInfo
    {
        public int TokenId { get; set; }
        public string TokenSymbol { get; set; }
        public double Quantity { get; set; }
        public uint TimeStamp { get; internal set; }
    }
}