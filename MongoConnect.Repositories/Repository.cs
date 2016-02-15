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
        TEntity Get(Identity id);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>> order, bool isAscending);
        IEnumerable<TEntity> GetAll(int pageIndex, int pageSize);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize);

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        //void Update(IEnumerable<TEntity> entities);

        void FindAndUpdate<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value);
        void FindAndUpdate(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);

        void Delete(Identity id);
        void Delete(Expression<Func<TEntity, bool>> filter);

        void Empty();
        bool Exists(Expression<Func<TEntity, bool>> filter);
        long Count();
        long Count(Expression<Func<TEntity, bool>> filter);
    }
}
