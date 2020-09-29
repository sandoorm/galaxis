using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.Model;

using GalaxisProjectWebAPI.Model.Token;
using GalaxisProjectWebAPI.Model.FundPerformanceCalculation;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundRepository fundRepository;
        private readonly IFundPerformanceCalculator fundPerformanceCalculator;

        public FundController(IFundRepository fundRepository, IFundPerformanceCalculator fundPerformanceCalculator)
        {
            this.fundRepository = fundRepository;
            this.fundPerformanceCalculator = fundPerformanceCalculator;
        }

        [HttpGet("GetAllFunds")]
        public async Task<ActionResult<IEnumerable<DataModelFund>>> GetAllFundsAsync()
        {
            return Ok(await this.fundRepository.GetAllFundsAsync());
        }

        [HttpGet("{fundAddress}/GetFund")]
        public async Task<ActionResult<DataModelFund>> GetFundByAddress(string fundAddress) 
        {
            return await this.fundRepository.GetFundByAddressAsync(fundAddress);
        }

        [HttpGet("{fundAddress}/Tokens/GetCurrentTokenAllocation")]
        public async Task<ActionResult<TokenList<FundTokenInfo>>> GetCurrentTokenAllocation(string fundAddress)
        {
            return await this.fundRepository.GetFundAndTokensAsync(fundAddress);
        }

        [HttpGet("{fundAddress}/Performance/GetFundPerformance")]
        public async Task<ActionResult<FundPerformance>> GetCurrentFundPerformance(string fundAddress)
        {
            return await this.fundPerformanceCalculator.CalculateFundPerformance(fundAddress);
        }

        [HttpPost("Create")]
        public async Task<int> CreateFundAsync([FromBody] FundCreateRequest fundCreateRequest)
        {
            return await this.fundRepository.CreateFundAsync(fundCreateRequest);
        }

        [HttpPut("{fundAddress}/Modify")]
        public async Task<int> ModifyFund(string fundAddress, [FromBody] FundCreateRequest fundCreateRequest)
        {
            return await this.fundRepository.ModifyFundAsync(fundAddress, fundCreateRequest);
        }

        [HttpDelete("{fundAddress}")]
        public async Task<int> DeleteFund(string fundAddress)
        {
            return await this.fundRepository.DeleteFundAsync(fundAddress);
        }   

        [HttpPost("{fundAddress}/Launch")]
        public async Task<int> LaunchFund(string fundAddress)
        {
            return await this.fundRepository.LaunchFundAsync(fundAddress);
        }

        [HttpPost("{fundAddress}/Tokens/AddTokenAllocationSnapshot")]
        public async Task<int> AddTokenToFund(string fundAddress, [FromBody] FundTokenCreateRequest fundTokenCreateRequest)
        {
            return await this.fundRepository.CreateFundTokensAsync(fundAddress, fundTokenCreateRequest);
        } 
    }
}