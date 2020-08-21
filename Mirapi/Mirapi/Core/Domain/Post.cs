using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Domain
{
    public class Post : BaseObject
    {
        public string title { get; set; }

        public string message  { get; set; }

        public bool isAnswered { get; set; }

        public int view { get; set; }

        public int vote { get; set; }
        public int answer { get; set; }

        public virtual Post parent { get; set; }

        public virtual User user { get; set; }

        public virtual Cars car { get; set; }

        public virtual ICollection<Post>  answers { get; set; }
    }
}
