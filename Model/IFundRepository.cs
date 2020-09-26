using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.Model.Token;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;

namespace GalaxisProjectWebAPI.Model
{
    public interface IFundRepository
    {
        Task<IEnumerable<DataModelFund>> GetAllFundsAsync();

        Task<ActionResult<DataModelFund>> GetFundByIdAsync(int fundId);

        Task<ActionResult<TokenList<FundTokenInfo>>> GetFundAndTokensAsync(int fundId);

        Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest);

        Task<int> CreateFundTokensAsync(int id, FundTokenCreateRequest fundTokenCreateRequest);
    }
}