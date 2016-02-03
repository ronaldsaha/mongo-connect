using MongoConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public abstract class MongoEntityDecoratedRepository<T> : EntityRepository<T> where T : Entity
    {
        protected EntityRepository<T> _DecoratedOn;
        public MongoEntityDecoratedRepository(EntityRepository<T> repository)
        {
            _DecoratedOn = repository;
        }

        public bool Create(T entity)
        {
            return _DecoratedOn.Create(entity);
        }

        public T Read(Identity id)
        {
            return _DecoratedOn.Read(id);
        }

        public bool Update(T entity)
        {
            return _DecoratedOn.Update(entity);
        }

        public bool Delete(Identity id)
        {
            return _DecoratedOn.Delete(id);
        }
    }
}
