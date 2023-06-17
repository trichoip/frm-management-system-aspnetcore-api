using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClassLocation
    {
        public long IdClass { get; set; }
        public virtual Class Class { get; set; }
        public long IdLocation { get; set;}
        public virtual Location Location { get; set; }
    }
}