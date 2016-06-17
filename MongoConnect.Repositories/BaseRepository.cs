using MongoConnect.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public abstract class BaseRepository<TEntity> : Repository<TEntity> where TEntity : Entity
    {
        protected BaseRepository(Context context, string collectionName)
        {
            Collection = ((BaseContext)context).DatabaseSession.Database.GetCollection<TEntity>(collectionName);
        }

        public TEntity Find(Identity id)
        {
            return Collection.Find(Filter.Eq<ObjectId>("_id", ((ObjectIdentity)id).IdentityValue)).FirstOrDefault();
        }
        protected TEntity Find(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> FindAll()
        {
            return FindAll(Filter.Empty);
        }
        public IEnumerable<TEntity> FindAll(int pageIndex, int pageSize)
        {
            return FindAll(Filter.Empty, pageIndex, pageSize);
        }
        protected IEnumerable<TEntity> FindAll(SortDefinition<TEntity> order, int pageIndex, int pageSize)
        {
            return FindAll(Filter.Empty, order, pageIndex, pageSize);
        }
        protected IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(filter).ToEnumerable();
        }
        protected IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, int pageIndex, int pageSize)
        {
            return Collection.Find(filter).Skip(GetSkip(pageIndex, pageSize)).Limit(pageSize).ToEnumerable();
        }
        protected IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, SortDefinition<TEntity> order, int pageIndex, int pageSize)
        {
            return Collection.Find(filter).Skip(GetSkip(pageIndex, pageSize)).Limit(pageSize).Sort(order).ToEnumerable();
        }

        public bool Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return !entity.Id.IsNull;
        }

        public bool Update(TEntity entity)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)entity.Id).IdentityValue)));
            return Collection.ReplaceOne(matchById, entity).IsAcknowledged;
        }

        public void Delete(Identity id)
        {
            Delete(Filter.Eq<ObjectId>("_id", ((ObjectIdentity)id).IdentityValue));
        }
        protected void Delete(FilterDefinition<TEntity> filter)
        {
            Collection.DeleteMany(filter);
        }

        public void Empty()
        {
            Delete(Filter.Empty);
        }

        protected bool Exists(FilterDefinition<TEntity> filter)
        {
            return Count(filter) > 0;
        }

        public long Count()
        {
            return Count(Filter.Empty);
        }
        protected long Count(FilterDefinition<TEntity> filter)
        {
            return Collection.Count(filter);
        }

        protected int GetSkip(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }

        protected IMongoCollection<TEntity> Collection;
        protected FilterDefinitionBuilder<TEntity> Filter
        {
            get
            {
                return Builders<TEntity>.Filter;
            }
        }
    }
}
