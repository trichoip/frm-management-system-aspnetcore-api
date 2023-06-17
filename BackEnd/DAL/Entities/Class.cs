using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DAL.Entities
{
    public class Class
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ClassCode { get; set; }
        public int Status { get; set; }
        public TimeSpan? StartTimeLearning { get; set; }
        public TimeSpan? EndTimeLearing { get; set; }
        public long? ReviewedBy { get; set; }
        public virtual User ReviewedUser { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public long CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ApprovedBy { get; set; }
        public virtual User ApprovedUser { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public int? PlannedAtendee { get; set; }
        public int? ActualAttendee { get; set; }
        public int? AcceptedAttendee { get; set; }
        public int? CurrentSession { get; set; }
        public int? CurrentUnit { get; set; }
        public int? StartYear { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ClassNumber { get; set; }
        public long? IdProgram { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }
        public long? IdTechnicalGroup { get; set; }
        public virtual ClassTechnicalGroup classTechnicalGroup { get; set; }
        public long? IdFSU { get; set; }
        public virtual FsoftUnit FsoftUnit { get; set; }
        public long? IdFSUContact { get; set; }
        public virtual FSUContactPoint FSUContactPoint { get; set; }
        public long IdStatus { get; set; }
        public virtual ClassStatus ClassStatus { get; set; }
        public long? IdSite { get; set; }
        public virtual ClassSite ClassSite { get; set; }
        public long? IdUniversity { get; set; }
        public virtual ClassUniversityCode ClassUniversityCode { get; set; }
        public long? IdFormatType { get; set; }
        public virtual ClassFormatType ClassFormatType { get; set; }
        public long? IdProgramContent { get; set; }
        public virtual ClassProgramCode ClassProgramCode { get; set; }
        public long? IdAttendeeType { get; set; }
        public virtual AttendeeType AttendeeType { get; set; }

        public class ImportClassRequest
        {
            public IFormFile File { get; set; }
        }

        public class ImportClassResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        public virtual IEnumerable<ClassLocation> Locations { get; set; }
        public virtual IEnumerable<ClassSelectedDate> ClassSelectedDates { get; set; }
        public virtual IEnumerable<ClassUpdateHistory> ClassUpdateHistories { get; set; }


        public virtual IEnumerable<ClassTrainee> ClassTrainees { get; set; }
        public virtual IEnumerable<ClassAdmin> ClassAdmins { get; set; }
        public virtual IEnumerable<ClassMentor> ClassMentors { get; set; }

    }
}