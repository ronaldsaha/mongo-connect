using MongoConnect.Models;
using MongoConnect.Repositories;
using MongoConnect.Workspace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace.Repositories
{
    public class RepositoryFactory
    {
        public static void Initialize(string connectionString)
        {
            MongoRepositoryFactory.Initialize<RepositoryRegistrar>(connectionString);
        }

        public static void Initialize(string connectionString, TenantProvider tenantProvider)
        {
            MongoRepositoryFactory.IntializeMultiTenant<RepositoryRegistrar>(connectionString, tenantProvider);
        }

        public static RepositorySession CreateSession()
        {
            return new RepositorySession(MongoRepositoryFactory.CreateContext());
        }
    }
}
