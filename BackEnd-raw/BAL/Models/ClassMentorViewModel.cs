using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class ClassMentorViewModel
    {
        public long IdUser { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long IdClass { get; set; }
        public string ClassCode { get; set; }
        public TimeSpan? StartTimeLearning { get; set; }
        public TimeSpan? EndTimeLearing { get; set; }
        public int? CurrentSession { get; set; }
        public int? CurrentUnit { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
