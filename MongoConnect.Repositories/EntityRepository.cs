using MongoConnect.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public abstract class EntityRepository<TEntity> where TEntity : Entity
    {
        protected EntityRepository(IMongoDatabase database, string collectionName)
        {
            _Collection = database.GetCollection<TEntity>(collectionName);
        }

        public bool Create(TEntity entity)
        {
            _Collection.InsertOne(entity);
            return entity.Id.IsNull;
        }

        public TEntity Read(Identity id)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return _Collection.Find(matchById).FirstOrDefault();
        }

        public bool Update(TEntity entity)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)entity.Id).IdentityValue)));
            ReplaceOneResult result = _Collection.ReplaceOne(matchById, entity);
            return result.ModifiedCount == 1;
        }

        public bool Delete(Identity id)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            DeleteResult result = _Collection.DeleteOne(matchById);
            return result.DeletedCount == 1;
        }

        public List<TEntity> ReadMany()
        {
            return _Collection.Find(Builders<TEntity>.Filter.Empty).ToList();
        }
        public List<TEntity> ReadMany(int pageIndex, int pageSize)
        {
            return _Collection.Find(Builders<TEntity>.Filter.Empty).Skip(GetOffset(pageIndex, pageSize)).Limit(pageSize).ToList();
        }
        //public List<TEntity> ReadMany(MongoQuery query)
        //{
        //    throw new NotImplementedException();
        //}
        //public List<TEntity> ReadMany(MongoQuery query, int pageIndex, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}
        //public bool DeleteMany()
        //{
        //    throw new NotImplementedException();
        //}


        //public PagedList<T> ReadMany(int pageIndex, int pageSize)
        //{
        //    return new PagedList<T>()
        //    {
        //        Lists = _Collection.FindAll(GetOffset(pageIndex, pageSize), pageSize).ToList(),
        //        Total = (int)_Collection.Count()
        //    };
        //}

        //public PagedList<T> ReadMany(MongoQuery query)
        //{
        //    return new PagedList<T>()
        //    {
        //        Lists = _Collection.FindAll(query).ToList(),
        //        Total = (int)_Collection.Count()
        //    };
        //}

        //public PagedList<T> ReadMany(MongoQuery query, int pageIndex, int pageSize)
        //{
        //    return new PagedList<T>()
        //    {
        //        Lists = _Collection.FindAll(query, pageIndex, pageSize).ToList(),
        //        Total = (int)_Collection.Count()
        //    };
        //}

        //public bool Delete()
        //{
        //    return _Collection.RemoveAll();
        //}

        protected int GetOffset(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }

        protected IMongoCollection<TEntity> _Collection;
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
