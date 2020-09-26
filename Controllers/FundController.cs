using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.Model;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;
using GalaxisProjectWebAPI.Model.Token;

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
        public async Task<ActionResult<IEnumerable<DataModelFund>>> GetAllFundsAsync()
        {
            return Ok(await this.fundRepository.GetAllFundsAsync());
        }

        [HttpGet("{id}/GetFund")]
        public async Task<ActionResult<DataModelFund>> GetFundById(int id) 
        {
            return await this.fundRepository.GetFundByIdAsync(id);
        }

        [HttpGet("{id}/Tokens/GetCurrentTokens")]
        public async Task<ActionResult<TokenList<FundTokenInfo>>> GetCurrentFundTokensById(int id)
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

        [HttpPost("{id}/Tokens/AddTokenAllocationSnapshot")]
        public async Task<int> AddTokenToFund(int id, [FromBody] FundTokenCreateRequest fundTokenCreateRequest)
        {
            return await this.fundRepository.CreateFundTokensAsync(id, fundTokenCreateRequest);
        }
    }
}