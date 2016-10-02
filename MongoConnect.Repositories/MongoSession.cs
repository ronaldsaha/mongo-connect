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
        protected MongoSession(Context context, string connectionUrl)
        {
            Context = context;
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

        public static Context CreateContext(string connectionUrl, string workspaceKey)
        {
            // TODO: ..
            return new MongoWorkspaceContext(new NullIdentity());
        }

        public Context Context { get; private set; }
        internal IMongoDatabase Database { get; private set; }
    }
}
