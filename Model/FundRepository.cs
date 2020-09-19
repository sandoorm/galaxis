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
    }
}