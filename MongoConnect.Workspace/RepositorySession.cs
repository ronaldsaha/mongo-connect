using MongoConnect.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace
{
    public class RepositorySession : MongoDBSession
    {
        public RepositorySession(string connectionUrl) : base(connectionUrl) { }

        public PersonRepository GetPersonRepository()
        {
            return new PersonRepository(this.Context, "Person");
        }
    }
}
