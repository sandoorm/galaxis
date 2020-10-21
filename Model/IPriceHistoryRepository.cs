using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalaxisProjectWebAPI.Model
{
    public interface IPriceHistoryRepository
    {
        List<PriceHistoricData> GetAllHistoricPriceDataAsync(string baseTokenSymbol);

        void AddTokenPriceHistoryDatasAsync(List<PriceHistoricData> priceHistoryDatas);

        Task<int> DeleteTokenPriceHistoryAsync(string baseTokenSymbol, uint timeStamp);
    }
}