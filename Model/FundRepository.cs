using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Model.Token;
using GalaxisProjectWebAPI.Infrastructure;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;
using DataModelFundToken = GalaxisProjectWebAPI.DataModel.FundToken;

namespace GalaxisProjectWebAPI.Model
{
    public class FundRepository : IFundRepository
    {
        private readonly GalaxisDbContext galaxisContext;

        public FundRepository(GalaxisDbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public async Task<IEnumerable<DataModelFund>> GetAllFundsAsync()
        {
            var funds = await this.galaxisContext.Funds.ToListAsync();
            return funds.OrderBy(x => x.Id);
        }

        public async Task<ActionResult<DataModelFund>> GetFundByAddressAsync(string fundAddress)
        {
            return await GetFundAsync(fundAddress);
        }

        public async Task<ActionResult<TokenList<FundTokenInfo>>> GetFundAndTokensAsync(string fundAddress)
        {
            var fund = await GetFundAsync(fundAddress);
            if (fund == null)
            {
                return null;
            }

            var joinedFundTokens = await this.galaxisContext
                .FundTokens
                .Include(item => item.Token)
                .Where(x => x.FundId == fund.Id)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();

            IGrouping<uint, DataModelFundToken> lastSnapshotGroup = joinedFundTokens
                .GroupBy(x => x.Timestamp)
                .First();

            var resultFundTokenInfos = lastSnapshotGroup.Select(ConvertToFundTokenInfo).ToList();
            return new TokenList<FundTokenInfo>(resultFundTokenInfos);
        }

        private FundTokenInfo ConvertToFundTokenInfo(DataModelFundToken fundToken)
        {
            return new FundTokenInfo
            {
                TokenId = fundToken.TokenId,
                TokenSymbol = fundToken.Token.Symbol,
                Quantity = fundToken.Quantity,
                TimeStamp = fundToken.Timestamp
            };
        }

        public async Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest)
        {
            Company company = null;
            if (fundCreateRequest.CompanyId != 0)
            {
                company = await this.galaxisContext.Companies.FindAsync(fundCreateRequest.CompanyId);
            }

            int result = 0;
            if (company != null)
            {
                var fund = CreateDataModelFund(fundCreateRequest, company);
                AssignFundToCompany(company, fund);

                this.galaxisContext.Funds.Add(fund);
                result = await this.galaxisContext.SaveChangesAsync();
            }

            return result;
        }

        private static DataModelFund CreateDataModelFund(FundCreateRequest fundCreateRequest, Company company)
        {
            return new DataModelFund
            {
                Name = fundCreateRequest.Name,
                FundAddress = fundCreateRequest.Address,
                InvestmentFundManager = fundCreateRequest.InvestmentFundManager,
                InvestmentFocus = fundCreateRequest.InvestmentFocus,
                HighWaterMark = fundCreateRequest.HighWaterMark,
                HurdleRate = fundCreateRequest.HurdleRate,
                HurdleRatePercentage = fundCreateRequest.HurdleRatePercentage,
                DepositStartTimeStamp = fundCreateRequest.DepositStartTimeStamp,
                DepositCloseTimeStamp = fundCreateRequest.DepositCloseTimeStamp,
                CloseTimeStamp = fundCreateRequest.CloseTimeStamp,
                BaseCurrency = fundCreateRequest.BaseCurrency,
                MinimumContribution = fundCreateRequest.MinimumContribution,
                MaximumContribution = fundCreateRequest.MaximumContribution,
                IsLaunched = false,
                Company = company,
                CompanyId = company.Id
            };
        }

        public async Task<int> CreateFundTokensAsync(string fundAddress, FundTokenCreateRequest fundTokenCreateRequest)
        {
            var tokenAllocations = fundTokenCreateRequest.FundTokenAllocations;
            int result = 0;

            if (tokenAllocations != null)
            {
                uint batchTimeStamp = GetCurrentBatchTimeStamp();

                var requestedFund = await GetFundAsync(fundAddress);
                if (requestedFund != null)
                {
                    var fundTokenSnapshot = new List<DataModelFundToken>();
                    foreach (var tokenAllocation in tokenAllocations)
                    {
                        var token = this.galaxisContext.Tokens
                            .FirstOrDefault(t => t.Symbol == tokenAllocation.TokenSymbol);

                        fundTokenSnapshot.Add(new FundToken
                        {
                            FundId = requestedFund.Id,
                            Quantity = tokenAllocation.Quantity,
                            Timestamp = batchTimeStamp,
                            TokenId = token.Id
                        });
                    }

                    await this.galaxisContext.FundTokens.AddRangeAsync(fundTokenSnapshot);
                    result = await this.galaxisContext.SaveChangesAsync();
                }
            }

            return result;
        }

        private async Task<DataModelFund> GetFundAsync(string fundAddress)
        {
            return await this.galaxisContext
                .Funds
                .FirstOrDefaultAsync(fund => fund.FundAddress == fundAddress);
        }

        private uint GetCurrentBatchTimeStamp()
        {
            return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private void AssignFundToCompany(Company company, DataModelFund fund)
        {
            if (company.Funds == null)
            {
                company.Funds = new List<DataModelFund> { fund };
            }
            else
            {
                company.Funds.Add(fund);
            }
        }

        public async Task<int> LaunchFundAsync(string fundAddress)
        {
            var fund = await GetFundAsync(fundAddress);
            fund.IsLaunched = true;

            return this.galaxisContext.SaveChanges();
        }
    }
}