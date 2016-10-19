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
    public class BasicCollection<TEntity> where TEntity : Entity
    {
        public BasicCollection(Context context, MongoSession session, string collectionName)
        {
            Context = context;
            Collection = ((MongoSession)session).Database.GetCollection<TEntity>(collectionName);
        }

        public IFindFluent<TEntity, TEntity> Find(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(ProcessFilter(filter));
        }

        public long Count(FilterDefinition<TEntity> filter)
        {
            return Collection.Count(ProcessFilter(filter));
        }

        public DeleteResult DeleteMany(FilterDefinition<TEntity> filter)
        {
            return Collection.DeleteMany(ProcessFilter(filter));
        }

        public DeleteResult DeleteOne(FilterDefinition<TEntity> filter)
        {
            return Collection.DeleteOne(ProcessFilter(filter));
        }

        public void InsertOne(TEntity entity)
        {
            entity.UpdateContext(Context);
            Collection.InsertOne(entity);
        }

        public ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity entity)
        {
            entity.UpdateContext(Context);
            return Collection.ReplaceOne(ProcessFilter(filter), entity);
        }

        public UpdateResult UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            return Collection.UpdateOne(ProcessFilter(filter), update);
        }

        public virtual IAsyncCursor<TResult> Aggregate<TResult>(IPipelineStageDefinition[] pipeline)
        {
            return Collection.Aggregate<TResult>(pipeline);
        }

        public virtual IAggregateFluent<TEntity> Aggregate()
        {
            return Collection.Aggregate();
        }

        public virtual IAsyncCursor<TReturn> Distinct<TReturn>(FieldDefinition<TEntity, TReturn> field, FilterDefinition<TEntity> filter)
        {
            return Collection.Distinct<TReturn>(field, ProcessFilter(filter));
        }

        protected virtual FilterDefinition<TEntity> ProcessFilter(FilterDefinition<TEntity> filter)
        {
            return filter;
        }

        protected IMongoCollection<TEntity> Collection;
        protected Context Context;
    }
}
