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
        TEntity FindOne(Identity id);

        IEnumerable<TEntity> FindAll();
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, bool isAscending);
        IEnumerable<TEntity> FindAll(int pageIndex, int pageSize);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize);

        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize);

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);

        bool Update(TEntity entity, UpdateDefinition<TEntity> update);
        bool Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        bool Update<TField>(TEntity entity, Expression<Func<TEntity, TField>> field, TField value);
        bool Update<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value);

        void Replace(TEntity entity);
        void Replace(IEnumerable<TEntity> entities);

        void Delete(string id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> filter);

        bool Exists(Expression<Func<TEntity, bool>> filter);
        long Count();
        long Count(Expression<Func<TEntity, bool>> filter);
    }
}
