using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalaxisProjectWebAPI.Model
{
    public interface IPriceHistoryRepository
    {
        Task<List<PriceHistoricData>> GetAllHistoricPriceDataAsync(string baseTokenSymbol);
    }
}