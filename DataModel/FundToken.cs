using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxisProjectWebAPI.DataModel
{
    public class FundToken
    {
        public uint Timestamp { get; set; }
        public int Quantity { get; set; }

        public int FundId { get; set; }
        public int TokenId { get; set; }
    }
}