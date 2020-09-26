using System.Collections.Generic;
using GalaxisProjectWebAPI.Model.DummyDataFactory;

namespace GalaxisProjectWebAPI.DataModel
{
    public class Token : IData
    {
        public Token()
        {
            FundTokens = new List<FundToken>();
        }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Decimals { get; set; }

        public List<TokenPriceHistoricData> TokenPriceHistoricDatas { get; set; }
        public List<FundToken> FundTokens { get; set; }
    }
}