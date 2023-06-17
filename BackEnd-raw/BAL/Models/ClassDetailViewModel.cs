using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class ClassDetailViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ClassCode { get; set; }
        public string Status { get; set; }
        public TimeSpan? StartTimeLearning { get; set; }
        public TimeSpan? EndTimeLearing { get; set; }
        public List<string> Locations { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ReviewBy { get; set; }
        public DateTime? ReviewOn { get; set; }
        public string? Approve { get; set; }
        public List<string>? Trainer { get; set; }
        public string UniversityCode { get; set; }
        public int ClassNumber { get; set; }
        public string? FormatType { get; set; }
        public string Site { get; set; }
        public string? TechnicalGroup { get; set; }
        public string? ProgramCode { get; set; }
        public DateTime? ApproveOn { get; set; }

        public DateTime[]? ActiveDate { get; set; }
        public ClassFUSViewModel? ClassFSU { get; set; }

        public ClassDetailTrainingViewModel ClassTrainingProgram { get; set; }
    }
}
