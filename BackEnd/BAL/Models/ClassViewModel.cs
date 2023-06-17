using System;
using System.Linq;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BAL.Models
{
  public class ClassViewModel
  {
    public long Id { get; set; }
    public string? ClassCode { get; set; }
    public string Name { get; set; }
    public long? Status { get; set; }
    public TimeSpan? StartTimeLearning { get; set; }
    public TimeSpan? EndTimeLearing { get; set; }
    public long? ReviewedBy { get; set; }
    public string? ReviewBy { get; set; }
    public DateTime? ReviewedOn { get; set; }
    public long CreatedByid { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public long? ApprovedBy { get; set; }
    public DateTime? ApprovedOn { get; set; }
    public string? Approve { get; set; }
    public DateTime? ApproveOn { get; set; }
    public int? PlannedAtendee { get; set; }
    public int? ActualAttendee { get; set; }
    public int? AcceptedAttendee { get; set; }
    public int? CurrentSession { get; set; }
    public int? CurrentUnit { get; set; }
    public int? StartYear { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int ClassNumber { get; set; }
    public long IdProgram { get; set; }
    public string? ProgramCode { get; set; }
    public long? IdTechnicalGroup { get; set; }
    public string? TechnicalGroup { get; set; }
    public long? IdFSU { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public FsoftUnit? FsoftUnit { get; set; }

    public long? IdFSUContact { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public FSUContactPoint? FSUContactPoint { get; set; }

    public long IdStatus { get; set; }

    public long? IdSite { get; set; }
    public string? Site { get; set; }
    public long? IdUniversity { get; set; }
    public string? UniversityCode { get; set; }
    public long? IdFormatType { get; set; }
    public string? FormatType { get; set; }
    public long? IdProgramContent { get; set; }

    public long? IdAttendeeType { get; set; }
    public string? AttendeeTypeName { get; set; }
    public List<DateTime> ActiveDate { get; set; }
    public List<long>? IdTrainee { get; set; }
    public List<string>? Trainer { get; set; }
    public List<long>? IdAdmin { get; set; }
    public List<string>? Admin { get; set; }
    public List<long>? IdMentor { get; set; }
    public List<string>? Mentor { get; set; }
    public List<long>? Idsyllabus { get; set; }

    public IEnumerable<Curriculum>? Curriculum { get; set; }

    public IEnumerable<ClassFUSViewModel>? ClassFSU { get; set; }

    public List<ClassTrainingProgamViewModel>? ClassTrainingProgram { get; set; }

    public List<string>? Locations { get; set; }

    public IEnumerable<ClassAttendeeViewModel>? ClassAttendee { get; set; }



  }
}


