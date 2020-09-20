using System.Collections.Generic;

using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Model
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies();
    }
}