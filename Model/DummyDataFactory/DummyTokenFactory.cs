using System.Collections.Generic;

using DataModelToken = GalaxisProjectWebAPI.DataModel.Token;

namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{
    public class DummyTokenFactory : IDummyDataFactory<DataModelToken>
    {
        public List<DataModelToken> CreateDummyDatas()
        {
            return new List<DataModelToken>
            {
                new DataModelToken
                {
                    Symbol = "ETH",
                    Name = "Ether",
                    Decimals = 18
                },
                new DataModelToken
                {
                    Symbol = "BTC",
                    Name = "Bitcoin",
                    Decimals = 18
                },
            };
        }
    }
}