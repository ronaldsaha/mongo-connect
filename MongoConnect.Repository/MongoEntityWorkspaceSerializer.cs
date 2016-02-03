using MongoConnect.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public class MongoEntityWorkspaceSerializer<TEntity> : DocumentSerializer<TEntity> where TEntity : Entity
    {
        public MongoEntityWorkspaceSerializer(MongoWorkspaceContext context, string workspaceKey)
        {
            Context = context;
            WorkspaceKey = workspaceKey;
        }

        public BsonDocument Serialize(TEntity entity)
        {
            return entity.ToBsonDocument().Add(new BsonElement(WorkspaceKey, ((ObjectIdentity)Context.WorkspaceId).IdentityValue));
        }

        public TEntity Deserialize(BsonDocument document)
        {
            document.Remove(WorkspaceKey);
            return BsonSerializer.Deserialize<TEntity>(document);
        }

        public IEnumerable<TEntity> Deserialize(IEnumerator<BsonDocument> documents)
        {
            throw new NotImplementedException();
        }

        private MongoWorkspaceContext Context;
        private string WorkspaceKey;
    }
}
