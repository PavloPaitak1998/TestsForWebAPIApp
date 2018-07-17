using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> :IDisposable where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteAll();

    }
}
