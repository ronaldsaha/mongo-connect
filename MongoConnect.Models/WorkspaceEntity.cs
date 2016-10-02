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

        internal override void SetContext(Context context)
        {
            base.SetContext(context);
            WorkspaceId = ((WorkspaceContext)context).WorkspaceId;
        }

        public Identity WorkspaceId { get; internal set; }
    }
}
