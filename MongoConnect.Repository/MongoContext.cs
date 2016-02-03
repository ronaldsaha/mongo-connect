using MongoConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public class MongoContext : Context
    {
        public MongoContext()
        {
            IdentityManager = new ObjectIdentityManager();
        }

        public override Identity GetEmptyID()
        {
            return IdentityManager.GetEmptyID();
        }

        public override Identity GetNewID()
        {
            return IdentityManager.GetNewID();
        }

        private ObjectIdentityManager IdentityManager;
    }
}
