using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories.Utils
{
    internal class ObjectIDGenerator : IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            return new ObjectIdentity((MongoDB.Bson.ObjectId)MongoDB.Bson.Serialization.IdGenerators.ObjectIdGenerator.Instance.GenerateId(container, document));
        }

        public bool IsEmpty(object id)
        {
            if (id == null)
                return true;
            if (id is MongoConnect.Models.NullIdentity)
                throw new System.InvalidOperationException("Use Context.CreateEntity to create new entity.");
            ObjectIdentity _id = id as ObjectIdentity;
            return _id.IsNull;
        }
    }
}
