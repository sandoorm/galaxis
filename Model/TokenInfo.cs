namespace GalaxisProjectWebAPI.Model
{
    public class TokenInfo
    {
        public int TokenId { get; set; }
        public string TokenSymbol { get; set; }
        public int Quantity { get; set; }
        public uint TimeStamp { get; internal set; }
    }
}