using MongoConnect.Models;
using MongoConnect.Repositories;
using MongoConnect.Workspace.Models;
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
                ApplicationContext applicationContext = new ApplicationContext();
                //RepositoryFactory.Initialize("mongodb://localhost/MongoConnectTest");
                RepositoryFactory.Initialize("mongodb://localhost/MongoConnectTest", applicationContext);

                SingleTenantCRUD(applicationContext.CurrentKey);
                MultiTenantCRUD();
            }
            catch (Exception exception) { }
        }

        public static void SingleTenantCRUD(string key)
        {
            RepositorySession repositroySession = RepositoryFactory.CreateSession();

            ClientRepository repository = repositroySession.GetClientRepository();

            Client model = new Client(key);

            repository.Insert(model);
        }

        public static void MultiTenantCRUD()
        {
            RepositorySession repositroySession = RepositoryFactory.CreateSession();

            PersonRepository personRepository = repositroySession.GetPersonRepository();

            Person person = new Person("This is test");

            personRepository.Insert(person);
            Person personFromDB = personRepository.Find(person.Id);
            personFromDB.FullName = "Name Changed";
            personRepository.Update(personFromDB);
            //personRepository.Delete(person.Id);
        }
    }
}
