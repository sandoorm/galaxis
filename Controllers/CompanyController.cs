using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController
    {
        [HttpGet]
        public ActionResult<IEnumerable<Company>> GetAllCompanies()
        {
            return new List<Company>
            {
                new Company
                {
                    CompanyName = "Galaxis",
                    Address = "Nicest str. 21."
                },
                new Company
                {
                    CompanyName = "Best Company",
                    Address = "Company str. 34."
                }
            };
        }
    }
}