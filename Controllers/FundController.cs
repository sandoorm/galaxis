using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.Model;
using System.Threading.Tasks;
using GalaxisProjectWebAPI.ApiModel;

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

        [HttpGet("GetAllFunds")]
        public ActionResult<IEnumerable<DataModel.Fund>> GetAllFunds()
        {
            return Ok(this.fundRepository.GetAllFunds());
        }

        [HttpGet("{id}/GetFund")]
        public ActionResult<int> GetFundById(int id) 
        {
            return id;
        }

        [HttpGet("{id}/Tokens/GetCurrentTokens")]
        public async Task<ActionResult<TokenList>> GetCurrentFundTokensById(int id)
        {
            return await this.fundRepository.GetFundAndTokensAsync(id);
        }

        [HttpGet("{id}/Performance/GetCurrentFundPerformance")]
        public ActionResult<FundPerformance> GetCurrentFundPerformance(int id)
        {
            return new FundPerformance();
        }

        [HttpPost("Create")]
        public async Task<int> CreateFundAsync([FromBody] FundCreateRequest fundCreateRequest)
        {
            return await this.fundRepository.CreateFundAsync(fundCreateRequest);
        }

        [HttpPost("{id}/Tokens/Create")]
        public async Task<int> AddTokenToFund(int id, [FromBody] FundTokenCreateRequest fundTokenCreateRequest)
        {
            return await this.fundRepository.CreateFundToken(id, fundTokenCreateRequest);
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