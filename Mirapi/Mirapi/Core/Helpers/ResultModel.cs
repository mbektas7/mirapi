using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Helpers
{
    public class ResultModel<T>
    {
        public T data { get; set; }
        public string message { get; set; }
    }
}
