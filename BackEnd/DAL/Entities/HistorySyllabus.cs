using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HistorySyllabus
    {
        public long IdUser { get; set; }
        public virtual User User { get; set; }
        public long IdSyllabus { get; set; }
        public string Action { get; set; }
        public virtual Syllabus Syllabus { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}