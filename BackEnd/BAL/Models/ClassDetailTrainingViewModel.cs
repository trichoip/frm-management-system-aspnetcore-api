using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BAL.Models
{
    public class ClassDetailTrainingViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
        public int? Status { get; set; }
        public IEnumerable<ClassDetailSyllabusViewModel> Syllabus { get; set; }
    }
}
