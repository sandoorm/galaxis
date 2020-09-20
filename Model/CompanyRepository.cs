using System.Collections.Generic;
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