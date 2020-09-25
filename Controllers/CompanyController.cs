using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.DataModel;
using GalaxisProjectWebAPI.Model;

namespace GalaxisProjectWebAPI.Controllers
{
    [EnableCors("AllowAnyOriginPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController
    {
        ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [EnableCors("AllowAnyOriginPolicy")]
        [HttpGet("GetAllCompanies")]
        public IEnumerable<Company> GetAllCompanies()
        {
            return companyRepository.GetAllCompanies();
        }

        [EnableCors("AllowAnyOriginPolicy")]
        [HttpGet("GetAllCompaniesWithFunds")]
        public IEnumerable<CompanyAndFunds> GetAllCompaniesWithFunds()
        {
            return companyRepository.GetAllCompaniesAndFunds();
        }

        [EnableCors("AllowAnyOriginPolicy")]
        [HttpPost("Create")]
        public Task<int> CreateCompanyAsync([FromBody] CompanyCreateRequest companyCreateRequest)
        {
            return companyRepository.CreateCompanyAsync(companyCreateRequest);
        }
    }
}