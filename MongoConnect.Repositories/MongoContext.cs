using MongoConnect.Models;
using MongoConnect.Repositories.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public class MongoContext
    {
        internal MongoContext(IMongoDatabase database)
        {
            Database = database;
            IdentityProvider = new ObjectIdentityProvider();
        }

        internal IMongoDatabase Database { get; private set; }
        internal IdentityProvider IdentityProvider { get; private set; }
    }
}
