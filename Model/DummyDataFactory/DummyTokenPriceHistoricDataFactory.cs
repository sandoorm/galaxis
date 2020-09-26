using System.Collections.Generic;

using DataModelTokenPriceHistoricData = GalaxisProjectWebAPI.DataModel.TokenPriceHistoricData;

namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{
    public class DummyTokenPriceHistoricDataFactory : IDummyDataFactory<DataModelTokenPriceHistoricData>
    {
        public DummyTokenPriceHistoricDataFactory()
        {
        }

        public List<DataModelTokenPriceHistoricData> CreateDummyDatas()
        {
            return new List<DataModelTokenPriceHistoricData>
            {
                new DataModelTokenPriceHistoricData
                {
                    TokenId = 1,
                    Timestamp = 1601113599,
                    UsdPrice = 345
                },
                new DataModelTokenPriceHistoricData
                {
                    TokenId = 1,
                    Timestamp = 1601113589,
                    UsdPrice = 342
                },
                new DataModelTokenPriceHistoricData
                {
                    TokenId = 1,
                    Timestamp = 1601113579,
                    UsdPrice = 340
                },
                new DataModelTokenPriceHistoricData
                {
                    TokenId = 2,
                    Timestamp = 1601113559,
                    UsdPrice = 10550
                }
            };
        }
    }
}