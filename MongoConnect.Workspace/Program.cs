using MongoConnect.Models;
using MongoConnect.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionUrl = "mongodb://localhost/MongoConnectTest";
            RepositorySession session = new RepositorySession(connectionUrl);
            Context context = session.GetContext();

            Person person = new Person(session.GetContext().GetNewID());

            PersonRepository personRepo = session.GetPersonRepository();
            personRepo.Create(person);
        }
    }
}
