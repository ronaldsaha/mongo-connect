using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoConnect.Models;

namespace MongoConnect.Repositories
{
    public class MongoWorkspaceContext : MongoContext, WorkspaceContext
    {
        public MongoWorkspaceContext(Identity workspaceId) : base()
        {
            WorkspaceId = workspaceId;
        }

        public Identity WorkspaceId { get; internal set; }
    }
}
