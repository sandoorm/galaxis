using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.Model;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController
    {
        private readonly IPriceHistoryRepository priceHistoryRepository;

        public PriceHistoryController(IPriceHistoryRepository priceHistoryRepository)
        {
            this.priceHistoryRepository = priceHistoryRepository;
        }

        [HttpGet("GetHistoricPriceData")]
        public ActionResult<IEnumerable<PriceHistoricData>> GetHistoricPriceData(string baseTokenSymbol)
        {
            return this.priceHistoryRepository
                .GetAllHistoricPriceDataAsync(baseTokenSymbol);
        }

        [HttpPost("PushHistoricPriceDatas")]
        public void PushHistoricPriceDatas(PriceHistoricDataCreateRequest priceHistoricDataCreateRequest)
        {
            this.priceHistoryRepository
                .AddTokenPriceHistoryDatasAsync(priceHistoricDataCreateRequest.priceHistoricDatas);
        }

        [HttpDelete("DeletePriceHistory")]
        public async Task<int> DeletePriceHistoricDatas(string baseTokenSymbol, uint timeStamp)
        {
            return await this.priceHistoryRepository.DeleteTokenPriceHistoryAsync(baseTokenSymbol, timeStamp);
        }
    }
}