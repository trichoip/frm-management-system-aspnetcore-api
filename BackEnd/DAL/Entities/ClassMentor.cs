using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClassMentor
    {
        public long IdUser { get; set; }
        public virtual User User { get; set; }
        public long IdClass { get; set;}
        public virtual Class Class { get; set; }
        
    }
}