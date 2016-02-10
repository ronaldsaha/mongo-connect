using MongoConnect.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public interface Repository<TEntity> where TEntity : Entity
    {
        TEntity Find(Identity id);
        IEnumerable<TEntity> Find();
        IEnumerable<TEntity> Find(int pageIndex, int pageSize);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int pageIndex, int size);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);

        bool Replace(TEntity entity);
        //void Replace(IEnumerable<TEntity> entities);

        bool Update(TEntity entity, UpdateDefinition<TEntity> update);
        bool Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        bool Update<TField>(TEntity entity, Expression<Func<TEntity, TField>> field, TField value);
        bool Update<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value);

        bool Delete(Identity id);
        bool Delete(TEntity entity);
        bool Delete(Expression<Func<TEntity, bool>> filter);

        bool Any(Expression<Func<TEntity, bool>> filter);
        long Count();
        long Count(Expression<Func<TEntity, bool>> filter);
    }
}
