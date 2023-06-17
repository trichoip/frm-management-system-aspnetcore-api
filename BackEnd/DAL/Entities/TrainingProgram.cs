using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class TrainingProgram
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual IEnumerable<Class> Classes { get; set; }
        public virtual IEnumerable<HistoryTrainingProgram> HistoryTrainingPrograms { get; set; }
        public virtual IEnumerable<Curriculum> Curricula { get; set; }

    }
}