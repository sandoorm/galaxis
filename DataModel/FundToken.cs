using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxisProjectWebAPI.DataModel
{
    public class FundToken
    {
        public uint Timestamp { get; set; }
        public double Quantity { get; set; }

        public int FundId { get; set; }
        public Fund Fund { get; set; }

        public int TokenId { get; set; }
        public Token Token { get; set; }
    }
}