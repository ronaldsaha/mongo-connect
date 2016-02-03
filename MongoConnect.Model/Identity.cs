using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Model
{
    public interface Identity
    {
        string Value { get; }
        bool IsNull { get; }
    }
}
