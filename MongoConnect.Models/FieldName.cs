using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public class FieldName
    {
        public FieldName(string name) { Name = name; }
        public static implicit operator FieldName(string name)
        {
            return new FieldName(name);
        }
        public string Name { get; private set; }
    }
}
