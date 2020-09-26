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

        [HttpGet("{fundAddress}/GetFund")]
        public async Task<ActionResult<DataModelFund>> GetFundById(string fundAddress) 
        {
            return await this.fundRepository.GetFundByAddressAsync(fundAddress);
        }

        [HttpGet("{fundAddress}/Tokens/GetCurrentTokens")]
        public async Task<ActionResult<TokenList<FundTokenInfo>>> GetCurrentFundTokensById(string fundAddress)
        {
            return await this.fundRepository.GetFundAndTokensAsync(fundAddress);
        }

        [HttpGet("{fundAddress}/Performance/GetCurrentFundPerformance")]
        public ActionResult<FundPerformance> GetCurrentFundPerformance(string fundAddress)
        {
            return new FundPerformance();
        }

        [HttpPost("Create")]
        public async Task<int> CreateFundAsync([FromBody] FundCreateRequest fundCreateRequest)
        {
            return await this.fundRepository.CreateFundAsync(fundCreateRequest);
        }

        [HttpPost("{fundAddress}/Tokens/AddTokenAllocationSnapshot")]
        public async Task<int> AddTokenToFund(string fundAddress, [FromBody] FundTokenCreateRequest fundTokenCreateRequest)
        {
            return await this.fundRepository.CreateFundTokensAsync(fundAddress, fundTokenCreateRequest);
        }
    }
}