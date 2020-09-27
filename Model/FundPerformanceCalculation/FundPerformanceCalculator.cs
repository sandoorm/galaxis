using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.Infrastructure;
using System.Collections.Generic;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;
using DataModelFundToken = GalaxisProjectWebAPI.DataModel.FundToken;

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

        public async Task<FundPerformance> CalculateFundPerformance(string fundAddress, string timeStampFrom, string timeStampTo)
        {
            Tuple<uint, uint> timeStampResults;
            if ((timeStampResults = TryParseTimeStamps(timeStampFrom, timeStampTo)) != null)
            {
                uint timeStampFromResult = timeStampResults.Item1;
                uint timeStampToResult = timeStampResults.Item2;

                uint timeStampDistance = timeStampToResult - timeStampFromResult;
                int resultCount = (int)(timeStampDistance / timePeriodInSecods);

                var fund = await GetFundAsync(fundAddress);
                List<DataModelFundToken> joinedFundTokens = await this.galaxisContext
                    .FundTokens
                    .Include(item => item.Token)
                    .Where(x => x.FundId == fund.Id)
                    .ToListAsync();

                //var relevantFundTokens = joinedFundTokens
                //    .Where(fundToken =>
                //       fundToken.Timestamp >= timeStampResults.Item1
                //    && fundToken.Timestamp <= timeStampResults.Item2)
                //    .ToList();

                var groupedFundTokens = joinedFundTokens.GroupBy(
                    x => new { x.Timestamp },
                    x => new { x.Token.Symbol, x.Quantity },
                    (key, result) => new { Key = key, TokenSymbolAndQuantity = result })
                    .OrderByDescending(x => x.Key.Timestamp)
                    .ToList();

                var priceHistory = await this.galaxisContext.TokenPriceHistoricDatas
                    .Include(x => x.Token)
                    .ToListAsync();

                var res = priceHistory.GroupBy(
                    x => new { x.Timestamp },
                    x => new { x.Token.Symbol, x.UsdPrice },
                    (key, result) => new
                    {
                        Key = key,
                        Result = result
                    }).ToList();

                var finalResult = groupedFundTokens.Join(res,
                    x => x.Key.Timestamp,
                    y => y.Key.Timestamp,
                    (x, y) => new { TokenAllocationDetails = x, TokenPriceDetails = y })
                    .ToList();

                foreach (var resultElement in finalResult)
                {
                    var currentAllocation = resultElement.TokenAllocationDetails;
                    var currentPriceDetails = resultElement.TokenPriceDetails;

                    foreach (var item in currentAllocation.TokenSymbolAndQuantity)
                    {
                        var matchingPriceDetail = currentPriceDetails
                            .Result
                            .FirstOrDefault(x => x.Symbol == item.Symbol);

                        if (matchingPriceDetail != null)
                        {
                            var value = item.Quantity * matchingPriceDetail.UsdPrice;
                        }
                    }

                    var quantityInfo = currentAllocation.TokenSymbolAndQuantity;
                    var cucc = currentPriceDetails.Result;
                }

                

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

        private async Task<DataModelFund> GetFundAsync(string fundAddress)
        {
            return await this.galaxisContext
                .Funds
                .FirstOrDefaultAsync(fund => fund.FundAddress == fundAddress);
        }
    }
}