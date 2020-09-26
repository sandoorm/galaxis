using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.Infrastructure;
using System.Collections.Generic;

using DataModelFundToken = GalaxisProjectWebAPI.DataModel.FundToken;
using System;

namespace GalaxisProjectWebAPI.Model.FundPerformanceCalculation
{
    public class FundPerformanceCalculator : IFundPerformanceCalculator
    {
        private readonly int timePeriodInSecods = 3600;
        private readonly GalaxisDbContext galaxisContext;

        public FundPerformanceCalculator(GalaxisDbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public async Task<FundPerformance> CalculateFundPerformance(int fundId, string timeStampFrom, string timeStampTo)
        {
            Tuple<uint, uint> timeStampResults;
            if ((timeStampResults = TryParseTimeStamps(timeStampFrom, timeStampTo)) != null)
            {
                uint timeStampFromResult = timeStampResults.Item1;
                uint timeStampToResult = timeStampResults.Item2;

                uint timeStampDistance = timeStampToResult - timeStampFromResult;
                int resultCount = (int)(timeStampDistance / timePeriodInSecods);

                List<DataModelFundToken> joinedFundTokens = await this.galaxisContext
                    .FundTokens
                    .Include(item => item.Token)
                    .Where(x => x.FundId == fundId)
                    .ToListAsync();

                var joinedTokenPriceHistory = await this.galaxisContext
                    .Tokens
                    .Join(this.galaxisContext.TokenPriceHistoricDatas,
                    token => token.Id,
                    historicData => historicData.TokenId,
                    (token, priceHistory) => new
                    {
                        token.Symbol,
                        HistoricDatas = priceHistory
                    })
                    .ToListAsync();

                var groupedFundTokens = joinedFundTokens.GroupBy(
                    x => new { x.Timestamp },
                    x => new { x.Token.Symbol, x.Quantity },
                    (key, result) => new { Key = key, TokenSymbolAndQuantity = result })
                    .OrderByDescending(x => x.Key.Timestamp)
                    .ToList();

                var res = joinedFundTokens.Select(x => x.Token.TokenPriceHistoricDatas).ToList();

                var relevantFundTokens = joinedFundTokens
                    .Where(fundToken =>
                       fundToken.Timestamp >= timeStampResults.Item1
                    && fundToken.Timestamp <= timeStampResults.Item2)
                    .ToList();

                return null;
            }

            return null;
        }

        private Tuple<uint, uint> TryParseTimeStamps(string timeStampFrom, string timeStampTo)
        {
            uint timeStampFromResult;
            uint timeStampToResult;

            if (uint.TryParse(timeStampFrom, out timeStampFromResult)
                && uint.TryParse(timeStampTo, out timeStampToResult))
            {
                return new Tuple<uint, uint>(timeStampFromResult, timeStampToResult);
            }

            return null;
        }
    }
}