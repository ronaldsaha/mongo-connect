using MongoConnect.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public class MongoConnectConfiguration
    {
        public static void Initialize<T>(string connectionString) where T : BaseRegistrar, new()
        {
            ConnectionString = connectionString;
            new T().Register();
        }

        public static void IntializeMultiTenant<T>(string connectionString, TenantProvider tenantProvider) where T : BaseRegistrar, new()
        {
            IsMultiTenant = true;
            TenantProvider = tenantProvider;
            Initialize<T>(connectionString);
        }

        public static MongoContext CreateContext()
        {
            MongoUrl mongoUrl = MongoUrl.Create(ConnectionString);
            IMongoDatabase mongoDatabase = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
            return IsMultiTenant ? new MongoTenantContext(mongoDatabase, TenantProvider) : new MongoContext(mongoDatabase);
        }

        internal static bool IsMultiTenant = false;
        private static string ConnectionString;
        private static TenantProvider TenantProvider;
    }
}
