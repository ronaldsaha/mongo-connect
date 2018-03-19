using MongoConnect.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public class MongoRepositoryFactory
    {
        public static void Initialize<T>(string connectionString) where T : BaseRegistrar, new()
        {
            ConnectionString = connectionString;
            new T().Register();
        }

        public static void IntializeMultiTenant<T>(string connectionString, TenantProvider tenantProvider) where T : BaseRegistrar, new()
        {
            IsMultiTenant = true;
            ConnectionString = connectionString;
            TenantProvider = tenantProvider;
            new T().Register();
        }

        public static IdentityProvider CreateContext()
        {
            if (IsMultiTenant)
            {
                string tenantCollectionName = TenantProvider.GetTenantCollectionName();
                string tenantKey = TenantProvider.GetCurrentTenantKey();

                if (string.IsNullOrEmpty(tenantCollectionName)) { throw new InvalidOperationException("Tenant collection name is not provided."); }
                if (string.IsNullOrEmpty(tenantKey)) { throw new ArgumentNullException("Tenant key is not provided."); }

                BasicCollection<Tenant> collection = new BasicCollection<Tenant>(new MongoContext(GetDatabase()), tenantCollectionName);
                Tenant tenant = collection.Find(Builders<Tenant>.Filter.Eq<string>("Key", tenantKey)).FirstOrDefault();
                return new MongoTenantContext(GetDatabase(), tenant.Id);
            }

            return new MongoContext(GetDatabase());
        }

        private static IMongoDatabase GetDatabase()
        {
            MongoUrl mongoUrl = MongoUrl.Create(ConnectionString);
            return new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
        }

        internal static bool IsMultiTenant = false;
        private static string ConnectionString;
        private static TenantProvider TenantProvider;
    }
}
