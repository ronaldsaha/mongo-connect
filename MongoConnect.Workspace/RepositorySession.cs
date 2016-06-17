using MongoConnect.Models;
using MongoConnect.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace
{
    internal class RepositorySession : BaseSession
    {
        public RepositorySession(string connectionUrl) : base(connectionUrl) { }

        public Context GetContext()
        {
            return this.Context;
        }

        public PersonRepository GetPersonRepository()
        {
            return new PersonRepository(this.Context, "Person");
        }
    }
}
