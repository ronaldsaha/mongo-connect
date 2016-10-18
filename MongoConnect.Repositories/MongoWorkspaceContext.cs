using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoConnect.Models;

namespace MongoConnect.Repositories
{
    public class MongoWorkspaceContext : MongoContext, Context, WorkspaceContext
    {
        public MongoWorkspaceContext(Entity workspace) : base()
        {
            Workspace = workspace;
        }

        public Entity Workspace { get; internal set; }
    }
}
