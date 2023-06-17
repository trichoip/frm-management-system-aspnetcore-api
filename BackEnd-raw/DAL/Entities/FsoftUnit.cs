using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FsoftUnit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual IEnumerable<FSUContactPoint> FSUContactPoints { get; set; }
        public virtual IEnumerable<Class> Classes { get; set; }
    }
}