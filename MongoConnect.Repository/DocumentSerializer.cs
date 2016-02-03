using MongoConnect.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public interface DocumentSerializer<TEntity> where TEntity : Entity
    {
        BsonDocument Serialize(TEntity entity);

        TEntity Deserialize(BsonDocument document);
        IEnumerable<TEntity> Deserialize(IEnumerator<BsonDocument> documents);
    }
}
