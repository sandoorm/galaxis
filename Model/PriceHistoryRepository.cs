using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.Infrastructure;
using GalaxisProjectWebAPI.DataModel;

using DataModelToken = GalaxisProjectWebAPI.DataModel.Token;

namespace GalaxisProjectWebAPI.Model
{
    public class PriceHistoryRepository : IPriceHistoryRepository
    {
        private readonly GalaxisDbContext galaxisContext;

        public PriceHistoryRepository(GalaxisDbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public List<PriceHistoricData> GetAllHistoricPriceDataAsync(string baseTokenSymbol)
        {
            DataModelToken foundToken = GetRelevantToken(baseTokenSymbol);

            return foundToken != null
                ? GetTokenHistory(baseTokenSymbol, foundToken.Id)
                : new List<PriceHistoricData>();
        }

        private List<PriceHistoricData> GetTokenHistory(string baseTokenSymbol, int tokenId)
        {
            return this.galaxisContext
                .TokenPriceHistoricDatas
                .Where(history => history.TokenId == tokenId)
                .Select(historicData => new PriceHistoricData
                {
                    BaseTokenSymbol = baseTokenSymbol,
                    Timestamp = historicData.Timestamp,
                    USDPrice = historicData.UsdPrice,
                    EURPrice = historicData.EurPrice
                })
                .ToList();
        }

        public void AddTokenPriceHistoryDatasAsync(List<PriceHistoricData> priceHistoricDatas)
        {
            var priceHistoricData = priceHistoricDatas.FirstOrDefault();
            if (priceHistoricData != null)
            {
                var relevantToken = GetRelevantToken(priceHistoricData.BaseTokenSymbol);
                if (relevantToken != null)
                {
                    var dataModelTokenPriceHistory = priceHistoricDatas
                    .Select(data => new TokenPriceHistoricData
                    {
                        Timestamp = data.Timestamp,
                        UsdPrice = data.USDPrice,
                        EurPrice = data.EURPrice,
                        Token = relevantToken
                    }).ToList();

                    this.galaxisContext.TokenPriceHistoricDatas.AddRange(dataModelTokenPriceHistory);
                    this.galaxisContext.SaveChanges();
                }
            }
        }

        private DataModelToken GetRelevantToken(string baseTokenSymbol)
        {
            return this.galaxisContext
                .Tokens
                .FirstOrDefault(token => token.Symbol == baseTokenSymbol);
        }

        public async Task<int> DeleteTokenPriceHistoryAsync(string baseTokenSymbol, uint timeStamp)
        {
            var relevant = await this.galaxisContext.TokenPriceHistoricDatas
                .Include(x => x.Token)
                .Where(x => x.Token.Symbol == baseTokenSymbol && x.Timestamp == timeStamp)
                .ToListAsync();

            this.galaxisContext.TokenPriceHistoricDatas.RemoveRange(relevant);
            return this.galaxisContext.SaveChanges();
        }
    }
}