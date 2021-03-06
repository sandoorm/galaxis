﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.ApiModel;
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

        [HttpGet("GetAllCompanies")]
        public IEnumerable<Company> GetAllCompanies()
        {
            return companyRepository.GetAllCompanies();
        }

        [HttpGet("GetAllCompaniesWithFunds")]
        public IEnumerable<CompanyAndFunds> GetAllCompaniesWithFunds()
        {
            return companyRepository.GetAllCompaniesAndFunds();
        }

        [HttpPost("Create")]
        public Task<int> CreateCompanyAsync([FromBody] CompanyCreateRequest companyCreateRequest)
        {
            return companyRepository.CreateCompanyAsync(companyCreateRequest);
        }
    }
}