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
            get; private set;
        }

        public FilterDefinitionBuilder<TEntity> Filter
        {
            get
            {
                return new FilterDefinitionBuilder<TEntity>();
                //return Builders<TEntity>.Filter;
            }
        }

        public UpdateDefinitionBuilder<TEntity> Updater
        {
            get
            {
                return Builders<TEntity>.Update;
            }
        }

        public ProjectionDefinitionBuilder<TEntity> Project
        {
            get
            {
                return Builders<TEntity>.Projection;
            }
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return Collection.Find(matchById).FirstOrDefault();
        }
        public IEnumerable<TEntity> Find()
        {
            return Collection.Find(Filter.Empty).ToList();
        }
        private IFindFluent<TEntity, TEntity> Query(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize)
        {
            return Collection.Find(filter).Skip(pageIndex * pageSize).Limit(pageSize);
        }
        #endregion MongoSpecific

        public TEntity FindOne(Identity id)
        {
            //return Find(i => i.Id == id).FirstOrDefault();
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return Collection.Find(matchById).FirstOrDefault();
        }

        public IEnumerable<TEntity> FindAll()
        public IEnumerable<TEntity> Find(int pageIndex, int pageSize)
        {
            return Collection.Find(Filter.Empty).Skip(GetOffset(pageIndex, pageSize)).Limit(pageSize).ToList();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return FindAll(i => i.Id, true);
        }
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, bool isAscending)
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int pageIndex, int size)
        {
            var query = Collection.Find(Builders<TEntity>.Filter.Empty);
            return (isAscending? query.SortBy(order) : query.SortByDescending(order)).ToEnumerable();
        }
        public IEnumerable<TEntity> FindAll(int pageIndex, int pageSize)
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size)
        {
            return FindAll(i => i.Id, true, pageIndex, pageSize);
        }
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize)
        {
            var query = Collection.Find(Builders<TEntity>.Filter.Empty).Skip(pageIndex * pageSize).Limit(pageSize);
            return (isAscending ? query.SortBy(order) : query.SortByDescending(order)).ToEnumerable();
        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter)
        {
            return FindMany(filter, i => i.Id, true);
        }
        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending)
        {
            var query = Query(filter);
            return (isAscending ? query.SortBy(order) : query.SortByDescending(order)).ToEnumerable();
        }
        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize)
        {
            return FindMany(filter, i => i.Id, true, pageIndex, pageSize);
        }
        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isAscending, int pageIndex, int pageSize)
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
        {
            var query = Query(filter, pageIndex , pageSize);
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
        #endregion Simplicity


        public bool Update(TEntity entity)
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


        //public PagedList<TEntity> ReadMany(int pageIndex, int pageSize)
        //{
        //    return new PagedList<TEntity>()
        //    {
        //        Lists = _Collection.FindAll(GetOffset(pageIndex, pageSize), pageSize).ToList(),
        //        Total = (int)_Collection.Count()
        //    };
        //}

        //public PagedList<TEntity> ReadMany(MongoQuery query)
        //{
        //    return new PagedList<TEntity>()
        //    {
        //        Lists = _Collection.FindAll(query).ToList(),
        //        Total = (int)_Collection.Count()
        //    };
        //}

        //public PagedList<TEntity> ReadMany(MongoQuery query, int pageIndex, int pageSize)
        //{
        //    return new PagedList<TEntity>()
        //    {
        //        Lists = _Collection.FindAll(query, pageIndex, pageSize).ToList(),
        //        Total = (int)_Collection.Count()
        //    };
        //}

        //public bool Delete()
        //{
        //    return _Collection.RemoveAll();
        //}

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



    //internal abstract class WorkspaceSimpleBaseRepository<TEntity> : MongoEntityRepository<TEntity>, EntityRepository<TEntity> where T : WorkspaceEntity
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

    //internal abstract class WorkspaceListBaseRepository<TEntity> : WorkspaceSimpleBaseRepository<TEntity>, EntityListRepository<TEntity> where T : WorkspaceEntity
    //{
    //    public WorkspaceListBaseRepository(MongoDatabase database, string collectionName, Context context)
    //        : base(database, collectionName, context)
    //    {
    //    }

    //    public PagedList<TEntity> Read()
    //    {
    //        return new PagedList<TEntity>()
    //        {
    //            Lists = _Collection.Find(GetClientQuery()).ToList(),
    //            Total = (int)_Collection.Count()
    //        };
    //    }

    //    public PagedList<TEntity> Read(int pageIndex, int pageSize)
    //    {
    //        return new PagedList<TEntity>()
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
