using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class ProgramViewModel
    {
        public string name { get; set; }
        public string? createdBy { get; set; }
        public List<CurriculumViewModel> syllabi { get; set; }
    }
}
