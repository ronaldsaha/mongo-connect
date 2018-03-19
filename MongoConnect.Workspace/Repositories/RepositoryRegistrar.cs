using MongoConnect.Models;
using MongoConnect.Repositories;
using MongoConnect.Workspace.Models;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace.Repositories
{
    internal class RepositoryRegistrar : BaseRegistrar
    {
        public override void OnRegistration()
        {
            BsonClassMap.RegisterClassMap<Client>();
        }
    }
}
