using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Infrastructure;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;
using GalaxisProjectWebAPI.Model.Token;

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
            return await this.galaxisContext.Funds.ToListAsync();
        }

        public async Task<ActionResult<DataModelFund>> GetFundByIdAsync(int fundId)
        {
            return await this.galaxisContext.Funds.FindAsync(fundId);
        }

        public async Task<ActionResult<TokenList<FundTokenInfo>>> GetFundAndTokensAsync(int fundId)
        {
            var joinedFundTokens = await this.galaxisContext
                .FundTokens
                .Include(item => item.Token)
                .Where(x => x.FundId == fundId)
                .ToListAsync();

            var groupedFundTokens = joinedFundTokens.GroupBy(
                x => new { x.Token.Id, x.Token.Symbol },
                x => new { x.Timestamp, x.Quantity },
                (key, result) => new { TokenDescriptor = key, QuantitiesByTimestamp = result });

            var relevantFundTokenList = new TokenList<FundTokenInfo>();
            foreach (var group in groupedFundTokens)
            {
                var relevantFundToken = group.QuantitiesByTimestamp.OrderByDescending(x => x.Timestamp).First();
                relevantFundTokenList.TokenInfos.Add(new FundTokenInfo
                {
                    TokenId = group.TokenDescriptor.Id,
                    TokenSymbol = group.TokenDescriptor.Symbol,
                    TimeStamp = relevantFundToken.Timestamp,
                    Quantity = relevantFundToken.Quantity
                });
            }

            return relevantFundTokenList;
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
                FundName = fundCreateRequest.FundName,
                InvestmentFundManagerName = fundCreateRequest.InvestmentFundManagerName,
                FloorLevel = fundCreateRequest.FloorLevel,
                Company = company
            };
        }

        public async Task<int> CreateFundTokenAsync(int fundId, FundTokenCreateRequest fundTokenCreateRequest)
        {
            var requestedFund = await this.galaxisContext.Funds.FindAsync(fundId);
            var tokenToAssign = await this.galaxisContext.Tokens.FindAsync(1);

            // this.dbContext.Tokens
            //.FirstOrDefault(token => token.Name.Equals(fundTokenCreateRequest.TokenName));

            requestedFund.FundTokens.Add(new FundToken
            {
                FundId = fundId,
                Quantity = fundTokenCreateRequest.Quantity,
                Timestamp = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                TokenId = tokenToAssign.Id
            });

            await this.galaxisContext.SaveChangesAsync();
            return 0;
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
    }
}