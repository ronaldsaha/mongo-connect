using MongoConnect.Models;
using MongoConnect.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace.Repositories
{
    public class RepositorySession : MongoSession
    {
        public RepositorySession(Context context, string connectionUrl)
            : base(context, connectionUrl) { }

        public PersonRepository GetPersonRepository()
        {
            return new PersonRepository(Context, this, "Person");
        }
    }
}
