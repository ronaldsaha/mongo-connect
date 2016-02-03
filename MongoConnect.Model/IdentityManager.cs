using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Model
{
    public interface IdentityManager
    {
        Identity ParseID(string id);
        string IDToString(Identity id);
    }
}
