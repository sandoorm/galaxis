using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

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

        [HttpGet("{currencyPair}")]
        public ActionResult<IEnumerable<PriceHistoricData>> GetHistoricPriceData(string currencyPair)
        {
            var result = new List<PriceHistoricData>();
            if (currencyPair == "eth-usd")
            {
                result.Add(new PriceHistoricData { Timestamp = 1600194556, BaseCurrency = "ETH", QuoteCurrency = "USD", Price = 330 });
                result.Add(new PriceHistoricData { Timestamp = 1600194576, BaseCurrency = "ETH", QuoteCurrency = "USD", Price = 332.6 });
                result.Add(new PriceHistoricData { Timestamp = 1600194596, BaseCurrency = "ETH", QuoteCurrency = "USD", Price = 334.67 });
            }

            return result;
        }

        //[HttpPost]
        //public ActionResult PushHistoricPriceData(PriceHistoricDataCreateRequest priceHistoricDataCreateRequest)
        //{

        //}
    }
}