using System;
using System.Net;

using Newtonsoft.Json;

namespace GalaxisProjectWebAPI.Model.TokenPriceHistoryProvider
{
    public class TokenPriceHistoryProvider : ITokenPriceHistoryProvider
    {
        private static string API_KEY = "8150719f-df72-44a2-824d-56585174f3cc";

        public double GetCurrentPrice(string tokenSymbol)
        {
            var uriBuilder = new UriBuilder(
                "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol="
                + tokenSymbol);

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            string result = client.DownloadString(uriBuilder.ToString());

            var res = JsonConvert.DeserializeObject<PriceResultRoot>(result);
            return res.data.ETH.quote.USD.price;
        }
    }
}