using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

using GalaxisProjectWebAPI.Model;


namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController
    {
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CandlestickPriceData>> GetHistoricPriceData(string id)
        {
            var result = new List<CandlestickPriceData>();
            if (id == "eth-dai")
            {
                result.Add(new CandlestickPriceData { TimeStamp = "34543434343", Price = 378, Open = 379, High = 380, Low = 378.6 }) ;
                result.Add(new CandlestickPriceData { TimeStamp = "45674343445", Price = 378.2, Open = 379.5, High = 381, Low = 377.9 });
                result.Add(new CandlestickPriceData { TimeStamp = "34565434577", Price = 378.5, Open = 379.7, High = 381.2, Low = 377.6 });
            }

            return result;
        }
    }
}