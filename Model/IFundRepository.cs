using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using Microsoft.AspNetCore.Mvc;

namespace GalaxisProjectWebAPI.Model
{
    public interface IFundRepository
    {
        IEnumerable<Fund> GetAllFunds();

        Task<ActionResult<List<Token>>> GetFundAndTokensAsync(int fundId);

        Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest);

        Task<int> CreateFundToken(int id, FundTokenCreateRequest fundTokenCreateRequest);
    }
}