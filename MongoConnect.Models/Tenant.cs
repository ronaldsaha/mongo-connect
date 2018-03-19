using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public class Tenant : Entity
    {
        protected Tenant(string key) : base() { Key = key; }
        protected Tenant(Identity id, string key) : base(id) { Key = key; }
        public string Key { get; set; }
    }
}
