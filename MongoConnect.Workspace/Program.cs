using MongoConnect.Models;
using MongoConnect.Repositories;
using MongoConnect.Workspace.Repositories;
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
                RepositorySessionFactory.Initialize();
                RepositorySessionFactory repositorySessionFactory = new RepositorySessionFactory("mongodb://localhost/MongoConnectTest");
                RepositorySession repositroySession = repositorySessionFactory.CreateSession(repositorySessionFactory.CreateContext());

                CreateReadUpdateDelete(repositroySession);
            }
            catch (Exception e) { }
        }

        public static void CreateReadUpdateDelete(RepositorySession session)
        {
            Context context = session.Context;
            PersonRepository personRepo = session.GetPersonRepository();

            Person person = new Person("This is test");

            personRepo.Insert(person);
            Person personFromDB = personRepo.Find(person.Id);
            personFromDB.FullName = "Name Changed";
            personRepo.Update(personFromDB);
            personRepo.Delete(person.Id);
        }
    }
}
