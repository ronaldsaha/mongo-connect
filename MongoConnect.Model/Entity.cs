using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Model
{
    public abstract class Entity
    {
        protected Entity() { }
        protected Entity(Identity id)
        {
            Id = id;
        }

        public Identity Id { get; private set; }
    }
}
