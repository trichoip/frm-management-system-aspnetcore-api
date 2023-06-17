using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClassProgramCode
    {
        public long Id { get; set; }
        public string ProgramCode { get; set; }
        public virtual IEnumerable<Class> Classes { get; set; }
    }
}