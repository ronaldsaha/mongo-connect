using MongoConnect.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public class ObjectIdentityManager : IdentityManager
    {
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
