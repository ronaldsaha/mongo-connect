using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoConnect.Models;

namespace MongoConnect.Repositories
{
    public class MongoTenantContext : MongoContext
    {
        internal MongoTenantContext(IMongoDatabase database, TenantProvider tenantProvider) : base(database)
        {
            TenantProvider = tenantProvider;
        }

        private void FindTenant()
        {
            string tenantCollectionName = TenantProvider.CollectionName;
            string tenantKeyPropertyName = TenantProvider.KeyPropertyName;
            string tenantKey = TenantProvider.CurrentKey;

            if (string.IsNullOrEmpty(tenantCollectionName)) { throw new InvalidOperationException("Tenant collection name is not provided."); }
            if (string.IsNullOrEmpty(tenantKey)) { throw new ArgumentNullException("Tenant key is not provided."); }

            BasicCollection<Tenant> collection = new BasicCollection<Tenant>(this, tenantCollectionName);
            Tenant = collection.Find(Builders<Tenant>.Filter.Eq<string>("Key", tenantKey)).FirstOrDefault();
        }

        public Identity TenantId
        {
            get
            {
                if (Tenant == null)
                    FindTenant();
                return Tenant.Id;
            }
        }

        private Tenant Tenant;
        internal TenantProvider TenantProvider { get; private set; }
    }
}
