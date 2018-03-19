using MongoConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace
{
    public class ApplicationContext : TenantProvider
    {
        public ApplicationContext()
        {
            TenantKey = "TestKey";
        }

        public string GetTenantCollectionName()
        {
            return "Client";
        }

        public string GetCurrentTenantKey()
        {
            return TenantKey;
        }

        public string TenantKey { get; private set; }
    }
}
