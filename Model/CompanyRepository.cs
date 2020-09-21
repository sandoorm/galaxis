using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Infrastructure;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;

namespace GalaxisProjectWebAPI.Model
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CompanyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return this.dbContext.Companies;
        }

        public IEnumerable<CompanyAndFunds> GetAllCompaniesAndFunds()
        {
            return this.dbContext.Companies
                .Join(this.dbContext.Funds,
                company => company.Id,
                fund => fund.CompanyId,
                (company, fund) => new
                {
                    company.CompanyName,
                    fund
                })
                .Select(x => new CompanyAndFunds
                {
                    CompanyName = x.CompanyName,
                    Fund = x.fund
                });
        }

        public Task<int> CreateCompanyAsync(CompanyCreateRequest companyCreateRequest)
        {
            var company = new Company
            {
                CompanyName = companyCreateRequest.CompanyName,
                Address = companyCreateRequest.Address,
                Funds = new List<DataModelFund>()
            };

            this.dbContext.Companies.Add(company);
            return this.dbContext.SaveChangesAsync();
        }
    }
}   