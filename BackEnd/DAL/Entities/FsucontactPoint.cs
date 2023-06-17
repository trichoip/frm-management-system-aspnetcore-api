using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FSUContactPoint
    {
        public long Id { get; set; }
        public long IdFSU { get; set; }
        public virtual FsoftUnit FSU { get; set; }
        public string Contact { get; set; }
        public int Status { get; set;}
        public virtual IEnumerable<Class> Classes { get; set;}
    }
}