using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;

namespace GalaxisProjectWebAPI.Model.DummyDataFactory
{
    public class DummyDataStorage : IDummyDataStorage
    {
        private readonly DbContext galaxisContext;

        public DummyDataStorage(DbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public void SaveDummyDatas<TDummyData>(IDummyDataFactory<TDummyData> factory)
            where TDummyData : class, IData 
        {
            List<TDummyData> dummyDatas = factory.CreateDummyDatas();
            int id = dummyDatas.First().Id;

            var dbSet = this.galaxisContext.Set<TDummyData>();
            if (dbSet.Find(id) == null)
            {
                dbSet.AddRange(dummyDatas);
                this.galaxisContext.SaveChanges();
            }
        }
    }
}