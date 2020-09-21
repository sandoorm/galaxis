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

namespace GalaxisProjectWebAPI.Model
{
    public class FundRepository : IFundRepository
    {
        private readonly ApplicationDbContext dbContext;

        public FundRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Fund> GetAllFunds()
        {
            return this.dbContext.Funds.Select(fund => new Fund(fund));
        }

        public async Task<ActionResult<TokenList>> GetFundAndTokensAsync(int fundId)
        {
            var joinedFundTokens = await this.dbContext
                .FundTokens
                .Include(item => item.Token)
                .Where(x => x.FundId == fundId)
                .ToListAsync();

            var groupedFundTokens = joinedFundTokens.GroupBy(
                x => new { x.Token.Id, x.Token.Symbol },
                x => new { x.Timestamp, x.Quantity },
                (key, result) => new { TokenDescriptor = key, QuantitiesByTimestamp = result });

            var relevantFundTokenList = new TokenList();
            foreach (var group in groupedFundTokens)
            {
                var relevantFundToken = group.QuantitiesByTimestamp.OrderByDescending(x => x.Timestamp).First();
                relevantFundTokenList.TokenInfos.Add(new TokenInfo
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
            var company = fundCreateRequest.CompanyId == 0
                ? CreateCompany(fundCreateRequest)
                : await this.dbContext.Companies.FindAsync(fundCreateRequest.CompanyId);

            _ = company ?? throw new ArgumentException();

            DataModelFund fund = new DataModelFund
            {
                FundName = fundCreateRequest.FundName,
                InvestmentFundManagerName = fundCreateRequest.InvestmentFundManagerName,
                FloorLevel = fundCreateRequest.FloorLevel,
                Company = company
            };

            AssignFundToCompany(company, fund);

            this.dbContext.Funds.Add(fund);
            return await this.dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateFundToken(int fundId, FundTokenCreateRequest fundTokenCreateRequest)
        {
            var requestedFund = await this.dbContext.Funds.FindAsync(fundId);
            var tokenToAssign = await this.dbContext.Tokens.FindAsync(1);

            // this.dbContext.Tokens
            //.FirstOrDefault(token => token.Name.Equals(fundTokenCreateRequest.TokenName));

            requestedFund.FundTokens.Add(new FundToken
            {
                FundId = fundId,
                Quantity = fundTokenCreateRequest.Quantity,
                Timestamp = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                TokenId = tokenToAssign.Id
            });

            await this.dbContext.SaveChangesAsync();
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

        private Company CreateCompany(FundCreateRequest fundCreateRequest)
        {
            return new Company
            {
                CompanyName = fundCreateRequest.CompanyName,
                Address = fundCreateRequest.Address
            };
        }
    }
}