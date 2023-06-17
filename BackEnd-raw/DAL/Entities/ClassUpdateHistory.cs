using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClassUpdateHistory
    {
        public long IdClass { get; set; }
        public virtual Class Class { get; set; }
        public long ModifyBy { get; set; }
        public virtual User User { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}