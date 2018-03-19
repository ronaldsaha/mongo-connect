using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoConnect.Models;

namespace MongoConnect.Repositories
{
    public class MongoTenantContext : MongoContext, IdentityProvider
    {
        public MongoTenantContext(IMongoDatabase database, Identity tenantId)
            : base(database) { TenantId = tenantId; }
        public Identity TenantId { get; internal set; }
    }
}
