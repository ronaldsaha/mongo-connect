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
    public abstract class BaseRepository<TEntity>: Repository<TEntity> where TEntity : Entity
    {
        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        #region MongoSpecific
        public IMongoCollection<TEntity> Collection
        {
            get; private set;
        }

        public FilterDefinitionBuilder<TEntity> Filter
        {
            get
            {
                return Builders<TEntity>.Filter;
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
        }

        private IFindFluent<TEntity, TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter);
        }
        #endregion MongoSpecific

        #region CRUD
        public virtual TEntity Get(string id)
        {
            return Find(i => ((ObjectIdentity)i.Id).Value == id).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
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

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
        {
            var query = Query(filter).Skip(pageIndex * size).Limit(size);
            return (isDescending ? query.SortByDescending(order) : query.SortBy(order)).ToEnumerable();
        }

        public virtual void Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }

        public virtual void Replace(TEntity entity)
        {
            Collection.ReplaceOne(i => i.Id == entity.Id, entity);
        }

        public void Replace(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Replace(entity);
            }
        }

        public bool Update<TField>(TEntity entity, Expression<Func<TEntity, TField>> field, TField value)
        {
            return Update(entity, Updater.Set(field, value));
        }

        public virtual bool Update(TEntity entity, UpdateDefinition<TEntity> update)
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

        public void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public virtual void Delete(string id)
        {
            Collection.DeleteOne(i => ((ObjectIdentity)i.Id).Value == id);
        }

        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            Collection.DeleteMany(filter);
        }
        #endregion CRUD

        #region Simplicity
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


    public TEntity Read(Identity id)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            return Collection.Find(matchById).FirstOrDefault();
        }

        public bool Update(TEntity entity)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)entity.Id).IdentityValue)));
            ReplaceOneResult result = Collection.ReplaceOne(matchById, entity);
            return result.ModifiedCount == 1;
        }

        public bool Delete(Identity id)
        {
            BsonDocument matchById = new BsonDocument().Add(new BsonElement("_id", new BsonObjectId(((ObjectIdentity)id).IdentityValue)));
            DeleteResult result = Collection.DeleteOne(matchById);
            return result.DeletedCount == 1;
        }

        public List<TEntity> ReadMany()
        {
            return Collection.Find(Builders<TEntity>.Filter.Empty).ToList();
        }
        public List<TEntity> ReadMany(int pageIndex, int pageSize)
        {
            return Collection.Find(Builders<TEntity>.Filter.Empty).Skip(GetOffset(pageIndex, pageSize)).Limit(pageSize).ToList();
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

        protected int GetOffset(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }

        //protected IMongoCollection<TEntity> _Collection;
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


    //public class Repository<TEntity> : IRepository<TEntity>
    //    where T : Entity
    //{
    //    #region MongoSpecific
    //    public Repository(string connectionString)
    //    {
    //        _Collection = Database<TEntity>.GetCollectionFromConnectionString(connectionString);
    //    }

    //    public IMongoCollection<TEntity> _Collection
    //    {
    //        get; private set;
    //    }

    //    public FilterDefinitionBuilder<TEntity> Filter
    //    {
    //        get
    //        {
    //            return Builders<TEntity>.Filter;
    //        }
    //    }

    //    public UpdateDefinitionBuilder<TEntity> Updater
    //    {
    //        get
    //        {
    //            return Builders<TEntity>.Update;
    //        }
    //    }

    //    public ProjectionDefinitionBuilder<TEntity> Project
    //    {
    //        get
    //        {
    //            return Builders<TEntity>.Projection;
    //        }
    //    }

    //    private IFindFluent<TEntity, T> Query(Expression<Func<TEntity, bool>> filter)
    //    {
    //        return _Collection.Find(filter);
    //    }
    //    #endregion MongoSpecific

    //    #region CRUD
    //    public virtual T Get(string id)
    //    {
    //        return Find(i => ((ObjectIdentity)i.Id).Value == id).FirstOrDefault();
    //    }

    //    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
    //    {
    //        return Query(filter).ToEnumerable();
    //    }

    //    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int pageIndex, int size)
    //    {
    //        return Find(filter, i => i.Id, pageIndex, size);
    //    }

    //    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size)
    //    {
    //        return Find(filter, order, pageIndex, size, true);
    //    }

    //    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
    //    {
    //        var query = Query(filter).Skip(pageIndex * size).Limit(size);
    //        return (isDescending ? query.SortByDescending(order) : query.SortBy(order)).ToEnumerable();
    //    }

    //    public virtual void Insert(T entity)
    //    {
    //        _Collection.InsertOne(entity);
    //    }

    //    public virtual void Insert(IEnumerable<TEntity> entities)
    //    {
    //        _Collection.InsertMany(entities);
    //    }

    //    public virtual void Replace(T entity)
    //    {
    //        _Collection.ReplaceOne(i => i.Id == entity.Id, entity);
    //    }

    //    public void Replace(IEnumerable<TEntity> entities)
    //    {
    //        foreach (T entity in entities)
    //        {
    //            Replace(entity);
    //        }
    //    }

    //    public bool Update<TField>(T entity, Expression<Func<TEntity, TField>> field, TField value)
    //    {
    //        return Update(entity, Updater.Set(field, value));
    //    }

    //    public virtual bool Update(T entity, UpdateDefinition<TEntity> update)
    //    {
    //        return Update(Filter.Eq(i => i.Id, entity.Id), update);
    //    }

    //    public bool Update<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value)
    //    {
    //        return Update(filter, Updater.Set(field, value));
    //    }

    //    public bool Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
    //    {
    //        //return _Collection.UpdateMany(filter, update.CurrentDate(i => i.ModifiedOn)).IsAcknowledged;
    //        return true;
    //    }

    //    public void Delete(T entity)
    //    {
    //        Delete(((ObjectIdentity)entity.Id).Value);
    //    }

    //    public virtual void Delete(string id)
    //    {
    //        _Collection.DeleteOne(i => ((ObjectIdentity)i.Id).Value == id);
    //    }

    //    public void Delete(Expression<Func<T, bool>> filter)
    //    {
    //        _Collection.DeleteMany(filter);
    //    }
    //    #endregion CRUD

    //    #region Simplicity
    //    public bool Any(Expression<Func<T, bool>> filter)
    //    {
    //        return _Collection.AsQueryable<TEntity>().Any(filter);
    //    }
    //    #endregion Simplicity
    //}
}
