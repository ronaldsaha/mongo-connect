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
        public WorkspaceCollection(Context context, MongoSession session, string collectionName)
            : base(context, session, collectionName)
        {
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

        private FilterDefinition<TEntity> GetWorkspaceFilter()
        {
            ObjectIdentity workspaceId = (ObjectIdentity)((WorkspaceContext)Context).Workspace.Id;
            return Builders<TEntity>.Filter.Eq<ObjectId>("WorkspaceId", workspaceId.IdentityValue);
        }
    }
}
