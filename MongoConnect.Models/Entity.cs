using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public abstract class Entity
    {
        protected Entity() : this(new NullIdentity()) { }
        protected Entity(Identity id) { Id = id; }
        public Identity Id { get; internal set; }
        public virtual void UpdateContext(Context context)
        {
            if (Id.IsNull)
                Id = context.GetNewID();
        }
    }
}