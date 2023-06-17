using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PermissionRight
    {
        public long IdRight { get; set; }
        public virtual Right Right { get; set; }
        public long IdPermission { get; set; }
        public virtual Permission Permission { get; set; }

        #region Group 5 - Authentication & Authorization
        public long IdRole { get; set; }
        public virtual Role Role { get; set; }
        #endregion

    }
}