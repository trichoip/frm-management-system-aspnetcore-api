using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Models
{
  public class UpdateClassViewModel
  {
    public long Id { get; set; }
    public string? ClassCode { get; set; }
    public string Name { get; set; }
    public long? Status { get; set; }
    public TimeSpan? StartTimeLearning { get; set; }
    public TimeSpan? EndTimeLearing { get; set; }
    public long? ReviewedBy { get; set; }
    public DateTime? ReviewedOn { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public long? ApprovedBy { get; set; }
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
    public long IdProgram { get; set; }

    public long? IdTechnicalGroup { get; set; }
    public long? IdFSU { get; set; }
    public long? IdFSUContact { get; set; }
    public long IdStatus { get; set; }
    public long? IdSite { get; set; }
    public long? IdUniversity { get; set; }
    public long? IdFormatType { get; set; }
    public long? IdProgramContent { get; set; }
    public List<long>? IdLocation { get; set; }
    public long? IdAttendeeType { get; set; }
    public List<DateTime> ActiveDate { get; set; }
    public List<long>? IdTrainee { get; set; }
    public List<long>? IdAdmin { get; set; }
    public List<long>? IdMentor { get; set; }
    public List<CurriculumViewModel>? Syllabi { get; set; }

  }
}