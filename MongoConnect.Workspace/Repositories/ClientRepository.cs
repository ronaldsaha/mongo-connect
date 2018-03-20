using MongoConnect.Models;
using MongoConnect.Repositories;
using MongoConnect.Workspace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {
        public ClientRepository(MongoContext context, string collectionName)
            : base(context, collectionName) { }
    }
}
