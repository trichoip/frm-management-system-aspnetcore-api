using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Curriculum
    {
        public long IdProgram { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }
        public long IdSyllabus { get; set; }
        public virtual Syllabus Syllabus { get; set; }
        public int NumberOrder { get; set; }

    }
}