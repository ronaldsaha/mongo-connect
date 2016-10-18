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
    internal class WorkspaceCollection<TEntity> : BasicCollection<TEntity> where TEntity : Entity
    {
        public WorkspaceCollection(Context context, MongoSession session, string collectionName)
            : base(context, session, collectionName)
        {
        }

        protected override FilterDefinition<TEntity> ProcessFilter(FilterDefinition<TEntity> filter)
        {
            ObjectIdentity workspaceId = (ObjectIdentity)((WorkspaceContext)Context).Workspace.Id;
            return Builders<TEntity>.Filter.And(filter,
                Builders<TEntity>.Filter.Eq<ObjectId>("WorkspaceId", workspaceId.IdentityValue));
        }
    }
}
