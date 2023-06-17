using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class ClassDetailSyllabusViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Version { get; set; }
        public int Status { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
        public int Duration { get; set; }
    }

}
