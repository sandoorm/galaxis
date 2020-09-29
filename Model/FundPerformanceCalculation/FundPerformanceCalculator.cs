using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.Infrastructure;
using GalaxisProjectWebAPI.DataModel;
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
            //uint currentTimeStamp = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            uint hardCodedTimeStamp = 1601316060;
            uint diff = hardCodedTimeStamp - fund.DepositStartTimeStamp;

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

            TokenPriceHistoricData[] priceHistory = await this.galaxisContext.TokenPriceHistoricDatas
                .Include(x => x.Token)
                .ToArrayAsync();

            var relevantPriceHistory = ApplyHackOnTimeStamps(fundTokenMapping, priceHistory);

            var groupedPriceHistory = relevantPriceHistory.GroupBy(
                x => new { x.Timestamp },
                x => new { x.Token.Symbol, x.UsdPrice },
                (key, result) => new
                {
                    Key = key,
                    Result = result
                }).ToList();

            var finalResult = fundTokenMapping.Join(groupedPriceHistory,
                x => x.Key,
                y => y.Key.Timestamp,
                (x, y) => new { AllocationDetails = x, PriceDetails = y })
                .ToList();

            var resultDictionary = new Dictionary<uint, double>();
            foreach (var resultElement in finalResult)
            {
                var currentAllocation = resultElement.AllocationDetails;
                var currentPriceDetails = resultElement.PriceDetails;

                double currResultValue = 0;
                foreach (var item in currentAllocation.Value)
                {
                    var matchingPriceDetail = currentPriceDetails
                        .Result
                        .FirstOrDefault(x => x.Symbol == item.TokenSymbol);

                    if (matchingPriceDetail != null)
                    {
                        //if (item.TokenSymbol == "CDAI")
                        //{
                        //    currResultValue += (item.Quantity * matchingPriceDetail.UsdPrice)
                        //        + (item.Quantity * DAI Price)
                        //}

                        currResultValue += item.Quantity * matchingPriceDetail.UsdPrice;
                    }
                }

                uint currResultTimeStamp = resultElement.PriceDetails.Key.Timestamp;
                resultDictionary.Add(currResultTimeStamp, currResultValue);
            }

            //    var quantityInfo = currentAllocation.TokenSymbolAndQuantity;
            //    var cucc = currentPriceDetails.Result;
            var performance = new FundPerformance();
            performance.FundValuesByTimeStamps = resultDictionary;

            return performance;
        }

        private TokenPriceHistoricData[] ApplyHackOnTimeStamps(Dictionary<uint, List<TokenAllocationInfo>> fundTokenMapping, TokenPriceHistoricData[] priceHistory)
        {
            var etherResult = priceHistory.Where(x => x.Token.Symbol == "ETH").OrderBy(x => x.Timestamp).ToArray();
            var wethResult = priceHistory.Where(x => x.Token.Symbol == "WETH").OrderBy(x => x.Timestamp).ToArray();
            var daiResult = priceHistory.Where(x => x.Token.Symbol == "DAI").OrderBy(x => x.Timestamp).ToArray();
            var cdaiResult = priceHistory.Where(x => x.Token.Symbol == "CDAI").OrderBy(x => x.Timestamp).ToArray();
            HackAllTypesOfHistoricData(etherResult, fundTokenMapping.Keys.ToList());

            return priceHistory;
        }

        private TokenPriceHistoricData[] HackAllTypesOfHistoricData(TokenPriceHistoricData[] priceHistory, List<uint> keys)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                uint relevantKey = keys[i];
                priceHistory[i].Timestamp = relevantKey;
            }

            return priceHistory;
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