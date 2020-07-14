using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Helpers
{
    public class FilterModel
    {
        public int take { get; set; }
        public int beginIndex { get; set; }
        public bool orderByDesc { get; set; }
        public string orderByColumnName { get; set; }

        public List<FilterModelIn> propertyFilterValues { get; set; }
    }

    public class FilterModelIn
    {
        public string property { get; set; }
        public string condition { get; set; }//>=,=<,<,>..
        public string value { get; set; }
        public string optr { get; set; }//&&,||..
    }
}
