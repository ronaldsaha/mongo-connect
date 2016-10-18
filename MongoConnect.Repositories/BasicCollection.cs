using MongoConnect.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    internal class BasicCollection<TEntity> where TEntity : Entity
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

        protected virtual FilterDefinition<TEntity> ProcessFilter(FilterDefinition<TEntity> filter)
        {
            return filter;
        }

        private IMongoCollection<TEntity> Collection;
        protected Context Context;
    }
}
