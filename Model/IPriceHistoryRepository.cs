using System.Collections.Generic;

namespace GalaxisProjectWebAPI.Model
{
    public interface IPriceHistoryRepository
    {
        List<PriceHistoricData> GetAllHistoricPriceDataAsync(string baseTokenSymbol);

        void AddTokenPriceHistoryDatasAsync(List<PriceHistoricData> priceHistoryDatas);
    }
}