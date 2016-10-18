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
    public abstract class MongoSession
    {
        protected MongoSession(string connectionUrl)
        {
            MongoUrl mongoUrl = MongoUrl.Create(connectionUrl);
            Database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
        }

        public static void Initialize<T>() where T : BaseRegistrar, new()
        {
            new T().Register();
        }

        public static Context CreateContext()
        {
            return new MongoContext();
        }

        public static Context CreateContext(Entity entity)
        {
            return new MongoWorkspaceContext(entity);
        }

        internal IMongoDatabase Database { get; private set; }
    }
}
