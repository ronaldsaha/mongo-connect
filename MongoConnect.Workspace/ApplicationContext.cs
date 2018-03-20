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
            CurrentKey = new Guid().ToString();
        }
        public string CollectionName { get { return "Client"; } }
        public string KeyPropertyName { get { return "_Tenant"; } }
        public string CurrentKey { get; set; }
    }
}
