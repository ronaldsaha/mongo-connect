using MongoConnect.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public class MongoEntityWorkspaceCollection<TEntity> : MongoEntityCollection<TEntity> where TEntity : Entity
    {
        public MongoEntityWorkspaceCollection(MongoWorkspaceContext context, IMongoDatabase database, string collectionName)
            : base(context, new MongoEntityWorkspaceSerializer<TEntity>(context, "Workspace"), database, collectionName)
        {
        }
    }
}
