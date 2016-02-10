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
            try
            {
                RepositorySession.Initialize();
                string connectionUrl = "mongodb://localhost/MongoConnectTest";
                RepositorySession session = new RepositorySession(connectionUrl);

                CreateReadUpdateDelete(session);
            }
            catch (Exception e) { }
        }

        public static void CreateReadUpdateDelete(RepositorySession session)
        {
            Context context = session.GetContext();
            PersonRepository personRepo = session.GetPersonRepository();

            Person person = new Person(context, "This is test");

            personRepo.Insert(person);
            Person personFromDB = personRepo.Find(person.Id);
            personFromDB.FullName = "Name Changed";
            personRepo.Replace(personFromDB);
            personRepo.Delete(person.Id);
        }
    }
}
