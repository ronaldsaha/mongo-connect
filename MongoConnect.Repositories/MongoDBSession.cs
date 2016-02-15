using MongoConnect.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public abstract class MongoDBSession
    {
        private MongoDBSession() { }
        protected MongoDBSession(string connectionUrl)
        {
            MongoUrl mongoUrl = MongoUrl.Create(connectionUrl);
            Database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
            Context = new MongoDBContext(this);
        }

        public static void Initialize()
        {
            if (IsInitialized) return;
            IsInitialized = true;
            ObjectIDGenerator IDGenerator = new ObjectIDGenerator();
            BsonSerializer.RegisterIdGenerator(typeof(Identity), IDGenerator);
            BsonSerializer.RegisterIdGenerator(typeof(ObjectIdentity), IDGenerator);

            ObjectIDSerializer IDSerializer = new ObjectIDSerializer();
            BsonSerializer.RegisterSerializer(typeof(Identity), IDSerializer);
            BsonSerializer.RegisterSerializer(typeof(ObjectIdentity), IDSerializer);
        }
        private static bool IsInitialized = false;

        internal IMongoDatabase Database { get; private set; }
        public MongoDBContext Context { get; private set; }
    }
}
