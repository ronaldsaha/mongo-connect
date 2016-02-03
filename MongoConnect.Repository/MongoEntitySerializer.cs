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
    public class MongoEntitySerializer<TEntity> : DocumentSerializer<TEntity> where TEntity : Entity
    {
        public virtual BsonDocument Serialize(TEntity entity)
        {
            return entity.ToBsonDocument();
        }

        public virtual TEntity Deserialize(BsonDocument document)
        {
            return BsonSerializer.Deserialize<TEntity>(document);
        }

        public virtual IEnumerable<TEntity> Deserialize(IEnumerator<BsonDocument> documents)
        {
            throw new NotImplementedException();
        }
    }
}
