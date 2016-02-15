using MongoConnect.Models;
using MongoConnect.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(Context context, string collectionName) : base(context, collectionName) { }
    }
}
