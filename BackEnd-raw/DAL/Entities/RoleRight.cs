using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class RoleRight
    {
        public long IDRight { get; set; }
        public virtual Right Right { get; set; }
        public long IDRole { get; set; }
        public virtual Role Role { get; set; }

    }
}