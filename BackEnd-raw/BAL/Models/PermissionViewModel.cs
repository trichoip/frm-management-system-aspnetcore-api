using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class PermissionViewModel
    {
        public long IdRight { get; set; }
        public long IdRole { get; set; }
        public long IdPermission { get; set; }
    }
}
