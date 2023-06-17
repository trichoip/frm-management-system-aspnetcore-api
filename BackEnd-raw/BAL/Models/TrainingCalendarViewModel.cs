using System.ComponentModel.DataAnnotations;

namespace BAL.Models
{
    public class TrainingCalendarViewModel
    {
        public string? KeyWord { get; set; }
        public string[]? Locations { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string[]? TimeClasses { get; set; }
        public string[]? Statuses { get; set; }
        public string[]? Attendees { get; set; }
        public long? IdFSU { get; set; }
        public long? IdTrainer { get; set; }

    }
}
