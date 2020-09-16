using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController
    {
        [HttpGet("{currencyPair}")]
        public ActionResult<IEnumerable<TokenPriceHistory>> GetHistoricPriceData(string currencyPair)
        {
            var result = new List<TokenPriceHistory>();
            if (currencyPair == "eth-usd")
            {
                result.Add(new TokenPriceHistory { ID = 1, Timestamp = 1600194556, USD_Price = 356, EUR_Price = 310 }) ;
                result.Add(new TokenPriceHistory { ID = 2, Timestamp = 1600194546, USD_Price = 356.3, EUR_Price = 310.3 });
                result.Add(new TokenPriceHistory { ID = 3, Timestamp = 1600194536, USD_Price = 356.8, EUR_Price = 310.8 });
            }

            return result;
        }
    }
}