using MongoConnect.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public interface EntityListRepository<TEntity> : EntityRepository<TEntity> where TEntity : Entity
    {
        PagedList<TEntity> ReadMany();
        PagedList<TEntity> ReadMany(int pageIndex, int pageSize);
        PagedList<TEntity> ReadMany(MongoQuery query);
        PagedList<TEntity> ReadMany(MongoQuery query, int pageIndex, int pageSize);
        bool Delete();
    }
}
