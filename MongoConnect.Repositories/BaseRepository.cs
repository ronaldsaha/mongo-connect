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
            Collection = ((MongoDBContext)context).DatabaseSession.Database.GetCollection<TEntity>(collectionName);
        }

        public TEntity Get(Identity id)
        {
            return Collection.Find(Filter.Eq<ObjectId>("_id", ((ObjectIdentity)id).IdentityValue)).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return FindAll(Filter.Empty);
        }
        public IEnumerable<TEntity> GetAll(int pageIndex, int pageSize)
        {
            return FindAll(Filter.Empty, pageIndex, pageSize);
        }
        public IEnumerable<TEntity> GetAll(SortDefinition<TEntity> order, int pageIndex, int pageSize)
        {
            return FindAll(Filter.Empty, order, pageIndex, pageSize);
        }

        public IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(filter).ToEnumerable();
        }
        public IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, int pageIndex, int pageSize)
        {
            return Collection.Find(filter).Skip(GetSkip(pageIndex, pageSize)).Limit(pageSize).ToEnumerable();
        }
        public IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, SortDefinition<TEntity> order, int pageIndex, int pageSize)
        {
            return Collection.Find(filter).Skip(GetSkip(pageIndex, pageSize)).Limit(pageSize).Sort(order).ToEnumerable();
        }

        public void Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
        }

        public void Update(TEntity entity)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)entity.Id).IdentityValue)));
            Collection.ReplaceOne(matchById, entity);
        }

        public void Delete(Identity id)
        {
            Delete(Filter.Eq<ObjectId>("_id", ((ObjectIdentity)id).IdentityValue));
        }
        public void Delete(FilterDefinition<TEntity> filter)
        {
            Collection.DeleteMany(filter);
        }

        public void Empty()
        {
            Delete(Filter.Empty);
        }

        public bool Exists(FilterDefinition<TEntity> filter)
        {
            return Count(filter) > 0;
        }

        public long Count()
        {
            return Count(Filter.Empty);
        }
        public long Count(FilterDefinition<TEntity> filter)
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
