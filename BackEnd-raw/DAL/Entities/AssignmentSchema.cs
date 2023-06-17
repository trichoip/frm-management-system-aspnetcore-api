using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AssignmentSchema
    {

        public long IDSyllabus { get; set; }
        public virtual Syllabus Syllabus { get; set; }
        public float? PercentQuiz { get; set; }
        public float? PercentAssigment { get; set; }
        public float? PercentFinal { get; set; }
        public float? PercentTheory { get; set; }
        public float? PercentFinalPractice { get; set; }
        public float? PassingCriterial { get; set; }

    }
}