using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.Infrastructure;

namespace GalaxisProjectWebAPI.Model
{
    public class FundRepository : IFundRepository
    {
        private readonly ApplicationDbContext dbContext;

        public FundRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Fund> GetAllFundsAsync()
        {
            return this.dbContext.Funds.Select(fund => new Model.Fund(fund));
        }

        public async Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest)
        {
            DataModel.Fund fund = new DataModel.Fund
            {
                FundName = fundCreateRequest.FundName,
                InvestmentFundManagerName = fundCreateRequest.InvestmentFundManagerName,
                FloorLevel = fundCreateRequest.FloorLevel,
                Company = new DataModel.Company
                {
                    Address = fundCreateRequest.Address,
                    CompanyName = fundCreateRequest.CompanyName
                }      
            };

            try
            {
                this.dbContext.Funds.Add(fund);
                return await this.dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                    e = e.InnerException;
            }

            return 0;
        }

        public async Task<int> CreateFundToken(int id, FundTokenCreateRequest fundTokenCreateRequest)
        {
            var requestedFund = await this.dbContext.Funds.FindAsync(id);
            var tokenToAssign = await this.dbContext.Tokens.FindAsync(2);

            // this.dbContext.Tokens
            //.FirstOrDefault(token => token.Name.Equals(fundTokenCreateRequest.TokenName));

            requestedFund.FundTokens.Add(new DataModel.FundToken
            {
                FundId = id,
                Quantity = 2,
                Timestamp = 1600516876,
                TokenId = tokenToAssign.Id
            });

            await this.dbContext.SaveChangesAsync();
            return 0;
        }
    }
}