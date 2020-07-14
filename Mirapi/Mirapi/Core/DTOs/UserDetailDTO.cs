using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.DTOs
{
    public class UserDetailDTO
    {
        public string UserId { get; set; }
        public DateTime Birthday { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsFemale { get; set; }
        public string About { get; set; }
        public string Mail { get; set; }
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

    }
}
