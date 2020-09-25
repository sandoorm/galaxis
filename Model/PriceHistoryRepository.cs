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

        public async Task<List<PriceHistoricData>> GetAllHistoricPriceDataAsync(string baseTokenSymbol)
        {
            DataModelToken foundToken = GetRelevantToken(baseTokenSymbol);

            return foundToken != null
                ? GetTokenHistory(baseTokenSymbol, foundToken.Id)
                : new List<PriceHistoricData>();
        }

        private DataModelToken GetRelevantToken(string baseTokenSymbol)
        {
            return this.galaxisContext
                .Tokens
                .FirstOrDefault(token => token.Symbol == baseTokenSymbol);
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
                    USD_Price = historicData.USD_Price,
                    EUR_Price = historicData.EUR_Price
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
                        USD_Price = data.USD_Price,
                        EUR_Price = data.EUR_Price,
                        Token = relevantToken
                    }).ToList();

                    dataModelTokenPriceHistory.ForEach(data => this.galaxisContext.TokenPriceHistoricDatas.Add(data));
                    this.galaxisContext.SaveChanges();
                }
            }
        }
    }
}