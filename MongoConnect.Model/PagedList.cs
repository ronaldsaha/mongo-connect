using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Model
{
    public class PagedList<T>
    {
        public List<T> Lists { get; set; }
        public int CurrentIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
