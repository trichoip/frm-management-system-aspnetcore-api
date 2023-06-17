using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class ClassCalenderViewModel
    {
        public long Id { get; set; }
        public string ClassCode { get; set; }
        public int? CurrentSession { get; set; }
        public TimeSpan? StartTimeLearning { get; set; }
        public TimeSpan? EndTimeLearing { get; set; }
        public DateTime ActiveDate { get; set; }
        public TrainingCalendarViewModel? TrainingCalendarFilter { get; set; }
        public IEnumerable<ClassLocationViewModel> Locations { get; set; }
        public IEnumerable<AdminViewModel> ClassAdmins { get; set; }

        public IEnumerable<TrainerViewModel> ClassMentors { get; set; }

    }
}
