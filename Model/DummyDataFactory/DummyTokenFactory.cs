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
                //new DataModelToken
                //{
                //    Symbol = "ETH",
                //    Name = "Ether",
                //    Decimals = 18
                //},
                //new DataModelToken
                //{
                //    Symbol = "BTC",
                //    Name = "Bitcoin",
                //    Decimals = 18
                //},
                new DataModelToken
                {
                    Symbol = "DAI",
                    Name = "Dai",
                    Decimals = 18
                },
                new DataModelToken
                {
                    Symbol = "CDAI",
                    Name = "cDAI",
                    Decimals = 18
                },
                new DataModelToken
                {
                    Symbol = "WETH",
                    Name = "WETH",
                    Decimals = 18
                }
            };
        }
    }
}