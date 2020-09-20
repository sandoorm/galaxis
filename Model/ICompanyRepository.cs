using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Model
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies();

        IEnumerable<Company> GetAllCompaniesAndFunds(int companyId);

        Task<int> CreateCompanyAsync(CompanyCreateRequest companyCreateRequest);
    }
}