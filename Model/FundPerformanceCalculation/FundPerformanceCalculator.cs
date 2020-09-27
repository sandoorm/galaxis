using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.Infrastructure;
using GalaxisProjectWebAPI.Model.Token;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;
using DataModelFundToken = GalaxisProjectWebAPI.DataModel.FundToken;

namespace GalaxisProjectWebAPI.Model.FundPerformanceCalculation
{
    public class FundPerformanceCalculator : IFundPerformanceCalculator
    {
        private readonly int timeRange = 3600;
        private readonly GalaxisDbContext galaxisContext;

        public FundPerformanceCalculator(GalaxisDbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public async Task<FundPerformance> CalculateFundPerformance(string fundAddress)
        {
            var fund = await GetFundAsync(fundAddress);
            uint currentTimeStamp = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            uint diff = currentTimeStamp - fund.DepositStartTimeStamp;

            int resultCount = (int)(diff / timeRange);
            var resultList = new List<long>();
            for (int i = 0; i < resultCount; i++)
            {
                resultList.Add(fund.DepositStartTimeStamp + (i * timeRange));
            }

            var timeStampResults = resultList.Select(x => (uint)x).ToList();

            List<DataModelFundToken> joinedFundTokens = await this.galaxisContext
                .FundTokens
                .Include(item => item.Token)
                .Where(x => x.FundId == fund.Id)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();

            var fundTokenMapping = MapFundTokensToRelevantTimeStamp(
                resultCount,
                timeStampResults,
                joinedFundTokens);

            var priceHistory = await this.galaxisContext.TokenPriceHistoricDatas
                .Include(x => x.Token)
                .ToListAsync();

            var groupedPriceHistory = priceHistory.GroupBy(
                x => new { x.Timestamp },
                x => new { x.Token.Symbol, x.UsdPrice },
                (key, result) => new
                {
                    Key = key,
                    Result = result
                }).ToList();

            var finalResult = fundTokenMapping.Keys.Join(groupedPriceHistory,
                x => x,
                y => y.Key.Timestamp,
                (x, y) => new { TokenAllocationDetails = x, TokenPriceDetails = y })
                .ToList();

            //foreach (var resultElement in finalResult)
            //{
            //    var currentAllocation = resultElement.TokenAllocationDetails;
            //    var currentPriceDetails = resultElement.TokenPriceDetails;

            //    foreach (var item in currentAllocation.TokenSymbolAndQuantity)
            //    {
            //        var matchingPriceDetail = currentPriceDetails
            //            .Result
            //            .FirstOrDefault(x => x.Symbol == item.Symbol);

            //        if (matchingPriceDetail != null)
            //        {
            //            var value = item.Quantity * matchingPriceDetail.UsdPrice;
            //        }
            //    }

            //    var quantityInfo = currentAllocation.TokenSymbolAndQuantity;
            //    var cucc = currentPriceDetails.Result;
            //}

            return new FundPerformance();
        }

        private Dictionary<uint, List<TokenAllocationInfo>> MapFundTokensToRelevantTimeStamp(int resultCount, List<uint> timeStampResults, List<DataModelFundToken> joinedFundTokens)
        {
            var groupedFundTokens = joinedFundTokens.GroupBy(
                            x => new { x.Timestamp },
                            x => new { x.Token.Symbol, x.Quantity },
                            (key, result) => new { Key = key, TokenSymbolAndQuantity = result })
                            .OrderBy(x => x.Key.Timestamp)
                            .ToList();

            Dictionary<uint, List<TokenAllocationInfo>> fundTokenMapping = new Dictionary<uint, List<TokenAllocationInfo>>();

            int fundTokenCount = groupedFundTokens.Count;
            for (int i = 0; i < resultCount; i++)
            {
                List<TokenAllocationInfo> allocationInfos = new List<TokenAllocationInfo>();

                int index = i < fundTokenCount ? i : fundTokenCount - 1;
                foreach (var info in groupedFundTokens[index].TokenSymbolAndQuantity)
                {
                    allocationInfos.Add(new TokenAllocationInfo
                    {
                        TokenSymbol = info.Symbol,
                        Quantity = info.Quantity
                    });
                }

                fundTokenMapping.Add(timeStampResults[i], allocationInfos);
            }

            return fundTokenMapping;
        }

        private async Task<DataModelFund> GetFundAsync(string fundAddress)
        {
            return await this.galaxisContext
                .Funds
                .FirstOrDefaultAsync(fund => fund.FundAddress == fundAddress);
        }
    }
}