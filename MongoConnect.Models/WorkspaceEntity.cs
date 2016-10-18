using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public abstract class WorkspaceEntity : Entity
    {
        public WorkspaceEntity() : base() { WorkspaceId = new NullIdentity(); }
        public WorkspaceEntity(Identity id) : base(id) { WorkspaceId = new NullIdentity(); }
        public Identity WorkspaceId { get; internal set; }
        public override void UpdateContext(Context context)
        {
            //base.UpdateContext(context);
            WorkspaceId = ((WorkspaceContext)context).Workspace.Id;
        }
    }
}