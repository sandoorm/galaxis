using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.Model;
using System.Threading.Tasks;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundRepository fundRepository;

        public FundController(IFundRepository fundRepository)
        {
            this.fundRepository = fundRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataModel.Fund>> GetAllFunds()
        {
            return Ok(new List<DataModel.Fund>
            {
                new DataModel.Fund
                {
                    FundName = "Best Fund",
                    InvestmentFundManagerName = "Sandor Maraczy",
                    FloorLevel = 0.7
                },
                new DataModel.Fund
                {
                    FundName = "Galaxis Fund LTD",
                    InvestmentFundManagerName = "Mate Brezovszki",
                    FloorLevel = 0.6
                }
            });

            //IEnumerable<Model.Fund> allDbFunds = this.fundRepository.GetAllFundsAsync();
            //allDbFunds.Concat(staticFunds.Select(fund => new Model.Fund(fund)).ToList()).ToList();
            //List<Model.Fund> list = allDbFunds.ToList();
            //return Ok(allDbFunds);
        }

        [HttpGet("{id}")]
        public ActionResult<int> GetFundById(int id) 
        {
            return id;
        }

        [HttpPost]
        public async Task<int> CreateFund()
        {
            return await this.fundRepository.CreateFundAsync();
        }

        [HttpPut("{id}")]
        public int ModifyFund()
        {
            return 3;
        }

        [HttpDelete("{id}")]
        public int DeleteFund()
        {
            return 3;
        }
    }
}