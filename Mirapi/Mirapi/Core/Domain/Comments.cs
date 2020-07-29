using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Domain
{
    public class Comments : BaseObject
    {

        public string comment { get; set; }

        public  int userId { get; set; }
    }
}
