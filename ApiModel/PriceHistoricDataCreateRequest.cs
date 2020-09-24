using System.Collections.Generic;
using GalaxisProjectWebAPI.Model;

namespace GalaxisProjectWebAPI.ApiModel
{
    public class PriceHistoricDataCreateRequest
    {
        public List<PriceHistoricData> priceHistoricDatas { get; set; }
    }
}