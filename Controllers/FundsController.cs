using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Fund>> GetAllFunds()
        {
            return Ok(new List<Fund>
            {
                new Fund
                {
                    FundName = "Best Fund",
                    InvestmentFundManagerName = "Sandor Maraczy",
                    FloorLevel = 0.7
                },
                new Fund
                {
                    FundName = "Galaxis Fund LTD",
                    InvestmentFundManagerName = "Mate Brezovszki",
                    FloorLevel = 0.6
                }
            });
        }
    }
}