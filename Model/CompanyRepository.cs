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

        public Task<int> CreateCompanyAsync(CompanyCreateRequest companyCreateRequest)
        {
            var company = new Company
            {
                CompanyName = companyCreateRequest.CompanyName,
                CompanyAddress = companyCreateRequest.CompanyAddress,
                RegistrationNumber = companyCreateRequest.RegistrationNumber,
                vatNumber = companyCreateRequest.vatNumber,
                OfficialEmailAddress = companyCreateRequest.OfficialEmailAddress,
                ContactPersonName =  companyCreateRequest.ContactPersonName,
                Position = companyCreateRequest.Position,
                ContactPersonPhoneNumber = companyCreateRequest.ContactPersonPhoneNumber,
                ContactPersonEmail = companyCreateRequest.ContactPersonEmail,

                Funds = new List<DataModelFund>()
            };

            this.galaxisContext.Companies.Add(company);
            return this.galaxisContext.SaveChangesAsync();
        }
    }
}   