using MongoConnect.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace
{
    public class PersonRepository : EntityRepository<Person>
    {
        public PersonRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }
    }
}
