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
    public abstract class BaseSession
    {
        private BaseSession() { }
        protected BaseSession(string connectionUrl)
        {
            MongoUrl mongoUrl = MongoUrl.Create(connectionUrl);
            Database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
            Context = new BaseContext(this);
        }

        public static void Initialize<T>() where T : BaseRegistrar, new()
        {
            new T().Register();
        }
        

        internal IMongoDatabase Database { get; private set; }
        protected Context Context { get; private set; }
    }
}
