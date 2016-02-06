using MongoConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Workspace
{
    public class Person : Entity
    {
        public Person(Identity id) : base(id) { }
        public string FullName { get; set; }
    }
}
