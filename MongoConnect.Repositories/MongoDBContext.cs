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
        public MongoDBContext()
        {
        }
        public override Identity GetEmptyID()
        {
            return new ObjectIdentity();
        }
        public override Identity GetNewID()
        {
            return new ObjectIdentity(ObjectId.GenerateNewId());
        }
    }
}
