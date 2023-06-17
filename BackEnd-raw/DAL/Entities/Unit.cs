using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Unit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public long IdSession { get; set; }
        public int? Status { get; set; }
        public virtual Session Session { get; set; }


        public virtual IEnumerable<Lesson> Lessons { get; set; }

    }
}