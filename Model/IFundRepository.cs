using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;

using DataModelFund = GalaxisProjectWebAPI.DataModel.Fund;

namespace GalaxisProjectWebAPI.Model
{
    public interface IFundRepository
    {
        Task<IEnumerable<DataModelFund>> GetAllFundsAsync();

        Task<ActionResult<DataModelFund>> GetFundByIdAsync(int fundId);

        Task<ActionResult<TokenList>> GetFundAndTokensAsync(int fundId);

        Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest);

        Task<int> CreateFundTokenAsync(int id, FundTokenCreateRequest fundTokenCreateRequest);
    }
}