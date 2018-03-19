using MongoConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace.Models
{
    public class Client : Tenant
    {
        public Client(string key) : base(key) { }
    }
}
