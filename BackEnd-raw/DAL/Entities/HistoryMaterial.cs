using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HistoryMaterial
    {
        public long IdUser { get; set; }
        
        public long IdMaterial { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Action { get; set; }
        public virtual Material Material { get; set; }
        public virtual User User { get; set; }

    }
}