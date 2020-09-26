namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{
    public interface IDummyDataStorage
    {
        void SaveDummyDatas<TDummyData>(IDummyDataFactory<TDummyData> factory)
            where TDummyData : class, IData;
    }
}