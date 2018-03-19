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
    public class WorkspaceCollection<TEntity> : BasicCollection<TEntity> where TEntity : Entity
    {
        public WorkspaceCollection(MongoContext context, string collectionName)
            : base(context, collectionName)
        {
            _WorspaceCollection = context.Database.GetCollection<BsonDocument>(collectionName);
        }

        public override void InsertOne(TEntity entity)
        {
            UpdateEntity(entity);
            _WorspaceCollection.InsertOne(UpdateAndGetTenantEntity(entity));
        }

        public override ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity entity)
        {
            return _WorspaceCollection.ReplaceOne(ProcessFilter(filter).ToBsonDocument(), UpdateAndGetTenantEntity(entity));
        }

        public override UpdateResult UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            return _WorspaceCollection.UpdateOne(ProcessFilter(filter).ToBsonDocument(), UpdateAndGetUpdateDefinition(update));
        }

        public override IAsyncCursor<TResult> Aggregate<TResult>(IPipelineStageDefinition[] pipeline)
        {
            PipelineStageDefinition<TEntity, TResult> workspaceQuery = new BsonDocument { { "$match", GetWorkspaceFilter().ToBsonDocument() } };
            List<IPipelineStageDefinition> pipelineList = pipeline.ToList();
            pipelineList.Insert(0, workspaceQuery);
            return base.Aggregate<TResult>(pipelineList.ToArray());
        }

        public override IAggregateFluent<TEntity> Aggregate()
        {
            return Collection.Aggregate().Match(GetWorkspaceFilter());
        }

        protected override FilterDefinition<TEntity> ProcessFilter(FilterDefinition<TEntity> filter)
        {
            return Builders<TEntity>.Filter.And(filter, GetWorkspaceFilter());
        }

        private BsonDocument UpdateAndGetUpdateDefinition(UpdateDefinition<TEntity> update)
        {
            BsonDocument document = update.ToBsonDocument();
            document.InsertAt(document.ElementCount, new BsonElement("_Tenant", ((ObjectIdentity)((MongoTenantContext)Context).TenantId).IdentityValue));
            return document;
        }

        private BsonDocument UpdateAndGetTenantEntity(TEntity entity)
        {
            BsonDocument document = entity.ToBsonDocument();
            document.InsertAt(document.ElementCount, new BsonElement("_Tenant", ((ObjectIdentity)((MongoTenantContext)Context).TenantId).IdentityValue));
            return document;
        }

        private FilterDefinition<TEntity> GetWorkspaceFilter()
        {
            ObjectIdentity workspaceId = (ObjectIdentity)((MongoTenantContext)Context).TenantId;
            return Builders<TEntity>.Filter.Eq<ObjectId>("_Tenant", workspaceId.IdentityValue);
        }

        protected IMongoCollection<BsonDocument> _WorspaceCollection;
    }
}
