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
            //return Find(i => i.Id == id).FirstOrDefault();
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return Collection.Find(matchById).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Collection.Find(Filter.Empty).ToList();
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>> order, bool isAscending)
        {
            var query = Collection.Find(Builders<TEntity>.Filter.Empty);
            return (isAscending ? query.SortBy(order) : query.SortByDescending(order)).ToEnumerable();
        }
        public IEnumerable<TEntity> GetAll(int pageIndex, int pageSize)
        {
            return GetAll(i => i.Id, true, pageIndex, pageSize);
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize)
        {
            var query = Collection.Find(Builders<TEntity>.Filter.Empty).Skip(pageIndex * pageSize).Limit(pageSize);
            return (isAscending ? query.SortBy(order) : query.SortByDescending(order)).ToEnumerable();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return FindAll(filter, i => i.Id, true);
        }
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending)
        {
            var query = Query(filter);
            return (isAscending ? query.SortBy(order) : query.SortByDescending(order)).ToEnumerable();
        }
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize)
        {
            return FindAll(filter, i => i.Id, true, pageIndex, pageSize);
        }
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize)
        {
            var query = Query(filter, pageIndex, pageSize);
            return (isAscending ? query.SortBy(order) : query.SortByDescending(order)).ToEnumerable();
        }

        public void Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
        }
        public void Insert(IEnumerable<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }

        public void Update(TEntity entity)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)entity.Id).IdentityValue)));
            Collection.ReplaceOne(matchById, entity);
        }
        //public void Update(IEnumerable<TEntity> entities)
        //{
        //    foreach (TEntity entity in entities)
        //    {
        //        Replace(entity);
        //    }
        //}

        public void FindAndUpdate<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value)
        {
            FindAndUpdate(filter, Updater.Set(field, value));
        }
        public void FindAndUpdate(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            Collection.UpdateMany(filter, update);
        }

        public void Delete(Identity id)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            Collection.DeleteOne(matchById);
        }
        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            Collection.DeleteMany(filter);
        }

        public void Empty()
        {
            Collection.DeleteMany(Filter.Empty);
        }
        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            //return Collection.AsQueryable<TEntity>().Any(filter);
            return Count(filter) > 0;
        }
        public long Count()
        {
            return Collection.Count(Filter.Empty);
        }
        public long Count(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Count(filter);
        }

        protected IFindFluent<TEntity, TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter);
        }
        protected IFindFluent<TEntity, TEntity> Query(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize)
        {
            return Collection.Find(filter).Skip(GetOffset(pageIndex, pageSize)).Limit(pageSize);
        }
        protected int GetOffset(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }

        protected IMongoCollection<TEntity> Collection
        {
            get; set;
        }
        protected FilterDefinitionBuilder<TEntity> Filter
        {
            get
            {
                return new FilterDefinitionBuilder<TEntity>();
                //return Builders<TEntity>.Filter;
            }
        }
        protected UpdateDefinitionBuilder<TEntity> Updater
        {
            get
            {
                return Builders<TEntity>.Update;
            }
        }
        protected ProjectionDefinitionBuilder<TEntity> Project
        {
            get
            {
                return Builders<TEntity>.Projection;
            }
        }
    }
}
