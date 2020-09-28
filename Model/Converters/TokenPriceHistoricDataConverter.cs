using Newtonsoft.Json;

using System.IO;
using System.Collections.Generic;

namespace GalaxisProjectWebAPI.Model.Converters
{
    public class TokenPriceHistoricDataConverter
    {
        public List<PriceHistoricData> Convert(string jsonFilePath)
        {
            var json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<PriceHistoricData>>(json);
        }
    }
}