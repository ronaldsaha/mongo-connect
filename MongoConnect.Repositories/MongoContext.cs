using MongoConnect.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public class MongoContext : Context
    {
        public MongoContext() { }
        public Identity GetEmptyID()
        {
            return new ObjectIdentity();
        }
        public Identity GetNewID()
        {
            return new ObjectIdentity(ObjectId.GenerateNewId());
        }
        public Identity ParseID(string id)
        {
            ObjectId objectId = ObjectId.Empty;
            ObjectId.TryParse(id, out objectId);
            return new ObjectIdentity(objectId); ;
        }

        public string IDToString(Identity id)
        {
            if (id.IsNull)
                return string.Empty;
            return id.ToString();
        }
    }
}
