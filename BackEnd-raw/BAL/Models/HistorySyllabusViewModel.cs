using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class HistorySyllabusViewModel
    {
        // Team6
        public long IdUser { get; set; }
        //public long IdSyllabus { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? Action { get; set; }
        // Team6
    }
}

