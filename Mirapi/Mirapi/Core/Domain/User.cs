using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Domain
{
    public class User : BaseObject
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }

        public DateTime Birthday { get; set; }

        public string About { get; set; }

        public bool IsFemale { get; set; }

        public bool IsMailConfirmed { get; set; }


        public string Phone { get; set; }

        public string Address { get; set; }



    }
}
