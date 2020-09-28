using System.Collections.Generic;
using System.IO;

using GalaxisProjectWebAPI.Model.Converters;

using DataModelTokenPriceHistoricData = GalaxisProjectWebAPI.DataModel.TokenPriceHistoricData;

namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{
    public class DummyTokenPriceHistoricDataFactory : IDummyDataFactory<DataModelTokenPriceHistoricData>
    {
        private readonly TokenPriceHistoricDataConverter converter;

        public DummyTokenPriceHistoricDataFactory(TokenPriceHistoricDataConverter converter)
        {
            this.converter = converter;
        }

        public List<DataModelTokenPriceHistoricData> CreateDummyDatas()
        {
            var converter = new TokenPriceHistoricDataConverter();
            var finalResult = new List<DataModelTokenPriceHistoricData>();

            var ethHistoricFilePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "PriceHistories", "ETHPriceHistoricData.json");
            List<PriceHistoricData> ethResult = converter.Convert(ethHistoricFilePath);

            var wethHistoricFilePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "PriceHistories", "WETHPriceHistoricData.json");
            List<PriceHistoricData> wethResult = converter.Convert(wethHistoricFilePath);

            var daiHistoricFilePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "PriceHistories", "DAIPriceHistoricData.json");
            List<PriceHistoricData> daiResult = converter.Convert(daiHistoricFilePath);

            finalResult.AddRange(Convert(1, ethResult));
            finalResult.AddRange(Convert(2, ethResult));
            finalResult.AddRange(Convert(3, ethResult));

            return finalResult;
        }

        private List<DataModelTokenPriceHistoricData> Convert(int tokenId, List<PriceHistoricData> wethResult)
        {
            var result = new List<DataModelTokenPriceHistoricData>();
            foreach (var item in wethResult)
            {
                result.Add(new DataModelTokenPriceHistoricData
                {
                    TokenId = tokenId,
                    Timestamp = item.Timestamp,
                    UsdPrice = item.USDPrice,
                    EurPrice = item.EURPrice
                });
            }

            return result;
        }
    }
}