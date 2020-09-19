using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;

namespace GalaxisProjectWebAPI.Model
{
    public interface IFundRepository
    {
        IEnumerable<Fund> GetAllFundsAsync();

        Task<int> CreateFundAsync(FundCreateRequest fundCreateRequest);
    }
}