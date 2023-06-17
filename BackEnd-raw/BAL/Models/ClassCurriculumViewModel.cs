using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class ClassCurriculumViewModel
    {
        public long IdProgram { get; set; }
        public long IdSyllabus { get; set; }

        public IEnumerable<ClassSyllabusViewModel>? Sysllabus { get; set; }
    }
}
