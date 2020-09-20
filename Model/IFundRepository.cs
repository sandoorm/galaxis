using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;

namespace GalaxisProjectWebAPI.Model
{
    public interface IFundRepository
    {
        IEnumerable<Fund> GetAllFunds();

        Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest);

        Task<int> CreateFundToken(int id, FundTokenCreateRequest fundTokenCreateRequest);
    }
}