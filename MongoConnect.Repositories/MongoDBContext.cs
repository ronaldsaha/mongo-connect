using MongoConnect.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public class MongoDBContext : Context
    {
        public MongoDBContext(MongoDBSession databaseSession)
        {
            DatabaseSession = databaseSession;
        }
        public override Identity GetEmptyID()
        {
            return new ObjectIdentity();
        }
        public override Identity GetNewID()
        {
            return new ObjectIdentity(ObjectId.GenerateNewId());
        }

        public MongoDBSession DatabaseSession { get; private set; }
    }
}
