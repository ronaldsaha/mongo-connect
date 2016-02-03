using MongoConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public abstract class MongoEntityListRepository<T> : MongoEntityRepository<T>, EntityListRepository<T> where T : Entity
    {
        public MongoEntityListRepository(MongoEntityCollection<T> collection)
            : base(collection)
        {
        }

        public PagedList<T> ReadMany()
        {
            return new PagedList<T>()
            {
                Lists = _Collection.FindAll().ToList(),
                Total = (int)_Collection.Count()
            };
        }

        public PagedList<T> ReadMany(int pageIndex, int pageSize)
        {
            return new PagedList<T>()
            {
                Lists = _Collection.FindAll(GetOffset(pageIndex, pageSize), pageSize).ToList(),
                Total = (int)_Collection.Count()
            };
        }

        public PagedList<T> ReadMany(MongoQuery query)
        {
            return new PagedList<T>()
            {
                Lists = _Collection.FindAll(query).ToList(),
                Total = (int)_Collection.Count()
            };
        }

        public PagedList<T> ReadMany(MongoQuery query, int pageIndex, int pageSize)
        {
            return new PagedList<T>()
            {
                Lists = _Collection.FindAll(query, pageIndex, pageSize).ToList(),
                Total = (int)_Collection.Count()
            };
        }

        public bool Delete()
        {
            return _Collection.RemoveAll();
        }

        protected int GetOffset(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }
    }
}
