using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Infrastructure;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;
using Microsoft.EntityFrameworkCore;

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

        public ActionResult<FundAndTokens> GetFundAndTokensAsync(int fundId)
        {
            var result = this.dbContext
                .FundTokens
                .Include(item => item.Token)
                .Where(x => x.FundId == fundId)
                .ToList();

            //var relevantFundTokens = this.dbContext.FundTokens
            //    .Where(fundToken => fundToken.FundId == fundId);

            //var result = relevantFundTokens.Join(this.dbContext.Tokens,
            //    fundToken => fundToken.TokenId,
            //    token => token.Id,
            //    (fundToken, token) => new
            //    {
            //        fundToken.TokenId,
            //        fundToken.Timestamp,
            //        token.Symbol
            //    }).ToList();

            //var result = this.dbContext.Funds
            //    .Join(this.dbContext.Tokens,
            //    fund => fund.Id,
            //    token => token.,
            //    (company, fund) => new
            //    {
            //        company.CompanyName,
            //        fund.FundName
            //    })
            //    .ToList();

            return null;
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
            var tokenToAssign = await this.dbContext.Tokens.FindAsync(2);

            // this.dbContext.Tokens
            //.FirstOrDefault(token => token.Name.Equals(fundTokenCreateRequest.TokenName));

            requestedFund.FundTokens.Add(new FundToken
            {
                FundId = fundId,
                Quantity = fundTokenCreateRequest.Quantity,
                Timestamp = 1600516886,
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