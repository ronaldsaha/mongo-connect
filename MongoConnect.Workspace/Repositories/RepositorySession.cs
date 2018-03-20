using MongoConnect.Models;
using MongoConnect.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace.Repositories
{
    public class RepositorySession
    {
        public RepositorySession(MongoContext context) { Context = context; }

        public PersonRepository GetPersonRepository()
        {
            return new PersonRepository(Context, "Person");
        }

        public ClientRepository GetClientRepository()
        {
            return new ClientRepository(Context, "Client");
        }

        public MongoContext Context;
    }
}
