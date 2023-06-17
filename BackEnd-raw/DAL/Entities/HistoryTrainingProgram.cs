using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HistoryTrainingProgram
    {
        public long IdUser { get; set; }
        public virtual User User { get; set; }
        public long IdProgram { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}