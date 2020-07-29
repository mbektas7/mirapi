using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Domain
{
    public class PostCategory : BaseObject
    {
        public int postId { get; set; }
        public int categoryId { get; set; }
    }
}
