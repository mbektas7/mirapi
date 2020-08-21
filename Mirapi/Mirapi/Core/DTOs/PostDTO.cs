using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.DTOs
{
    public class PostDTO
    {
        public string title { get; set; }

        public string message { get; set; }

        public bool isAnswered { get; set; }

        public string parentId { get; set; }

        public string userId { get; set; }

        public string carId { get; set; }
    }
}
