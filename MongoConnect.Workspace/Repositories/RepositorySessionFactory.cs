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

        public static void Initialize()
        {
            RepositorySession.Initialize<RepositoryRegistrar>();
        }

        public Context CreateContext()
        {
            return RepositorySession.CreateContext();
        }

        public RepositorySession CreateSession(Context context)
        {
            return new RepositorySession(context, ConnectionString);
        }

        private string ConnectionString { get; set; }
    }
}
