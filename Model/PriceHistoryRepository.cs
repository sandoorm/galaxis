using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.Infrastructure;

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
            var foundToken = await this.galaxisContext.Tokens.FirstOrDefaultAsync(token => token.Symbol == baseTokenSymbol);

            return foundToken != null
                ? GetTokenHistory(baseTokenSymbol, foundToken.Id)
                : new List<PriceHistoricData>();
        }

        private List<PriceHistoricData> GetTokenHistory(string baseTokenSymbol, int tokenId)
        {
            return this.galaxisContext
                .TokenPriceHistory
                .Where(history => history.TokenId == tokenId)
                .Select(historicData => new PriceHistoricData
                {
                    BaseCurrency = baseTokenSymbol,
                    Timestamp = historicData.Timestamp,
                    USD_Price = historicData.USD_Price,
                    EUR_Price = historicData.EUR_Price
                })
                .ToList();
        }
    }
}