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
        public async Task<ActionResult<IEnumerable<PriceHistoricData>>> GetHistoricPriceData(string baseTokenSymbol)
        {
            return await this.priceHistoryRepository
                .GetAllHistoricPriceDataAsync(baseTokenSymbol);
        }

        [HttpPost("PushHistoricPriceDatas")]
        public async Task PushHistoricPriceDatas(PriceHistoricDataCreateRequest priceHistoricDataCreateRequest)
        {
            await this.priceHistoryRepository
                .AddTokenPriceHistoryDatasAsync(priceHistoricDataCreateRequest.priceHistoricDatas);
        }
    }
}