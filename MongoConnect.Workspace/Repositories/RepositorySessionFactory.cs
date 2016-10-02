using MongoConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace.Repositories
{
    public class RepositorySessionFactory
    {
        public RepositorySessionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public RepositorySession CreateSession(Context context)
        {
            return new RepositorySession(context, ConnectionString);
        }

        public static void Initialize()
        {
            RepositorySession.Initialize<RepositoryRegistrar>();
        }

        public Context CreateContext()
        {
            return RepositorySession.CreateContext();
        }

        public Context CreateContext(string workspaceKey)
        {
            return RepositorySession.CreateContext(ConnectionString, workspaceKey);
        }

        private string ConnectionString { get; set; }
    }
}
