using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OutputStandard
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<Lesson> Lessons { get; set; }    
    }
}