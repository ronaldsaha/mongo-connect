using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public interface Repository<TEntity> where TEntity : Entity
    {
        TEntity Find(Identity id);
        IEnumerable<TEntity> FindAll();
        IEnumerable<TEntity> FindAll(IEnumerable<Identity> ids);
        IEnumerable<TEntity> FindAll(int pageIndex, int pageSize);
        //IEnumerable<TEntity> FindAll(SortOrder order);
        //IEnumerable<TEntity> FindAll(SortOrder order, int pageIndex, int pageSize);
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        void Delete(Identity id);
        void Empty();
        long Count();

        //TEntity Find(FilterDefinition<TEntity> filter);
        //IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter);
        //IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, int pageIndex, int pageSize);
        //IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, SortDefinition<TEntity> order, int pageIndex, int pageSize);
        //void Delete(FilterDefinition<TEntity> filter);
        //bool Exists(FilterDefinition<TEntity> filter);
        //long Count(FilterDefinition<TEntity> filter);
    }
}
