using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public abstract class Entity
    {
        protected Entity() { Id = new NullIdentity(); }

        internal virtual void SetContext(Context context)
        {
            Id = context.GetNewID();
        }

        public Identity Id { get; internal set; }
    }
}
