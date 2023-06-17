using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Session
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public long IdSyllabus { get; set; }
        public virtual Syllabus Syllabus { get; set; }

        public int? Status { get; set; }

        public virtual IEnumerable<Unit> Units { get; set; }
    }
}