using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Domain
{
    public class Cars : BaseObject
    {
        public string name { get; set; }
        public int modelYear { get; set; }

        public virtual Brands Brand { get; set; }
    }
}
