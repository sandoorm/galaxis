﻿using Microsoft.EntityFrameworkCore;

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
            DataModelToken foundToken = await GetRelevantToken(baseTokenSymbol);

            return foundToken != null
                ? GetTokenHistory(baseTokenSymbol, foundToken.Id)
                : new List<PriceHistoricData>();
        }

        private async Task<DataModelToken> GetRelevantToken(string baseTokenSymbol)
        {
            return await this.galaxisContext
                .Tokens
                .FirstOrDefaultAsync(token => token.Symbol == baseTokenSymbol);
        }

        private List<PriceHistoricData> GetTokenHistory(string baseTokenSymbol, int tokenId)
        {
            return this.galaxisContext
                .TokenPriceHistory
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

        public async Task AddTokenPriceHistoryDatasAsync(List<PriceHistoricData> priceHistoricDatas)
        {
            var priceHistoricData = priceHistoricDatas.FirstOrDefault();
            if (priceHistoricData != null)
            {
                var relevantToken = await GetRelevantToken(priceHistoricData.BaseTokenSymbol);
                if (relevantToken != null)
                {
                    var dataModelTokenPriceHistory = priceHistoricDatas
                    .Select(data => new TokenPriceHistory
                    {
                        TokenId = relevantToken.Id,
                        Timestamp = data.Timestamp,
                        USD_Price = data.USD_Price,
                        EUR_Price = data.EUR_Price
                    }).ToList();

                    this.galaxisContext.TokenPriceHistory.AddRange(dataModelTokenPriceHistory);
                    await this.galaxisContext.SaveChangesAsync();
                }
            }
        }
    }
}