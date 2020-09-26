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

        Task<ActionResult<DataModelFund>> GetFundByAddressAsync(string fundAddress);

        Task<ActionResult<TokenList<FundTokenInfo>>> GetFundAndTokensAsync(string fundAddress);

        Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest);

        Task<int> CreateFundTokensAsync(string fundAddress, FundTokenCreateRequest fundTokenCreateRequest);

        Task<int> LaunchFundAsync(string fundAddress);
    }
}