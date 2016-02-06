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
        protected MongoDBSession(string connectionUrl)
        {
            MongoUrl mongoUrl = MongoUrl.Create(connectionUrl);
            _Database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
            _Context = new MongoDBContext();
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

            //initialization codes.
        }

        public Context GetContext()
        {
            return _Context;
        }

        private static bool IsInitialized = false;

        protected IMongoDatabase _Database;
        private MongoDBContext _Context;
    }
}
