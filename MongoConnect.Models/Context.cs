using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public abstract class Context
    {
        protected Context() { }
        public abstract Identity GetEmptyID();
        public abstract Identity GetNewID();
    }
}
