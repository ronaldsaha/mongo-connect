using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public class NullIdentity : Identity
    {
        public NullIdentity() { }
        public bool IsNull { get { return true; } }
        public string Value { get { return string.Empty; } }
    }
}
