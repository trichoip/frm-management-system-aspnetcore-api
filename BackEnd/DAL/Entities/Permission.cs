using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Permission
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<PermissionRight> PermissionRights { get; set; }


    }
}