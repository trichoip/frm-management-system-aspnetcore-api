using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class SessionViewModel
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        //public long? IdSyllabus { get; set; }
        //team 01
        public int Index { get; set; }
        public int? Status { get; set; }
        //team 01
        public List<UnitViewModel>? Units { get; set; }
    }


}
