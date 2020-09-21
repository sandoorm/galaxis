using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Model
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies();

        IEnumerable<CompanyAndFunds> GetAllCompaniesAndFunds();

        Task<int> CreateCompanyAsync(CompanyCreateRequest companyCreateRequest);
    }
}