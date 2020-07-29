using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Domain
{
    public class Post : BaseObject
    {
        public string title { get; set; }

        public string message  { get; set; }

        public bool isAnswered { get; set; }

        public int parent_postId { get; set; }

        public int userId { get; set; }

        public int carId { get; set; }
    }
}
