using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AttendeeType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Class> Classes { get; set; }
    }
}