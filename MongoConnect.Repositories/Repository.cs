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
        IEnumerable<TEntity> GetAll(int pageIndex, int pageSize);
        IEnumerable<TEntity> GetAll(SortDefinition<TEntity> order, int pageIndex, int pageSize);

        IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter);
        IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, int pageIndex, int pageSize);
        IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, SortDefinition<TEntity> order, int pageIndex, int pageSize);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(Identity id);
        void Delete(FilterDefinition<TEntity> filter);

        void Empty();

        bool Exists(FilterDefinition<TEntity> filter);

        long Count();
        long Count(FilterDefinition<TEntity> filter);
    }
}
