using MongoConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public abstract class MongoEntityRepository<TEntity> : EntityRepository<TEntity> where TEntity : Entity
    {
        public MongoEntityRepository(MongoEntityCollection<TEntity> collection)
        {
            _Collection = collection;
        }

        public TEntity Read(Identity id)
        {
            //return _Collection.FindOne(MongoQuery.EQ("_id", ((MongoEF.Helpers.ObjectIdentity)id).IdentityValue));
            throw new NotImplementedException();
        }

        public bool Create(TEntity content)
        {
            return _Collection.Insert(content);
        }

        public bool Update(TEntity content)
        {
            return _Collection.Save(content);
        }

        public bool Delete(Identity id)
        {
            //return _Collection.Remove(MongoQuery.EQ("_id", ((MongoEF.Helpers.ObjectIdentity)id).IdentityValue));
            throw new NotImplementedException();
        }

        protected MongoEntityCollection<TEntity> _Collection;
    }



    //internal abstract class WorkspaceSimpleBaseRepository<T> : MongoEntityRepository<T>, EntityRepository<T> where T : WorkspaceEntity
    //{
    //    protected Identity _ClientId;
    //    public WorkspaceSimpleBaseRepository(MongoDatabase database, string collectionName, Context context)
    //        : base(database, collectionName)
    //    {
    //        _ClientId = context.WorkspaceId;
    //    }

    //    protected IMongoQuery GetClientQuery()
    //    {
    //        return Query.EQ("ClientId", ((MongoIdentity)_ClientId)._Value);
    //    }
    //}

    //internal abstract class WorkspaceListBaseRepository<T> : WorkspaceSimpleBaseRepository<T>, EntityListRepository<T> where T : WorkspaceEntity
    //{
    //    public WorkspaceListBaseRepository(MongoDatabase database, string collectionName, Context context)
    //        : base(database, collectionName, context)
    //    {
    //    }

    //    public PagedList<T> Read()
    //    {
    //        return new PagedList<T>()
    //        {
    //            Lists = _Collection.Find(GetClientQuery()).ToList(),
    //            Total = (int)_Collection.Count()
    //        };
    //    }

    //    public PagedList<T> Read(int pageIndex, int pageSize)
    //    {
    //        return new PagedList<T>()
    //        {
    //            Lists = _Collection.Find(GetClientQuery()).SetSkip(GetOffset(pageIndex, pageSize)).SetLimit(pageSize).ToList(),
    //            Total = (int)_Collection.Count()
    //        };
    //    }

    //    public bool Delete()
    //    {
    //        return _Collection.Remove(GetClientQuery()).Ok;
    //    }
    //}

}
