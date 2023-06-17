using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Right
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<RoleRight> RoleRights { get; set; }
        public virtual IEnumerable<PermissionRight> PermissionRights { get; set; }

    }
}