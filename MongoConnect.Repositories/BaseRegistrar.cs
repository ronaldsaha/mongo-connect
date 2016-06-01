using MongoConnect.Models;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public abstract class BaseRegistrar
    {
        public void Register()
        {
            if (IsRegistered) return;
            IsRegistered = true;
            ObjectIDGenerator IDGenerator = new ObjectIDGenerator();
            BsonSerializer.RegisterIdGenerator(typeof(Identity), IDGenerator);
            BsonSerializer.RegisterIdGenerator(typeof(ObjectIdentity), IDGenerator);

            ObjectIDSerializer IDSerializer = new ObjectIDSerializer();
            BsonSerializer.RegisterSerializer(typeof(Identity), IDSerializer);
            BsonSerializer.RegisterSerializer(typeof(ObjectIdentity), IDSerializer);

            OnRegistration();
        }
        public abstract void OnRegistration();

        private static bool IsRegistered = false;
    }
}
