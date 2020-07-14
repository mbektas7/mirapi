using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Domain
{
    public class BaseObject 
    {
        public BaseObject()
        {
            CreationDate = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreationDate  { get; set; }

        [DefaultValue(0)]
        public bool IsDeleted { get; set; }
    }
}
