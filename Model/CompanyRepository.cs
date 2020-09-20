using System.Collections.Generic;

using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Infrastructure;

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
    }
}   