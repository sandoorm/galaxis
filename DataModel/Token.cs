using System.Collections.Generic;

namespace GalaxisProjectWebAPI.DataModel
{
    public class Token
    {
        public Token()
        {
            FundTokens = new List<FundToken>();
        }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Decimals { get; set; }

        public TokenPriceHistoricData TokenPriceHistoricData { get; set; }
        public List<FundToken> FundTokens { get; set; }
    }
}