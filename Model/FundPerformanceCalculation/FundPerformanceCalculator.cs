using Microsoft.EntityFrameworkCore;

using System;
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
        private readonly int timeRange = 86400;
        private readonly int hardcodedHourToAdd = 7;
        private readonly GalaxisDbContext galaxisContext;

        public FundPerformanceCalculator(GalaxisDbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public async Task<FundPerformance> CalculateFundPerformance(string fundAddress, int intervalInDays)
        {
            var fund = await GetFundAsync(fundAddress);

            DateTime currentDateTime = DateTime.UtcNow;
            uint todayTimeStamp = GetTimeStampAdjusted(currentDateTime);

            DateTime depositDateTime = UnixTimeStampToDateTime(fund.DepositStartTimeStamp);
            var startDateTime = new DateTime(depositDateTime.Year, depositDateTime.Month, depositDateTime.Day, hardcodedHourToAdd, 0, 0);
            uint startTimeStamp = (uint)startDateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            uint diff = todayTimeStamp - startTimeStamp;
            int resultCount = (int)(diff / timeRange);
            var resultList = new List<long>();

            for (int i = 0; i < resultCount; i++)
            {
                resultList.Add(startTimeStamp + (i * timeRange));
            }

            if (currentDateTime > GetDateTimeAdjusted(currentDateTime))
            {
                resultList.Add(resultList.Last() + timeRange);
            }

            var timeStampResults = resultList.TakeLast(intervalInDays).Select(x => (uint)x).ToList();

            List<DataModelFundToken> joinedFundTokens = await this.galaxisContext
                .FundTokens
                .Include(item => item.Token)
                .Where(x => x.FundId == fund.Id)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();

            var fundTokenMapping = MapFundTokensToRelevantTimeStamp(
                timeStampResults,
                joinedFundTokens);

            TokenPriceHistoricData[] priceHistory = await this.galaxisContext.TokenPriceHistoricDatas
                .Include(x => x.Token)
                .ToArrayAsync();

            var groupedPriceHistory = priceHistory.GroupBy(
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
                .ToArray();

            var performanceResultDatas = new List<PerformanceResultData>();
            double initialPerformanceValue = 0;

            for (int i = 0; i < finalResult.Length; i++)
            {
                var resultElement = finalResult[i];
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
                        if (item.TokenSymbol == "CDAI")
                        {
                            var daiPriceResult = currentPriceDetails
                                .Result
                                .FirstOrDefault(x => x.Symbol == "DAI");

                            if (daiPriceResult != null)
                            {
                                // calculation given by Mate:
                                // (cdai quantity * CDAI price) + (cdai quantity * DAI Price)
                                var cdaiValue = (item.Quantity * matchingPriceDetail.UsdPrice)
                                    + (item.Quantity * daiPriceResult.UsdPrice);
                                currResultValue += cdaiValue;
                            }
                        }

                        currResultValue += item.Quantity * matchingPriceDetail.UsdPrice;
                    }
                }

                uint currResultTimeStamp = resultElement.PriceDetails.Key.Timestamp;
                double performanceValue = Math.Round(currResultValue, 2);

                if (i == 0)
                {
                    initialPerformanceValue = performanceValue;
                }

                performanceResultDatas.Add(new PerformanceResultData
                {
                    TimeStamp = currResultTimeStamp,
                    PerformanceValue = performanceValue,
                    PerformancePercentage = Math.Round(((performanceValue / initialPerformanceValue) * 100), 2),
                });
            }

            return new FundPerformance { PerformanceResultDatas = performanceResultDatas };
        }

        private DateTime GetDateTimeAdjusted(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hardcodedHourToAdd, 0, 0);
        }

        private uint GetTimeStampAdjusted(DateTime dateTime)
        {
            var dateTimeAdjusted = GetDateTimeAdjusted(dateTime);
            uint todayTimeStamp = (uint)dateTimeAdjusted.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return todayTimeStamp;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dtDateTime.ToUniversalTime();
        }

        private Dictionary<uint, List<TokenAllocationInfo>> MapFundTokensToRelevantTimeStamp(List<uint> timeStampResults, List<DataModelFundToken> joinedFundTokens)
        {
            var groupedFundTokens = joinedFundTokens.GroupBy(
                            x => new { x.Timestamp },
                            x => new { x.Token.Symbol, x.Quantity },
                            (key, result) => new { Key = key, TokenSymbolAndQuantity = result })
                            .OrderByDescending(x => x.Key.Timestamp)
                            .ToList();

            Dictionary<uint, List<TokenAllocationInfo>> fundTokenMapping = new Dictionary<uint, List<TokenAllocationInfo>>();

            // timestamp matching between result timestamps and grouped fund tokens by timestamps
            uint[] groupedFundTokenTimeStamps = groupedFundTokens.Select(token => token.Key.Timestamp).ToArray();
            List<Tuple<uint, int>> diffs = CalculateTimeStampDiffs(timeStampResults, groupedFundTokenTimeStamps);

            int fundTokenCount = groupedFundTokens.Count;
            for (int i = 0; i < timeStampResults.Count; i++)
            {
                List<TokenAllocationInfo> allocationInfos = new List<TokenAllocationInfo>();
                int relevantIndex = diffs.First(tuple => tuple.Item1 == timeStampResults[i]).Item2;

                foreach (var info in groupedFundTokens[relevantIndex].TokenSymbolAndQuantity)
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

        private List<Tuple<uint, int>> CalculateTimeStampDiffs(List<uint> timeStampResults, uint[] groupedFundTokenTimeStamps)
        {
            int fundTokenLength = groupedFundTokenTimeStamps.Length;

            List<Tuple<uint, int>> fundTokenIndexByTimeStamps = new List<Tuple<uint, int>>();

            for (int i = 0; i < timeStampResults.Count; i++)
            {
                // fix
                int currTimeStamp = (int)timeStampResults[i];
                List<Tuple<int, int>> diffsByIndexes = new List<Tuple<int, int>>();
                for (int j = 0; j < fundTokenLength; j++)
                {
                    int currentFundTokenTimeStamp = (int)groupedFundTokenTimeStamps[j];
                    diffsByIndexes.Add(
                        Tuple.Create(Math.Abs(currTimeStamp - currentFundTokenTimeStamp), j));
                }

                int minDiff = diffsByIndexes.Min(tuple => tuple.Item1);

                //overkill, needs optimization
                int resultIndex = diffsByIndexes.First(x => x.Item1 == minDiff).Item2;
                fundTokenIndexByTimeStamps.Add(Tuple.Create((uint)currTimeStamp, resultIndex));
            }

            return fundTokenIndexByTimeStamps;
        }

        private async Task<DataModelFund> GetFundAsync(string fundAddress)
        {
            return await this.galaxisContext
                .Funds
                .FirstOrDefaultAsync(fund => fund.FundAddress == fundAddress);
        }
    }
}