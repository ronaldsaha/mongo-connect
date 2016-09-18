using MongoConnect.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public class ObjectIdentity : Identity
    {
        public ObjectIdentity()
        {
            this.IdentityValue = ObjectId.Empty;
        }

        public ObjectIdentity(ObjectId id)
        {
            this.IdentityValue = id;
        }

        public ObjectIdentity(string id)
        {
            this.IdentityValue = ObjectId.Parse(id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ObjectIdentity);
        }

        public virtual bool Equals(ObjectIdentity obj)
        {
            if (obj == null)
                return false;

            return IdentityValue.Equals(obj.IdentityValue);
        }

        public override int GetHashCode()
        {
            return IdentityValue.GetHashCode();
        }

        public override string ToString()
        {
            return IdentityValue.ToString();
        }

        public bool IsNull { get { return IdentityValue == ObjectId.Empty; } }
        public string Value { get { return this.ToString(); } }
        public ObjectId IdentityValue { get; set; }
    }
}
