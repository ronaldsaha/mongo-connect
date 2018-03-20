using MongoConnect.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories.Utils
{
    public class ObjectIDSerializer : SerializerBase<Identity>
    {
        public override Identity Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new ObjectIdentity(context.Reader.ReadObjectId());
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Identity value)
        {
            context.Writer.WriteObjectId(((ObjectIdentity)value).IdentityValue);
        }
    }
}
