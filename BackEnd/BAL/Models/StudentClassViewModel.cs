using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class StudentClassViewModel
    {
        public string ClassCode { get; set; }
        public IEnumerable<ClassMentor> ClassMentors { get; set; }
        public TimeSpan? StartTimeLearning { get; set; }
        public TimeSpan? EndTimeLearing { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
