using MongoConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public interface EntityRepository<TEntity> where TEntity : Entity
    {
        bool Create(TEntity entity);
        TEntity Read(Identity id);
        bool Update(TEntity entity);
        bool Delete(Identity id);
    }
}
