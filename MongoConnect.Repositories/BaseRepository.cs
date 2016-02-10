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
        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        public TEntity Find(Identity id)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return Collection.Find(matchById).FirstOrDefault();
        }
        public IEnumerable<TEntity> Find()
        {
            return Collection.Find(Filter.Empty).ToList();
        }
        public IEnumerable<TEntity> Find(int pageIndex, int pageSize)
        {
            return Collection.Find(Filter.Empty).Skip(GetOffset(pageIndex, pageSize)).Limit(pageSize).ToList();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return Query(filter).ToEnumerable();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int pageIndex, int size)
        {
            return Find(filter, i => i.Id, pageIndex, size);
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size)
        {
            return Find(filter, order, pageIndex, size, true);
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
        {
            var query = Query(filter).Skip(pageIndex * size).Limit(size);
            return (isDescending ? query.SortByDescending(order) : query.SortBy(order)).ToEnumerable();
        }

        public void Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
        }
        public void Insert(IEnumerable<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }

        public bool Replace(TEntity entity)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)entity.Id).IdentityValue)));
            return Collection.ReplaceOne(matchById, entity).IsAcknowledged;
        }
        //public void Replace(IEnumerable<TEntity> entities)
        //{
        //    foreach (TEntity entity in entities)
        //    {
        //        Replace(entity);
        //    }
        //}

        public bool Update<TField>(TEntity entity, Expression<Func<TEntity, TField>> field, TField value)
        {
            return Update(entity, Updater.Set(field, value));
        }
        public bool Update(TEntity entity, UpdateDefinition<TEntity> update)
        {
            return Update(Filter.Eq(i => i.Id, entity.Id), update);
        }
        public bool Update<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value)
        {
            return Update(filter, Updater.Set(field, value));
        }
        public bool Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            //return Collection.UpdateMany(filter, update.CurrentDate(i => i.ModifiedOn)).IsAcknowledged;
            throw new NotImplementedException();
        }

        public bool Delete(Identity id)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return Collection.DeleteOne(matchById).IsAcknowledged;
        }
        public bool Delete(TEntity entity)
        {
            return Delete(entity.Id);
        }
        public bool Delete(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.DeleteMany(filter).IsAcknowledged;
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.AsQueryable<TEntity>().Any(filter);
        }
        public long Count()
        {
            throw new NotImplementedException();
        }
        public long Count(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }


        private IMongoCollection<TEntity> Collection
        {
            get; set;
        }
        private FilterDefinitionBuilder<TEntity> Filter
        {
            get
            {
                return Builders<TEntity>.Filter;
            }
        }
        private UpdateDefinitionBuilder<TEntity> Updater
        {
            get
            {
                return Builders<TEntity>.Update;
            }
        }
        private ProjectionDefinitionBuilder<TEntity> Project
        {
            get
            {
                return Builders<TEntity>.Projection;
            }
        }
        private IFindFluent<TEntity, TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter);
        }
        protected int GetOffset(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }
    }
}
