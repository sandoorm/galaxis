using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Model;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController
    {
        ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpGet]
        public IEnumerable<Company> GetAllCompanies()
        {
            return companyRepository.GetAllCompanies();
        }
    }
}