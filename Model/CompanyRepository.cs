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
        private readonly GalaxisDbContext galaxisContext;

        public CompanyRepository(GalaxisDbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return this.galaxisContext.Companies;
        }

        public IEnumerable<CompanyAndFunds> GetAllCompaniesAndFunds()
        {
            return this.galaxisContext.Companies
                .Join(this.galaxisContext.Funds,
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

            this.galaxisContext.Companies.Add(company);
            return this.galaxisContext.SaveChangesAsync();
        }
    }
}   