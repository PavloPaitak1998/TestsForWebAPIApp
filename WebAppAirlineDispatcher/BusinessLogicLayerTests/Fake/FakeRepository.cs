using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayerTests.Fake
{
    public class FakeRepository<TEntity> : IRepository<TEntity>where TEntity:class, IEntity
    {
        public readonly List<TEntity> Data;

        public FakeRepository(params TEntity[] data)
        {
            Data = new List<TEntity>(data);
        }

        public void Add(TEntity entity)
        {
            Data.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            Data.Remove(entity);
        }

        public void DeleteAll()
        {
            Data.RemoveRange(0, Data.Count());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            return Data.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Data;
        }

        public void Update(TEntity _entity)
        {
            TEntity entity = Data.FirstOrDefault(x => x.Id == _entity.Id);
            entity = _entity;
        }
    }
}
