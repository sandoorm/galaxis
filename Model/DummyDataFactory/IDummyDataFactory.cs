using System.Collections.Generic;

namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{
    public interface IDummyDataFactory<TDummyData> where TDummyData : class, IData
    {
        List<TDummyData> CreateDummyDatas();
    }
}