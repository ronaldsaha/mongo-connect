using MongoConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public class MongoWorkspaceContext : MongoContext
    {
        public MongoWorkspaceContext(Identity workspaceId)
            : base()
        {
            WorkspaceId = workspaceId;
        }

        public Identity WorkspaceId { get; set; }
    }
}
