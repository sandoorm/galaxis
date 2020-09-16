using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalaxisProjectWebAPI.Model
{
    public interface IFundRepository
    {
        IEnumerable<Fund> GetAllFundsAsync();

        Task<int> CreateFundAsync();
    }
}