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
        protected BaseRepository(Context context, MongoSession session, string collectionName)
        {
            Collection = ((MongoSession)session).Database.GetCollection<TEntity>(collectionName);
        }

        public virtual TEntity Find(Identity id)
        {
            return Collection.Find(Filter.Eq<ObjectId>("_id", ((ObjectIdentity)id).IdentityValue)).FirstOrDefault();
        }
        protected virtual TEntity Find(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return FindAll(Filter.Empty);
        }
        public virtual IEnumerable<TEntity> FindAll(IEnumerable<Identity> ids)
        {            
            return FindAll(Filter.In<ObjectId>("_id", ConvertToObjectId(ids)));
        }
        public virtual IEnumerable<TEntity> FindAll(int pageIndex, int pageSize)
        {
            return FindAll(Filter.Empty, pageIndex, pageSize);
        }

        protected virtual IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(filter).ToEnumerable();
        }
        protected virtual IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, int pageIndex, int pageSize)
        {
            return Collection.Find(filter).Skip(GetSkip(pageIndex, pageSize)).Limit(pageSize).ToEnumerable();
        }

        public virtual IEnumerable<TEntity> FindAll(SortOrder order)
        {
            return FindAll(Filter.Empty, GetSortDefinition(order));
        }
        public virtual IEnumerable<TEntity> FindAll(SortOrder order, int pageIndex, int pageSize)
        {
            return FindAll(Filter.Empty, GetSortDefinition(order), pageIndex, pageSize);
        }

        protected virtual IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, SortDefinition<TEntity> order)
        {
            return Collection.Find(filter).Sort(order).ToEnumerable();
        }
        protected virtual IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter, SortDefinition<TEntity> order, int pageIndex, int pageSize)
        {
            var findFluent = Collection.Find(filter).Sort(order);
            if (pageIndex > 0 && pageSize > 0)
                findFluent = findFluent.Skip(GetSkip(pageIndex, pageSize)).Limit(pageSize);
            return findFluent.ToEnumerable();
        }

        public virtual bool Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return !entity.Id.IsNull;
        }

        public virtual bool Update(TEntity entity)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)entity.Id).IdentityValue)));
            return Collection.ReplaceOne(matchById, entity).IsAcknowledged;
        }
        protected virtual bool Update(Identity id, UpdateDefinition<TEntity> updateValue)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return Collection.UpdateOne(matchById, updateValue).IsAcknowledged;
        }

        public virtual void Delete(Identity id)
        {
            Delete(Filter.Eq<ObjectId>("_id", ((ObjectIdentity)id).IdentityValue));
        }
        protected virtual void Delete(FilterDefinition<TEntity> filter)
        {
            Collection.DeleteMany(filter);
        }

        public virtual void Empty()
        {
            Delete(Filter.Empty);
        }

        protected virtual bool Exists(FilterDefinition<TEntity> filter)
        {
            return Count(filter) > 0;
        }

        public virtual long Count()
        {
            return Count(Filter.Empty);
        }
        protected virtual long Count(FilterDefinition<TEntity> filter)
        {
            return Collection.Count(filter);
        }

        protected virtual int GetSkip(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }

        protected virtual SortDefinition<TEntity> GetSortDefinition(SortOrder sortOrder)
        {
            if (sortOrder.Direction == Models.SortDirection.Ascending)
                return Sort.Ascending(sortOrder.FieldName.Name);
            else
                return Sort.Descending(sortOrder.FieldName.Name);
        }

        protected virtual IEnumerable<ObjectId> ConvertToObjectId(IEnumerable<Identity> ids)
        {
            return ids.Select(i => ConvertToObjectId(i)).ToList();
        }

        protected virtual ObjectId ConvertToObjectId(Identity id)
        {
            return ((ObjectIdentity)id).IdentityValue;
        }

        protected IMongoCollection<TEntity> Collection;
        protected FilterDefinitionBuilder<TEntity> Filter
        {
            get
            {
                return Builders<TEntity>.Filter;
            }
        }
        protected SortDefinitionBuilder<TEntity> Sort
        {
            get
            {
                return Builders<TEntity>.Sort;
            }
        }
        protected UpdateDefinitionBuilder<TEntity> UpdateDefinition
        {
            get
            {
                return Builders<TEntity>.Update;
            }
        }

    }
}
