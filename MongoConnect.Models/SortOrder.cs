using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public class SortOrder
    {
        public SortOrder(FieldName fieldName, SortDirection direction)
        {
            FieldName = fieldName;
            Direction = direction;
        }

        public static SortOrder Ascending(FieldName fieldName)
        {
            return new SortOrder(fieldName, SortDirection.Ascending);
        }

        public static SortOrder Descending(FieldName fieldName)
        {
            return new SortOrder(fieldName, SortDirection.Descending);
        }

        public FieldName FieldName { get; private set; }
        public SortDirection Direction { get; private set; }
    }
}
