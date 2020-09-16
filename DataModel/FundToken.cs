namespace GalaxisProjectWebAPI.DataModel
{
    public class FundToken
    {
        public int ID { get; set; }
        public int FundID { get; set; }
        public int TokenID { get; set; }
        public uint Timestamp { get; set; }
        public int TokenQuantity { get; set; }

        public Fund Fund { get; set; }
        public Token Token { get; set; }
    }
}