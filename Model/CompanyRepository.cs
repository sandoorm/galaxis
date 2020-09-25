using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
            // no need for Select!!
            return this.galaxisContext.Companies
                .Join(this.galaxisContext.Funds,
                company => company.Id,
                fund => fund.CompanyId,
                (company, fund) => new CompanyAndFunds
                {
                    CompanyName = company.CompanyName,
                    Fund = fund
                });
        }

        public async Task<int> CreateCompanyAsync(CompanyCreateRequest companyCreateRequest)
        {
            Company company = null;
            company = await this.galaxisContext
                .Companies
                .FirstOrDefaultAsync(x => x.CompanyName == companyCreateRequest.CompanyName
                && x.ContactPersonEmail == companyCreateRequest.ContactPersonEmail);

            int result = 0;
            if (company == null)
            {
                company = new Company
                {
                    CompanyName = companyCreateRequest.CompanyName,
                    CompanyAddress = companyCreateRequest.CompanyAddress,
                    RegistrationNumber = companyCreateRequest.RegistrationNumber,
                    vatNumber = companyCreateRequest.vatNumber,
                    OfficialEmailAddress = companyCreateRequest.OfficialEmailAddress,
                    ContactPersonName = companyCreateRequest.ContactPersonName,
                    Position = companyCreateRequest.Position,
                    ContactPersonPhoneNumber = companyCreateRequest.ContactPersonPhoneNumber,
                    ContactPersonEmail = companyCreateRequest.ContactPersonEmail,

                    Funds = new List<DataModelFund>()
                };

                this.galaxisContext.Companies.Add(company);
                result = await this.galaxisContext.SaveChangesAsync();
            }

            return result;
        }
    }
}   